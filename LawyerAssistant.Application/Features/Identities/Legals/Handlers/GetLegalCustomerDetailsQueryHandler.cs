using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Features.Identities.Legals.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.IdentitiesModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Features.Identities.Legals.Handlers;

public class GetLegalCustomerDetailsQueryHandler : IRequestHandler<GetLegalDetailsQuery, SysResult<GetLegalCustomerDetailsDTO>>
{
    private readonly IRepository<LegalCustomersModel> _repository;

    public GetLegalCustomerDetailsQueryHandler(IRepository<LegalCustomersModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult<GetLegalCustomerDetailsDTO>> Handle(GetLegalDetailsQuery request, CancellationToken cancellationToken)
    {
        var legal = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id, c => c.CompanyCustomers);

        if (legal == null)
            throw new CustomException("مشتری حقوقی یافت نشد.");

        var dto = new GetLegalCustomerDetailsDTO
        {
            Id = legal.Id,
            Address = legal.Address,
            CompanyName = legal.CompanyName,
            LegalNationalCode = legal.LegalNationalCode,
            Customers = legal.CompanyCustomers.Select(c => new GenericDTO { 
                Id = c.Id,
                Title = $"{c.FirstName} {c.LastName}"
            }).ToList()
        };

        return new SysResult<GetLegalCustomerDetailsDTO>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = dto
        };
    }
}

