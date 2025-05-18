using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers.Commands;

public class UpdateDemandsCommandHandler : IRequestHandler<UpdateDemandsCommand, SysResult>
{
    private readonly IRepository<DemandsModel> _repository;
    public UpdateDemandsCommandHandler(IRepository<DemandsModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult> Handle(UpdateDemandsCommand request, CancellationToken cancellationToken)
    {
        var demand = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (demand is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        demand.Edit(request.Title, request.FileTypeId);

        _repository.Update(demand);
        await _repository.SaveChangesAsync();

        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
