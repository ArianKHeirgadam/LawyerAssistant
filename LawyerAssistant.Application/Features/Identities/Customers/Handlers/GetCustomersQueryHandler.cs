using Domain.Aggregates.Identities;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.Identities.Customers.Queries;
using LawyerAssistant.Application.Objects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LawyerAssistant.Application.Features.Identities.Customers.Handlers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, SysResult<PagingResponse<GetCustomersDTO>>>
{
    private readonly IRepository<CustomersEntity> _repository;
    private readonly IOptions<AppConfig> _options;
    public GetCustomersQueryHandler(IRepository<CustomersEntity> repository, IOptions<AppConfig> options)
    {
        _repository = repository;
        _options = options;
    }
    public async Task<SysResult<PagingResponse<GetCustomersDTO>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository
            .Where(c => !string.IsNullOrEmpty(request.FullName) ? (c.FirstName + " " + c.LastName).Contains(request.FullName) : c.Legal == null)
            .Include(c => c.City).Include(c => c.Province)
            .Select(c => new GetCustomersDTO
            {
                CityName = c.City != null ? c.City.Name : null,
                ProvinceName = c.Province != null ? c.Province.Name : null,
                CreateDate = c.CreateDate.ToDateShortFormatString(_options),
                FirstName = c.FirstName,
                Id = c.Id,
                LastName = c.LastName,
                MobileNumber = c.MobileNumber,
                NationalCode = c.NationalCode
            }).ToPagedListAsync(request.PageNumber, request.PageSize);


        return new SysResult<PagingResponse<GetCustomersDTO>>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result
        };
    }
}
