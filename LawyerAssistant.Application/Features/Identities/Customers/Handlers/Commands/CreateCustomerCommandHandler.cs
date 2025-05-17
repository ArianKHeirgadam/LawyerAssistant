using Application.Exceptions;
using Domain.Aggregates.Identities;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.Identities.Customers.Commands;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.Identities.Customers.Handlers.Commands;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, SysResult>
{
    private readonly IRepository<CustomersEntity> _repository;
    public CreateCustomerCommandHandler(IRepository<CustomersEntity> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult> Handle(CreateCustomerCommand model, CancellationToken cancellationToken)
    {

        var mobileNumberExists = await _repository.FirstOrDefaultAsync(c => c.MobileNumber == model.MobileNumber);

        if (mobileNumberExists != null) throw new CustomException(SystemCommonMessage.MobileNumberIsDeplicated);

        var customer = new CustomersEntity(model.MobileNumber, model.FirstName, model.LastName, model.NationalCode, model.BirthDate, model.Address, model.CityId, model.ProvinceId);

        customer.RegDateAdd();

        await _repository.AddAsync(customer);
        await _repository.SaveChangesAsync();

        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
