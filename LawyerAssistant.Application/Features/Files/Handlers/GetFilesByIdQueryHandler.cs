using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Features.Files.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;
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
                Demand = file.Demand != null ? new GenericDTO() { Id = file.Demand.Id, Title = file.Demand.Name } : null,
                FileType = file.FilesTypes != null ? new GenericDTO() {Id = file.FilesTypes.Id, Title = file.FilesTypes.Name } : null,
                Legal = file.IsLegal ? new GetLegalDTO() {Id = file.Legal.Id, CompanyName = file.Legal.CompanyName } : null,
                Customer = !file.IsLegal ? new UserGenericDTO() { Id = file.Customer.Id, FirstName = file.Customer.FirstName, LastName = file.Customer.LastName } : null,
            }
        };
    }
}
