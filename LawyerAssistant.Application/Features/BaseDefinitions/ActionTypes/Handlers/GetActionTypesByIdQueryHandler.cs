using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Handlers.Queries;

public class GetActionTypesByIdQueryHandler : IRequestHandler<GetActionTypesByIdQuery, ActionDto?>
{
    private readonly IRepository<ActionTypesModel> _repository;

    public GetActionTypesByIdQueryHandler(IRepository<ActionTypesModel> repository)
    {
        _repository = repository;
    }

    public async Task<ActionDto?> Handle(GetActionTypesByIdQuery request, CancellationToken cancellationToken)
    {
        var action = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (action == null) throw new CustomException(SystemCommonMessage.DataWasNotFound);
            
        return new ActionDto
        {
            Id = action.Id,
            Title = action.Title,
            Priority = action.Priority,
        };
    }
}
