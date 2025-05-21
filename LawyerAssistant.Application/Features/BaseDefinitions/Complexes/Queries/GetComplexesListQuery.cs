using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Queries;

public class GetComplexesListQuery : IRequest<SysResult<List<GenericDTO>>>
{
    public string? Title { get; set; }
}

