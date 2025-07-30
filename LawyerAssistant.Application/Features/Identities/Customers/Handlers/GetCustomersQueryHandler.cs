using Domain.Aggregates.Identities;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
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
    private readonly IRepository<CustomersModel> _repository;
    private readonly IOptions<AppConfig> _options;
    public GetCustomersQueryHandler(IRepository<CustomersModel> repository, IOptions<AppConfig> options)
    {
        _repository = repository;
        _options = options;
    }
    public async Task<SysResult<PagingResponse<GetCustomersDTO>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository
            .Where(c => !string.IsNullOrEmpty(request.FirstName) ? c.FirstName.Contains(request.FirstName) : true)
            .Where(c => !string.IsNullOrEmpty(request.LastName) ? c.LastName.Contains(request.LastName) : true)
            .Where(c => !string.IsNullOrEmpty(request.NationalCode) ? c.NationalCode.Contains(request.NationalCode) : true)
            .Where(c => !string.IsNullOrEmpty(request.MobileNumber) ? c.MobileNumber.Contains(request.MobileNumber) : true)
            .Include(c => c.City).Include(c => c.Province)
            .Select(c => new GetCustomersDTO
            {
                City = c.City != null ? new GenericDTO() { Id = c.City.Id, Title = c.City.Name } : null,
                Province = c.City != null ? new GenericDTO() { Id = c.City.Province.Id, Title = c.City.Province.Name } : null,
                CreateDate = c.CreateDate.ToDateShortFormatString(_options),
                FirstName = c.FirstName,
                Id = c.Id,
                LastName = c.LastName,
                MobileNumber = c.MobileNumber,
                NationalCode = c.NationalCode,
                BirthDate = c.BirthDate,                
                Address = c.Address
            }).ToPagedListAsync(request.PageNumber, request.PageSize);


        return new SysResult<PagingResponse<GetCustomersDTO>>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result
        };
    }
}
