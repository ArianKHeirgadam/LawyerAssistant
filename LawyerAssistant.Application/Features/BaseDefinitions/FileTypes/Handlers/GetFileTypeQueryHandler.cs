using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers;

public class GetFileTypeQueryHandler : IRequestHandler<GetFileTypeQuery, SysResult<PagingResponse<GenericDTO>>>
{
    private readonly IRepository<FilesTypesModel> _repository;
    public GetFileTypeQueryHandler(IRepository<FilesTypesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<PagingResponse<GenericDTO>>> Handle(GetFileTypeQuery request, CancellationToken cancellationToken)
    {
        var result =  await _repository.Where(c => !string.IsNullOrEmpty(request.Title) ? c.Name.Contains(request.Title) : true)
            .Select(c => new GenericDTO
            {
                Id = c.Id,
                Title = c.Name,
            }).ToPagedListAsync(request.PageNumber, request.PageSize);


        return new SysResult<PagingResponse<GenericDTO>>
        {
            IsSuccess = true,   
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result
        };
    }
}
