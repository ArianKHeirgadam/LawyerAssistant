using Application.Exceptions;
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
        var customer = await _repository.Where(c => c.Id == request.Id).Include(c => c.City).Include(c => c.Province).FirstOrDefaultAsync();

        if (customer is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        return new SysResult<GetCustomerDetailsDTO>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = new GetCustomerDetailsDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                MobileNumber = customer.MobileNumber,
                NationalCode = customer.NationalCode,
                CreateDate = customer.CreateDate.ToDateShortFormatString(_options),
                Address = customer.Address,
                BirthDate = customer.BirthDate,
                City = customer.City != null ? new GenericDTO() { Id = customer.City.Id, Title = customer.City.Name } : null,
                Province = customer.Province != null ? new GenericDTO() { Id = customer.Province.Id, Title = customer.Province.Name } : null,
            }
        };
    }
}
