using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Provinces.Queries;

public class GetProvincesQuery : PagingRequest, IRequest<SysResult<PagingResponse<GenericDTO>>>
{
    public string? Title { get; set; }
}
