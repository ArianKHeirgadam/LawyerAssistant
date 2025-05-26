//using Application.Exceptions;
//using LawyerAssistant.Application.Contracts.Infrastructure;
//using LawyerAssistant.Application.Contracts.Persistence;
//using LawyerAssistant.Application.DTOs;
//using LawyerAssistant.Application.Features.ReActions.Commands;
//using LawyerAssistant.Application.Objects;
//using LawyerAssistant.Domain.Aggregates;
//using MediatR;
//using Microsoft.EntityFrameworkCore;

//namespace LawyerAssistant.Application.Features.ReActions.Handlers;

//public class CreateReActionCommandHandler : IRequestHandler<CreateReActionCommand, SysResult>
//{
//    private readonly IRepository<ReActionModel> _repository;
//    private readonly IRepository<FilesModel> _fileRepository;
//    private readonly ITransactionHandler _transactionHandler;
//    private readonly IKavenegarSmsSenderService _kavenegarSmsSenderService;
//    public CreateReActionCommandHandler(IRepository<ReActionModel> repository, ITransactionHandler transactionHandler, IKavenegarSmsSenderService kavenegarSmsSenderService, IRepository<FilesModel> fileRepository)
//    {
//        _repository = repository;
//        _transactionHandler = transactionHandler;
//        _kavenegarSmsSenderService = kavenegarSmsSenderService;
//        _fileRepository = fileRepository;
//    }

//    public async Task<SysResult> Handle(CreateReActionCommand request, CancellationToken cancellationToken)
//    {
//        var dto = request.Dto;

//        var file = await _fileRepository.Where(c => c.Id == dto.FileId).Include(c => c.Customer)
//            .Include(c => c.Legal).FirstOrDefaultAsync();

//        if (file == null)
//            throw new CustomException(SystemCommonMessage.DataWasNotFound);

//        // اگر یادآوری غیرفعال است، نیازی به ادامه نیست
//        if (!dto.IsRemember)
//            return;

//        // اگر یادآوری فعال است و زمان مهم است
//        if (dto.TimeIsImportant)
//        {
//            if (dto.VisitTime == null)
//                throw new CustomException("با توجه به اهمیت زمان، ثبت ساعت مراجعه الزامی است.");

//            if (dto.VisitDate == null)
//                throw new CustomException("تاریخ مراجعه مشخص نشده است.");

//            if (dto.RememberTime == null)
//                throw new CustomException("زمان یادآوری مشخص نشده است.");

//            // ترکیب تاریخ و ساعت برای ساختن DateTime کامل اقدام
//            var actionDateTime = dto.VisitDate.Date + dto.VisitTime.Value;

//            var reminderTime = actionDateTime - TimeSpan.FromMinutes(dto.RememberTime.Value);

//            if (reminderTime <= DateTime.Now)
//                throw new CustomException("زمان یادآوری نمی‌تواند قبل از زمان فعلی باشد.");
//        }
//        else
//        {
//            // زمان مهم نیست ولی یادآوری فعال است
//            if (dto.VisitDate == null)
//                throw new CustomException("تاریخ مراجعه مشخص نشده است.");

//            if (dto.RememberTime == null)
//                throw new CustomException("زمان یادآوری مشخص نشده است.");

//            // اگر ساعت داده نشده، فقط تاریخ رو در نظر بگیریم (00:00)
//            var actionDateTime = dto.VisitDate.Date +
//                                 (dto.VisitTime ?? TimeSpan.Zero); // اگر ساعت نبود، صفر در نظر بگیر

//            var reminderTime = actionDateTime - TimeSpan.FromMinutes(dto.RememberTime.Value);

//            if (reminderTime <= DateTime.Now)
//                throw new CustomException("زمان یادآوری نمی‌تواند قبل از زمان فعلی باشد.");
//        }
//        await _transactionHandler.ExecuteAsync(System.Data.IsolationLevel.ReadCommitted, async () =>
//        {
//            var reAction = new ReActionModel
//            (
//            dto.ActionTypeId,
//            dto.RememberTime,
//            dto.VisitTime,
//            dto.TimeIsImportant,
//            dto.GoingToBranch,
//            dto.IsRemember,
//            dto.BranchId,
//            dto.ComplexeId,
//            dto.VisitDate,
//            dto.FileTypeId
//            );


//            await _repository.AddAsync(reAction);
//            await _repository.SaveChangesAsync();
//        });
       

//        return new SysResult { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
//    }

   

//    public Task CalculateSendDate(CreateReActionDto dto)
//    {
//        if (file == null)
//            throw new CustomException(SystemCommonMessage.DataWasNotFound);

//        if (dto.Time == null)
//            throw new CustomException("زمان اقدام مشخص نشده است.");

//        if (dto.RememberTime == null)
//            throw new CustomException("زمان یادآوری مشخص نشده است.");

//        if (dto.RememberTime <= 0)
//            throw new CustomException("زمان یادآوری باید بیشتر از صفر باشد.");

//        var actionTime = dto.Time.Value;

//        // محاسبه زمان یادآوری
//        var reminderTime = actionTime - TimeSpan.FromMinutes(dto.RememberTime.Value);

//        if (reminderTime <= DateTime.Now)
//            throw new CustomException("زمان یادآوری قبل از زمان فعلی است. لطفاً بررسی نمایید.");

//        return reminderTime;

//    }

//}
