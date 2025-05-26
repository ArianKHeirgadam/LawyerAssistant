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

namespace LawyerAssistant.Application.Features.ReActions.Handlers;

public class CreateReActionCommandHandler : IRequestHandler<CreateReActionCommand, SysResult>
{
    private readonly IRepository<ReActionModel> _repository;
    private readonly IRepository<FilesModel> _fileRepository;
    private readonly IRepository<SMSCenterModel> _smsRepository;
    private readonly IRepository<ActionTypesModel> _actionTypesRepository;
    private readonly ITransactionHandler _transactionHandler;
    private readonly IKavenegarSmsSenderService _kavenegarSmsSenderService;
    public CreateReActionCommandHandler(IRepository<ReActionModel> repository, ITransactionHandler transactionHandler, IKavenegarSmsSenderService kavenegarSmsSenderService, IRepository<FilesModel> fileRepository, IRepository<ActionTypesModel> actionTypesRepository, IRepository<SMSCenterModel> smsRepository)
    {
        _repository = repository;
        _transactionHandler = transactionHandler;
        _kavenegarSmsSenderService = kavenegarSmsSenderService;
        _fileRepository = fileRepository;
        _actionTypesRepository = actionTypesRepository;
        _smsRepository = smsRepository;
    }

    public async Task<SysResult> Handle(CreateReActionCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var file = await _fileRepository.Where(c => c.Id == dto.FileId).Include(c => c.Customer)
            .Include(c => c.Demand)
            .Include(c => c.Legal).FirstOrDefaultAsync();

        if (file == null)
            throw new CustomException(SystemCommonMessage.FileIsNotFound);

        var actionType = await _actionTypesRepository.FirstOrDefaultAsync(c => c.Id == dto.ActionTypeId);

        if (actionType == null)
            throw new CustomException(SystemCommonMessage.ActionTypeIsNotFound);

        await _transactionHandler.ExecuteAsync(System.Data.IsolationLevel.ReadCommitted, async () =>
        {
            
            var reAction = new ReActionModel
            (
                dto.ActionTypeId,
                dto.RememberTime,
                dto.VisitTime,
                dto.TimeIsImportant,
                dto.GoingToBranch,
                dto.IsRemember,
                dto.BranchId,
                dto.ComplexeId,
                dto.VisitDate,
                dto.FileId
            );


            await _repository.AddAsync(reAction);
            await _repository.SaveChangesAsync();
            var customerName = file.IsLegal
                ? $"{file.Legal.CompanyName}"
                : $"{file.Customer.FirstName} {file.Customer.LastName}";


            if (dto.IsRemember)
            {
                var reminderTime = await GetReminderTime(dto);

                if (reminderTime == null)
                    throw new CustomException("زمان یادآوری نامعتبر است.");

                string actionTime = (dto.VisitDate + dto.VisitTime).Value.ToHijriDateTime();

                var result = await _kavenegarSmsSenderService.SendSMS(customerName, file.Demand.Name, actionTime, actionType.Title, reminderTime.Value);

                var sms = new SMSCenterModel(result.Value.MessageId.ToString() , reAction.Id);


                await _smsRepository.AddAsync(sms);
                await _smsRepository.SaveChangesAsync();
            }

        });


        return new SysResult { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }



    public async Task<DateTime?> GetReminderTime(CreateReActionDto dto)
    {
        if (!dto.IsRemember)
            return null;

        if (dto.RememberTime == null)
            throw new CustomException("زمان یادآوری مشخص نشده است.");

        DateTime actionDateTime;

        if (dto.TimeIsImportant)
        {
            if (dto.VisitTime == null)
                throw new CustomException("با توجه به اهمیت زمان، ثبت ساعت مراجعه الزامی است.");

            if (dto.VisitDate == null)
                throw new CustomException("تاریخ مراجعه مشخص نشده است.");

            actionDateTime = dto.VisitDate.Date + dto.VisitTime.Value;
        }
        else
        {
            if (dto.VisitDate == null)
                throw new CustomException("تاریخ مراجعه مشخص نشده است.");

            actionDateTime = dto.VisitDate.Date + (dto.VisitTime ?? TimeSpan.Zero);
        }

        var reminderTime = actionDateTime - TimeSpan.FromMinutes(dto.RememberTime.Value);

        if (reminderTime <= DateTime.Now)
            throw new CustomException("زمان یادآوری نمی‌تواند قبل از زمان فعلی باشد.");

        return reminderTime;
    }

}
