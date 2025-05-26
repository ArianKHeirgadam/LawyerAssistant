using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Features.Files.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;
using Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Features.Files.Handlers;

public class GetFilesByIdQueryHandler : IRequestHandler<GetFilesByIdQuery, SysResult<FilesDetailsDto>>
{
    private readonly IRepository<FilesModel> _repository;

    public GetFilesByIdQueryHandler(IRepository<FilesModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult<FilesDetailsDto>> Handle(GetFilesByIdQuery request, CancellationToken cancellationToken)
    {
        var file = await _repository.Where(f => f.Id == request.Id)
            .Include(f => f.Customer)
            .Include(f => f.Legal)
            .Include(f => f.Demand)
            .Include(f => f.FilesTypes)
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
                CustomerId = file.IsLegal ? null : (int?)file.CustomerId,
                CustomerFullName = file.IsLegal && file.Customer == null ? null : file.Customer.FirstName + " " + file.Customer.LastName,
                LegalId = file.IsLegal ? (int?)file.LegalId : null,
                LegalCompanyName = file.IsLegal ? file.Legal?.CompanyName : null,
                DemandId = file.DemandId,
                DemandTitle = file.Demand?.Name,
                FileTypeId = file.FileTypeId,
                FileTypeTitle = file.FilesTypes?.Name,
            }
        };
    }
}
