using Application.Exceptions;
using Domain.Aggregates.Identities;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Features.Identities.Customers.Commands;
using LawyerAssistant.Application.Features.Identities.Customers.Queries;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.Identities.Customers.Handlers.Commands;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, SysResult<GetCustomerDetailsDTO>>
{
    private readonly IRepository<CustomersModel> _repository;
    private readonly ISender _sender;
    public CreateCustomerCommandHandler(IRepository<CustomersModel> repository, ISender sender)
    {
        _repository = repository;
        _sender = sender;
    }
    public async Task<SysResult<GetCustomerDetailsDTO>> Handle(CreateCustomerCommand model, CancellationToken cancellationToken)
    {

        var mobileNumberExists = await _repository.FirstOrDefaultAsync(c => c.MobileNumber == model.MobileNumber);

        if (mobileNumberExists != null) throw new CustomException(SystemCommonMessage.MobileNumberIsDeplicated);

        var customer = new CustomersModel(model.MobileNumber, model.FirstName, model.LastName, model.NationalCode, model.BirthDate, model.Address, model.CityId, model.ProvinceId);

        customer.RegDateAdd();

        await _repository.AddAsync(customer);
        await _repository.SaveChangesAsync();
        return  await _sender.Send(new GetCustomerDetailsQuery() { Id = customer.Id });
        
    }
}
