using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.Identities.Legals.Queries;

public class GetLegalsQuery : PagingRequest, IRequest<SysResult<PagingResponse<GetLegalCustomersDTO>>>
{
    public string? CompanyName { get; set; }
    public string? LegalNationalCode { get; set; }
}