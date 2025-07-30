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
using System.Xml.Linq;

namespace LawyerAssistant.Application.Features.Files.Handlers;

public class GetFilesQueryHandler : IRequestHandler<GetFilesQuery, SysResult<PagingResponse<FilesListDto>>>
{
    private readonly IRepository<FilesModel> _repository;
    private readonly IOptions<AppConfig> _options;

    public GetFilesQueryHandler(IRepository<FilesModel> repository, IOptions<AppConfig> options)
    {
        _repository = repository;
        _options = options;
    }

    public async Task<SysResult<PagingResponse<FilesListDto>>> Handle(GetFilesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository
            .Where(f =>
                string.IsNullOrEmpty(request.Title) ||
                (!f.IsLegal && (f.Customer.FirstName + " " + f.Customer.LastName).ToLower().Contains(request.Title.ToLower()))
            )
            .Where(f => request.IsLegal != null ? f.IsLegal == request.IsLegal : true)
            .Include(f => f.Customer).ThenInclude(c => c.City).ThenInclude(c => c.Province)
            .Include(f => f.Legal)
            .Include(f => f.Demand)
            .Select(f => new FilesListDto
            {
                Id = f.Id,
                IsLegal = f.IsLegal,
                Title = f.Title,
                Demand = f.Demand != null ? new GetDemandsDTO
                {
                    Id = f.Demand.Id,
                    Title = f.Demand.Name,
                    FileType = f.Demand.FilesType != null ? new GenericDTO() { Id = f.Demand.FilesType.Id, Title = f.Demand.FilesType.Name } : null
                } : null,
                Legal = f.IsLegal ? new GetLegalCustomerDetailsDTO
                {
                    Id = f.Legal.Id,
                    Address = f.Legal.Address,
                    CompanyName = f.Legal.CompanyName,
                    LegalNationalCode = f.Legal.LegalNationalCode,
                    Customers = f.Legal.CompanyCustomers.Select(c => new GetCustomersDTO
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
                    }).ToList()
                }
                : null,
                Customer = !f.IsLegal ? new GetCustomersDTO() { 
                    Id = f.Customer.Id,
                    FirstName = f.Customer.FirstName,
                    LastName =  f.Customer.LastName,
                    Address = f.Customer.Address,
                    BirthDate = f.Customer.BirthDate,
                    MobileNumber = f.Customer.MobileNumber,
                    NationalCode = f.Customer.NationalCode,
                    CreateDate = f.RegDateTime.ToLocalDateShortFormatString(_options),
                    City = f.Customer.City != null ? new GenericDTO() { Id = f.Customer.City.Id, Title = f.Customer.City.Name } : null,
                    Province = f.Customer.City != null ? new GenericDTO() { Id = f.Customer.City.Province.Id, Title = f.Customer.City.Province.Name } : null,

                } : null,
            }).ToPagedListAsync(request.PageNumber, request.PageSize);

        return new SysResult<PagingResponse<FilesListDto>>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result
        };
    }
}