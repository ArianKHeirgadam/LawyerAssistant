using Application.Exceptions;
using Domain.Aggregates.Identities;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.Identities.Customers.Commands;
using LawyerAssistant.Application.Objects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Features.Identities.Customers.Handlers.Commands;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, SysResult>
{
    private readonly IRepository<CustomersModel> _repository;

    public DeleteCustomerCommandHandler(IRepository<CustomersModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var actions = await _repository
            .Where(x => request.Ids.Contains(x.Id))
            .ToListAsync();

            if (actions.Count != request.Ids.Count)
                throw new CustomException(SystemCommonMessage.DataWasNotFound);

            _repository.DeleteRange(actions);
            await _repository.SaveChangesAsync();

            return new SysResult
            {
                IsSuccess = true,
                Message = SystemCommonMessage.OperationDoneSuccessfully
            };

        }
        catch (Exception ex)
        {
            return new SysResult
            {
                IsSuccess = false,
                Message = SystemCommonMessage.CantRemoveBecauseThereIsDependy
            };
        }
    }
}
