using Application.Exceptions;
using Domain.Aggregates.Identities;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.Identities.Legals.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.IdentitiesModels;
using MediatR;

namespace LawyerAssistant.Application.Features.Identities.Legals.Handlers.Commands;

public class DeleteLegalCustomerCommandHandler : IRequestHandler<DeleteLegalCustomerCommand, SysResult>
{
    private readonly IRepository<LegalCustomersEntity> _legalRepository;
    private readonly IRepository<CustomersEntity> _customerRepository;

    public DeleteLegalCustomerCommandHandler(
        IRepository<LegalCustomersEntity> legalRepository,
        IRepository<CustomersEntity> customerRepository)
    {
        _legalRepository = legalRepository;
        _customerRepository = customerRepository;
    }

    public async Task<SysResult> Handle(DeleteLegalCustomerCommand model, CancellationToken cancellationToken)
    {
        var legal = await _legalRepository.FirstOrDefaultAsync(c => c.Id == model.Id);
        if (legal == null)
            throw new CustomException("مشتری حقوقی یافت نشد.");

        var customers = _customerRepository.Where(c => c.LegalCompanyId == legal.Id).ToList();
        foreach (var customer in customers)
        {
            customer.LegalCompanyId = null;
        }

        _legalRepository.Delete(legal);
        await _legalRepository.SaveChangesAsync();

        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
