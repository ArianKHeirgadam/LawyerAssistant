using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Queries;

public class GetComplexesQuery : PagingRequest, IRequest<SysResult<PagingResponse<GetComplexDTO>>>
{
    public string? Title { get; set; }
    public string? City { get; set; }
}