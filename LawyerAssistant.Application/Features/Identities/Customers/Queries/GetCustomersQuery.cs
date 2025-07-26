using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.Identities.Customers.Queries;

public class GetCustomersQuery : PagingRequest, IRequest<SysResult<PagingResponse<GetCustomersDTO>>>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MobileNumber { get; set; }
    public string? NationalCode { get; set; }
}
