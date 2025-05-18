using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Branches.Queries;

public class GetBranchesQuery : PagingRequest, IRequest<SysResult<PagingResponse<GetBranchDTO>>>
{
    public string? Title { get; set; }
}