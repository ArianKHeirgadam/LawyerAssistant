using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.Identities.Legals.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.IdentitiesModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LawyerAssistant.Application.Features.Identities.Legals.Handlers;

public class GetLegalCustomerDetailsQueryHandler : IRequestHandler<GetLegalDetailsQuery, SysResult<GetLegalCustomerDetailsDTO>>
{
    private readonly IRepository<LegalCustomersModel> _repository;
    private readonly IOptions<AppConfig> _options;
    public GetLegalCustomerDetailsQueryHandler(IRepository<LegalCustomersModel> repository, IOptions<AppConfig> options)
    {
        _repository = repository;
        _options = options;
    }

    public async Task<SysResult<GetLegalCustomerDetailsDTO>> Handle(GetLegalDetailsQuery request, CancellationToken cancellationToken)
    {
        var legal = await _repository.Where(c => c.Id == request.Id).Include(c => c.CompanyCustomers).ThenInclude(c => c.City).ThenInclude(c => c.Province).FirstOrDefaultAsync();

        if (legal == null)
            throw new CustomException("مشتری حقوقی یافت نشد.");

        var dto = new GetLegalCustomerDetailsDTO
        {
            Id = legal.Id,
            Address = legal.Address,
            CompanyName = legal.CompanyName,
            LegalNationalCode = legal.LegalNationalCode,
            Customers = legal.CompanyCustomers.Select(c => new GetCustomersDTO
            {
                City = c.City != null ? new GenericDTO() { Id = c.City.Id, Title = c.City.Name } : null,
                Province = c.City != null ? new GenericDTO() { Id = c.City.Province.Id, Title = c.City.Province.Name } : null,
                CreateDate = c.CreateDate.ToDateShortFormatString(_options),
                FirstName = c.FirstName,
                Id = c.Id,
                LastName = c.LastName,
                MobileNumber = c.MobileNumber,
                NationalCode = c.NationalCode,
                BirthDate = c.BirthDate,
                Address = c.Address
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

