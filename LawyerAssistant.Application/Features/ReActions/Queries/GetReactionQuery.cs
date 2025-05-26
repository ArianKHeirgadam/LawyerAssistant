using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.ReActions.Queries;

public class GetReactionQuery : PagingRequest, IRequest<SysResult<PagingResponse<ReactionGetDTO>>>
{
    public bool IsCompleted { get; set; } = false;
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}
