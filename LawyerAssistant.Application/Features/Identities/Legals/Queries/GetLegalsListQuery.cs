using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.FileTypes.Queries;

public class GetLegalsListQuery : IRequest<SysResult<List<GenericDTO>>>
{
    public string? Title { get; set; }
}

