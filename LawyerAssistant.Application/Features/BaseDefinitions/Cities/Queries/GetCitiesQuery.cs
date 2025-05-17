using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Queries;

public class GetCitiesQuery : PagingRequest, IRequest<SysResult<PagingResponse<GetCityDTO>>>
{
    public string? Title { get; set; }
}
