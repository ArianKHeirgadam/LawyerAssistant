using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Infrastructure;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.ReActions.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LawyerAssistant.Application.Features.ReActions.Handlers.Commands;

public class UpdateReactionCommandHandler : IRequestHandler<UpdateReactionCommand, SysResult>
{
    private readonly IRepository<ReActionModel> _repository;
    private readonly IRepository<FilesModel> _fileRepository;
    private readonly IRepository<SMSCenterModel> _smsRepository;
    private readonly IRepository<ActionTypesModel> _actionTypesRepository;
    private readonly IOptions<AppConfig> _options;
    private readonly ITransactionHandler _transactionHandler;
    private readonly IKavenegarSmsSenderService _kavenegarSmsSenderService;
    public UpdateReactionCommandHandler(IRepository<ReActionModel> repository, ITransactionHandler transactionHandler, IKavenegarSmsSenderService kavenegarSmsSenderService, IRepository<FilesModel> fileRepository, IRepository<ActionTypesModel> actionTypesRepository, IRepository<SMSCenterModel> smsRepository, IOptions<AppConfig> options)
    {
        _repository = repository;
        _transactionHandler = transactionHandler;
        _kavenegarSmsSenderService = kavenegarSmsSenderService;
        _fileRepository = fileRepository;
        _actionTypesRepository = actionTypesRepository;
        _smsRepository = smsRepository;
        _options = options;
    }

    public async Task<SysResult> Handle(UpdateReactionCommand request, CancellationToken cancellationToken)
    {
        var model = request.Dto;

        var reaction = await GetReaction(model.Id);

        await _transactionHandler.ExecuteAsync(System.Data.IsolationLevel.ReadCommitted, async () =>
        {
            if (model.IsRemember)
                await RenewTheSMS(model, reaction);

            reaction.Edit(
                model.ActionTypeId,
                model.RememberTime,
                model.VisitTime,
                model.TimeIsImportant,
                model.GoingToBranch,
                model.IsRemember,
                model.BranchId,
                model.ComplexeId,
                model.VisitDate,
                model.FileId
            );

            await _repository.SaveChangesAsync();
        });


        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }

    private async Task RenewTheSMS(UpdateReactionDTO model, ReActionModel reaction)
    {
        bool hasChanged =
            model.RememberTime != reaction.RememberTime ||
            model.VisitTime != reaction.VisitTime;

        if (!hasChanged)
            return;

        var sms = await _smsRepository.FirstOrDefaultAsync(c => c.ReactionId == reaction.Id);
        if (sms == null)
            throw new CustomException("پیامک مرتبط پیدا نشد."); // Optional fallback message

        var file = await _fileRepository.Where(c => c.Id == model.FileId).Include(c => c.Customer)
            .Include(c => c.Demand)
            .Include(c => c.Legal).FirstOrDefaultAsync();

        if (file == null)
            throw new CustomException(SystemCommonMessage.FileIsNotFound);

        // Cancel previous SMS
        var cancelResponse = await _kavenegarSmsSenderService.Cancel(sms.MessageId);
        var smsResponse = cancelResponse.Value.entries.FirstOrDefault();
        sms.Edit(smsResponse.status, smsResponse.statustext);

        // Prepare new SMS content
        var customerName = file.IsLegal
            ? file.Legal.CompanyName
            : $"{file.Customer.FirstName} {file.Customer.LastName}";

        var actionTime = (model.VisitDate + model.VisitTime).Value.ToLocalDateLongFormatString(_options);

        var reminder = await GetReminderTime(model);
        if (reminder == null)
            throw new CustomException("زمان یادآوری نامعتبر است.");

        var newSmsResponse = await _kavenegarSmsSenderService.SendSMS(
            customerName,
            file.Demand.Name,
            actionTime,
            reaction.ActionType.Title,
            reminder.Value
        );

        var newSms = new SMSCenterModel(newSmsResponse.Value.entries.FirstOrDefault().messageid.ToString(), reaction.Id);

        await _smsRepository.AddAsync(newSms);
        await _smsRepository.SaveChangesAsync();
    }
    private async Task<ReActionModel> GetReaction(int reactionId)
    {
        var reaction = await _repository.SelectAllAsQuerable().Include(c => c.Branch)
           .Include(c => c.Complexe).Include(c => c.ActionType).Include(c => c.Files).ThenInclude(c => c.Demand)
           .Include(c => c.Files).ThenInclude(c => c.Customer).Include(c => c.Files).ThenInclude(c => c.Legal)
           .FirstOrDefaultAsync(c => c.Id == reactionId);

        if (reaction == null) throw new CustomException(SystemCommonMessage.ReactionIsNotFound);

        return reaction;
    }
    private async Task<DateTime?> GetReminderTime(UpdateReactionDTO dto)
    {
        if (!dto.IsRemember)
            return null;

        if (dto.RememberTime == null)
            throw new CustomException("زمان یادآوری مشخص نشده است.");

        if (dto.VisitDate == null)
            throw new CustomException("تاریخ مراجعه مشخص نشده است.");

        if (dto.TimeIsImportant && dto.VisitTime == null)
            throw new CustomException("با توجه به اهمیت زمان، ثبت ساعت مراجعه الزامی است.");

        var visitTime = dto.VisitTime ?? TimeSpan.Zero;
        var actionDateTime = dto.VisitDate.Date + visitTime;

        var reminderTime = actionDateTime - TimeSpan.FromHours(dto.RememberTime.Value);

        if (reminderTime <= DateTime.Now)
            throw new CustomException("زمان یادآوری نمی‌تواند قبل از زمان فعلی باشد.");

        return reminderTime;
    }
}
