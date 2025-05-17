using Application.Exceptions;
using Domain.Aggregates.Identities;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.Identities.Customers.Commands;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.Identities.Customers.Handlers.Commands;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, SysResult>
{
    private readonly IRepository<CustomersEntity> _repository;

    public DeleteCustomerCommandHandler(IRepository<CustomersEntity> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (customer == null)
            throw new CustomException(SystemCommonMessage.DataWasNotFound);

        _repository.Delete(customer);
        await _repository.SaveChangesAsync();

        return new SysResult();
    }
}
