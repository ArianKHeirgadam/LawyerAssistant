using Application.Exceptions;
using Domain.Aggregates.Identities;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Features.Identities.Legals.Commands;
using LawyerAssistant.Application.Features.Identities.Legals.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.IdentitiesModels;
using MediatR;

namespace LawyerAssistant.Application.Features.Identities.Legals.Handlers.Commands;

public class UpdateLegalCustomerCommandHandler : IRequestHandler<UpdateLegalCommand, SysResult<GetLegalCustomerDetailsDTO>>
{
    private readonly IRepository<LegalCustomersModel> _legalRepository;
    private readonly IRepository<CustomersModel> _customerRepository;
    private readonly ISender _sender;
    public UpdateLegalCustomerCommandHandler(
        IRepository<LegalCustomersModel> legalRepository,
        IRepository<CustomersModel> customerRepository,
        ISender sender)
    {
        _legalRepository = legalRepository;
        _customerRepository = customerRepository;
        _sender = sender;
    }

    public async Task<SysResult<GetLegalCustomerDetailsDTO>> Handle(UpdateLegalCommand model, CancellationToken cancellationToken)
    {
        var legal = await _legalRepository.FirstOrDefaultAsync(c => c.Id == model.Id);
        if (legal == null)
            throw new CustomException("مشتری حقوقی یافت نشد.");

        var duplicate = await _legalRepository.FirstOrDefaultAsync(c =>
            c.LegalNationalCode == model.LegalNationalCode && c.Id != model.Id);
        if (duplicate != null)
            throw new CustomException("کد ملی حقوقی تکراری است.");

        legal.CompanyName = model.CompanyName;
        legal.LegalNationalCode = model.LegalNationalCode;
        legal.Address = model.Address;

        // Detach old customers
        var oldCustomers = _customerRepository.Where(c => c.LegalCompanyId == legal.Id).ToList();
        foreach (var customer in oldCustomers)
        {
            customer.LegalCompanyId = null;
        }

        // Assign new ones
        if (model.CustomerIds?.Any() == true)
        {
            var newCustomers = _customerRepository
                .Where(c => model.CustomerIds.Contains(c.Id)).ToList();

            foreach (var customer in newCustomers)
            {
                customer.LegalCompanyId = legal.Id;
            }

            legal.CompanyCustomers = newCustomers;
        }
        else
        {
            legal.CompanyCustomers = new List<CustomersModel>();
        }

        _legalRepository.Update(legal);
        await _legalRepository.SaveChangesAsync();
        return await _sender.Send(new GetLegalDetailsQuery() { Id = legal.Id });
    }
}

