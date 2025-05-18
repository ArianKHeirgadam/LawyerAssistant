using Application.Exceptions;
using Domain.Aggregates.Identities;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.Identities.Customers.Commands;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.Identities.Customers.Handlers.Commands;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, SysResult>
{
    private readonly IRepository<CustomersModel> _repository;

    public UpdateCustomerCommandHandler(IRepository<CustomersModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult> Handle(UpdateCustomerCommand model, CancellationToken cancellationToken)
    {
        var customer = await _repository.FirstOrDefaultAsync(c => c.Id == model.Id);

        if (customer == null)
            throw new CustomException(SystemCommonMessage.DataWasNotFound);

        var duplicateMobile = await _repository.FirstOrDefaultAsync(c =>
            c.MobileNumber == model.MobileNumber && c.Id != model.Id);

        if (duplicateMobile != null)
            throw new CustomException(SystemCommonMessage.MobileNumberIsDeplicated);

        customer.EditDetails(model.MobileNumber, model.FirstName, model.LastName, model.NationalCode,
                        model.BirthDate, model.Address, model.CityId, model.ProvinceId);

        customer.ModDateUpdate();

        _repository.Update(customer);
        await _repository.SaveChangesAsync();

        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
