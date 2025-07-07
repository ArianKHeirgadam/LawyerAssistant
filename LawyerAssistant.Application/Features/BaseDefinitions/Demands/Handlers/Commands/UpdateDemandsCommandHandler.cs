using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers.Commands;

public class UpdateDemandsCommandHandler : IRequestHandler<UpdateDemandsCommand, SysResult<GetDemandsDTO>>
{
    private readonly IRepository<DemandsModel> _repository;
    private readonly ISender _sender;
    public UpdateDemandsCommandHandler(IRepository<DemandsModel> repository, ISender sender)
    {
        _repository = repository;
        _sender = sender;
    }
    public async Task<SysResult<GetDemandsDTO>> Handle(UpdateDemandsCommand request, CancellationToken cancellationToken)
    {
        var demand = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (demand is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        demand.Edit(request.Title, request.FileTypeId);

        _repository.Update(demand);
        await _repository.SaveChangesAsync();

        return await _sender.Send(new GetDemandsByIdQuery() { Id = demand.Id });
    }
}
