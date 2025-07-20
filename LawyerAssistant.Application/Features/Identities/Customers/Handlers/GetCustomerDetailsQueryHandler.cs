using Application.Exceptions;
using Domain.Aggregates.Identities;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.Identities.Customers.Queries;
using LawyerAssistant.Application.Objects;
using MediatR;
using Microsoft.Extensions.Options;

namespace LawyerAssistant.Application.Features.Identities.Customers.Handlers;

public class GetCustomerDetailsQueryHandler : IRequestHandler<GetCustomerDetailsQuery, SysResult<GetCustomerDetailsDTO>>
{
    private readonly IRepository<CustomersModel> _repository;
    private readonly IOptions<AppConfig> _options;
    public GetCustomerDetailsQueryHandler(IRepository<CustomersModel> repository, IOptions<AppConfig> options)
    {
        _repository = repository;
        _options = options;
    }
    public async Task<SysResult<GetCustomerDetailsDTO>> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
    {
        var customer = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id, c => c.City);

        if (customer is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        return new SysResult<GetCustomerDetailsDTO>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.DataWasNotFound,
            Value = new GetCustomerDetailsDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                MobileNumber = customer.MobileNumber,
                NationalCode = customer.NationalCode,
                CreateDate = customer.CreateDate.ToDateShortFormatString(_options),
                Address = customer.Address,
                City = customer.City != null ? new GenericDTO() { Id = customer.City.Id, Title = customer.City.Name } : null,
                Province = customer.City != null ? new GenericDTO() { Id = customer.City.Province.Id, Title = customer.City.Province.Name } : null,
            }
        };
    }
}
