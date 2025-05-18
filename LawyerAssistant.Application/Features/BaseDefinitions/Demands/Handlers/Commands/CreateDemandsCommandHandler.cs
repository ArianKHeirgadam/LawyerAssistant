using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers.Commands;

public class CreateDemandsCommandHandler : IRequestHandler<CreateDemandsCommand, SysResult>
{
    private readonly IRepository<DemandsModel> _repository;
    public CreateDemandsCommandHandler(IRepository<DemandsModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult> Handle(CreateDemandsCommand model, CancellationToken cancellationToken)
    {
        var demand = new DemandsModel(model.Title, model.FileTypeId);

        await _repository.AddAsync(demand);
        await _repository.SaveChangesAsync();

        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
