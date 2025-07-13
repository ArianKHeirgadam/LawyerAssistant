using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers.Commands;

public class CreateDemandsCommandHandler : IRequestHandler<CreateDemandsCommand, SysResult<GetDemandsDTO>>
{
    private readonly IRepository<DemandsModel> _repository;
    private readonly ISender _sender;
    public CreateDemandsCommandHandler(IRepository<DemandsModel> repository, ISender sender)
    {
        _repository = repository;
        _sender = sender;
    }
    public async Task<SysResult<GetDemandsDTO>> Handle(CreateDemandsCommand model, CancellationToken cancellationToken)
    {
        var demand = new DemandsModel(model.Title, model.FileTypeId);

        await _repository.AddAsync(demand);
        await _repository.SaveChangesAsync();

        return await _sender.Send(new GetDemandsByIdQuery() { Id = demand.Id });
    }
}
