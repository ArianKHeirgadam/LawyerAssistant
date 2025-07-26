using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Queries;

public class GetDemandsQuery : PagingRequest, IRequest<SysResult<PagingResponse<GetDemandsDTO>>>
{
    public string? Title { get; set; }
    public string? FileTypes { get; set; }
}
