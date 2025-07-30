using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.Files.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LawyerAssistant.Application.Features.Files.Handlers;

public class GetFilesByIdQueryHandler : IRequestHandler<GetFilesByIdQuery, SysResult<FilesDetailsDto>>
{
    private readonly IRepository<FilesModel> _repository;
    private readonly IOptions<AppConfig> _options;

    public GetFilesByIdQueryHandler(IRepository<FilesModel> repository, IOptions<AppConfig> options)
    {
        _repository = repository;
        _options = options;
    }

    public async Task<SysResult<FilesDetailsDto>> Handle(GetFilesByIdQuery request, CancellationToken cancellationToken)
    {
        var file = await _repository.Where(f => f.Id == request.Id)
            .Include(f => f.Customer).ThenInclude(c => c.City).ThenInclude(c => c.Province)
            .Include(f => f.Legal)
            .Include(f => f.Demand).ThenInclude(c => c.FilesType)
            .FirstOrDefaultAsync();

        if (file == null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        return new SysResult<FilesDetailsDto>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = new FilesDetailsDto
            {
                Id = file.Id,
                IsLegal = file.IsLegal,
                Title = file.Title,
                Demand = file.Demand != null ? new GetDemandsDTO
                {
                    Id = file.Demand.Id,
                    Title = file.Demand.Name,
                    FileType = file.Demand.FilesType != null ? new GenericDTO() { Id = file.Demand.FilesType.Id, Title = file.Demand.FilesType.Name } : null
                } : null,
                Legal = file.IsLegal ? new GetLegalCustomerDetailsDTO
                {
                    Id = file.Legal.Id,
                    Address = file.Legal.Address,
                    CompanyName = file.Legal.CompanyName,
                    LegalNationalCode = file.Legal.LegalNationalCode,
                    Customers = file.Legal.CompanyCustomers != null ? file.Legal.CompanyCustomers.Select(c => new GetCustomersDTO
                    {
                        City = c.City != null ? new GenericDTO() { Id = c.City.Id, Title = c.City.Name } : null,
                        Province = c.City != null ? new GenericDTO() { Id = c.City.Province.Id, Title = c.City.Province.Name } : null,
                        CreateDate = c.CreateDate.ToDateShortFormatString(_options),
                        MobileNumber = c.MobileNumber,
                        NationalCode = c.NationalCode,
                        BirthDate = c.BirthDate,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Address = c.Address,
                        Id = c.Id
                    }).ToList() : null
                }
                : null,
                Customer = !file.IsLegal ? new GetCustomersDTO()
                {
                    Id = file.Customer.Id,
                    FirstName = file.Customer.FirstName,
                    LastName = file.Customer.LastName,
                    Address = file.Customer.Address,
                    BirthDate = file.Customer.BirthDate,
                    MobileNumber = file.Customer.MobileNumber,
                    NationalCode = file.Customer.NationalCode,
                    CreateDate = file.RegDateTime.ToLocalDateShortFormatString(_options),
                    City = file.Customer.City != null ? new GenericDTO() { Id = file.Customer.City.Id, Title = file.Customer.City.Name } : null,
                    Province = file.Customer.City != null ? new GenericDTO() { Id = file.Customer.City.Province.Id, Title = file.Customer.City.Province.Name } : null,

                } : null,
            }
        };
    }
}
