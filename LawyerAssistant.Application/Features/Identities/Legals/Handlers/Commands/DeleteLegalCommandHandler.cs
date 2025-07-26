using Application.Exceptions;
using Domain.Aggregates.Identities;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.Identities.Legals.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.IdentitiesModels;
using MediatR;

namespace LawyerAssistant.Application.Features.Identities.Legals.Handlers.Commands;

public class DeleteLegalCustomerCommandHandler : IRequestHandler<DeleteLegalCommand, SysResult>
{
    private readonly IRepository<LegalCustomersModel> _legalRepository;
    private readonly IRepository<CustomersModel> _customerRepository;

    public DeleteLegalCustomerCommandHandler(
        IRepository<LegalCustomersModel> legalRepository,
        IRepository<CustomersModel> customerRepository)
    {
        _legalRepository = legalRepository;
        _customerRepository = customerRepository;
    }

    public async Task<SysResult> Handle(DeleteLegalCommand model, CancellationToken cancellationToken)
    {
        try
        {
            foreach (var id in model.Ids)
            {
                var legal = await _legalRepository.FirstOrDefaultAsync(c => c.Id == id);
                if (legal == null)
                    throw new CustomException($"مشتری حقوقی با شناسه {id} یافت نشد.");

                var customers = _customerRepository.Where(c => c.LegalCompanyId == legal.Id).ToList();
                foreach (var customer in customers)
                {
                    customer.LegalCompanyId = null;
                }

                _legalRepository.Delete(legal);
            }

            await _legalRepository.SaveChangesAsync();

            return new SysResult
            {
                IsSuccess = true,
                Message = SystemCommonMessage.OperationDoneSuccessfully
            };
        }
        catch (Exception)
        {
            throw new CustomException(SystemCommonMessage.CantRemoveBecauseThereIsDependy);
        }
        
    }
}
