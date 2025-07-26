using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Queries;

public class GetAllActionTypesQuery : PagingRequest, IRequest<SysResult<PagingResponse<ActionDto>>>
{
    public string? Title { get; set; }
    public int? Priority { get; set; }
}
