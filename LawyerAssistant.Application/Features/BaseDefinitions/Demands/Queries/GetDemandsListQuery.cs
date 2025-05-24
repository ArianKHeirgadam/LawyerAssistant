using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Demands.Queries;

public class GetDemandsListQuery : IRequest<SysResult<List<GenericDTO>>>
{
    public string? Title { get; set; }
}


