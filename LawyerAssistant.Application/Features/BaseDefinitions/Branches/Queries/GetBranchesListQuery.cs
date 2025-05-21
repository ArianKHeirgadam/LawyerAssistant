using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Branches.Queries;

public class GetBranchesListQuery : IRequest<SysResult<List<GenericDTO>>>
{
    public string? Title { get; set; }
}

    