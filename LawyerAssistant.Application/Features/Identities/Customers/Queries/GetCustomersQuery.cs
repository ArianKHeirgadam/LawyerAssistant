using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.Identities.Customers.Queries;

public class GetCustomersQuery : PagingRequest, IRequest<SysResult<PagingResponse<GetCustomersDTO>>>
{
    public string? FullName { get; set; }
}
