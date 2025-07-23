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

public class CreateLegalCustomerCommandHandler : IRequestHandler<CreateLegalCommand, SysResult<GetLegalCustomerDetailsDTO>>
{
    private readonly IRepository<LegalCustomersModel> _legalRepository;
    private readonly IRepository<CustomersModel> _customerRepository;
    private readonly ISender _sender;
    public CreateLegalCustomerCommandHandler(
        IRepository<LegalCustomersModel> legalRepository,
        IRepository<CustomersModel> customerRepository,
        ISender sender)
    {
        _legalRepository = legalRepository;
        _customerRepository = customerRepository;
        _sender = sender;
    }

    public async Task<SysResult<GetLegalCustomerDetailsDTO>> Handle(CreateLegalCommand model, CancellationToken cancellationToken)
    {
        var exists = await _legalRepository.FirstOrDefaultAsync(c => c.LegalNationalCode == model.LegalNationalCode);
        if (exists != null)
            throw new CustomException("کد ملی حقوقی تکراری است.");

        var legal = new LegalCustomersModel(model.CompanyName, model.LegalNationalCode, model.Address);

        if (model.CustomerIds?.Any() == true)
        {
            var customers = _customerRepository
                .Where(c => model.CustomerIds.Contains(c.Id));

            foreach (var customer in customers)
            {
                customer.LegalCompanyId = legal.Id;
                legal.CompanyCustomers.Add(customer);
            }
        }

        await _legalRepository.AddAsync(legal);
        await _legalRepository.SaveChangesAsync();
        return await _sender.Send(new GetLegalDetailsQuery() { Id = legal.Id });
    }
}
