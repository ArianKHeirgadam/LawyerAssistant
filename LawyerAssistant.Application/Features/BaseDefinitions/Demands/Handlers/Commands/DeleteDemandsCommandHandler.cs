using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers.Commands;

public class DeleteDemandsCommandHandler : IRequestHandler<DeleteDemandsCommand, SysResult>
{
    private readonly IRepository<DemandsModel> _repository;
    public DeleteDemandsCommandHandler(IRepository<DemandsModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult> Handle(DeleteDemandsCommand request, CancellationToken cancellationToken)
    {
        var demand = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (demand is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        _repository.Delete(demand);
        await _repository.SaveChangesAsync();

        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
