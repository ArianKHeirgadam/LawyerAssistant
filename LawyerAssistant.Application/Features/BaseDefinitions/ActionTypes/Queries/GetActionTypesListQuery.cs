using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Queries;

public class GetActionTypesListQuery : IRequest<SysResult<List<GenericDTO>>>
{
    public string? Title { get; set; }
}
