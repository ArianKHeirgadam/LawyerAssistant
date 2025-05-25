using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.Files.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
            .Include(f => f.Customer)
            .Include(f => f.Legal)
            .Include(f => f.Demand)
            .Include(f => f.FilesTypes)
            .Select(f => new FilesListDto
            {
                Id = f.Id,
                IsLegal = f.IsLegal,
                Title = f.Title,
                CustomerId = !f.IsLegal ? f.CustomerId : (int?)null,
                CustomerFullName = f.IsLegal && f.Customer == null ? null : f.Customer.FirstName + " " + f.Customer.LastName,
                LegalId = f.IsLegal ? f.LegalId : (int?)null,
                LegalCompanyName = f.IsLegal ? f.Legal.CompanyName : null,
                DemandId = f.DemandId,
                DemandTitle = f.Demand.Name,
                FileTypeId = f.FileTypeId,
                FileTypeTitle = f.FilesTypes.Name,
            }).ToPagedListAsync(request.PageNumber, request.PageSize);

        return new SysResult<PagingResponse<FilesListDto>>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result
        };
    }
}