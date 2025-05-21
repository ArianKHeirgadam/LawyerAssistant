using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.ReActions.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;

namespace LawyerAssistant.Application.Features.ReActions.Handlers;

public class CreateReActionCommandHandler : IRequestHandler<CreateReActionCommand, SysResult>
{
    private readonly IRepository<ReActionModel> _repository;
    public CreateReActionCommandHandler(IRepository<ReActionModel> repository) => _repository = repository;
    public async Task<SysResult> Handle(CreateReActionCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var reAction = new ReActionModel(
            dto.ActionTypeId,
            timeIsImportant: dto.TimeIsImportant,
            goingToBranch: dto.GoingToBranch,
            branchId: dto.BranchId,
            complexeId: dto.ComplexeId,
            date: dto.Time,
            customerId: dto.CustomerId,
            fileTypeId: dto.FileTypeId
        );

        await _repository.AddAsync(reAction);
        await _repository.SaveChangesAsync();

        return new SysResult { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
