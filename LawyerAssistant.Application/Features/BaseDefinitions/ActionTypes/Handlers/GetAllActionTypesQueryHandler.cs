using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Handlers;

public class GetAllActionTypesQueryHandler : IRequestHandler<GetAllActionTypesQuery, SysResult<PagingResponse<ActionDto>>>
{
    private readonly IRepository<ActionTypesModel> _repository;

    public GetAllActionTypesQueryHandler(IRepository<ActionTypesModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult<PagingResponse<ActionDto>>> Handle(GetAllActionTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Where(a => !string.IsNullOrWhiteSpace(request.Title) ? a.Title.Contains(request.Title) : true)
            .Select(a => new ActionDto
            {
                Id = a.Id,
                Title = a.Title,
                Priority = a.Priority,
                RememberTime = a.RememberTime
            }).ToPagedListAsync(request.PageNumber, request.PageSize);

        return new SysResult<PagingResponse<ActionDto>>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result
        };
    }
}