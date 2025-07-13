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

public class GetDemandsQueryHandler : IRequestHandler<GetDemandsQuery, SysResult<PagingResponse<GetDemandsDTO>>>
{
    private readonly IRepository<DemandsModel> _repository;
    public GetDemandsQueryHandler(IRepository<DemandsModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<PagingResponse<GetDemandsDTO>>> Handle(GetDemandsQuery request, CancellationToken cancellationToken)
    {
        var result =  await _repository.Where(c => !string.IsNullOrEmpty(request.Title) ? c.Name.Contains(request.Title) : true)
            .Include(c => c.FilesType)
            .Select(c => new GetDemandsDTO
            {
                Id = c.Id,
                Title = c.Name,
                FileType = c.FilesType != null ? new GenericDTO() { Id = c.FilesType.Id , Title = c.FilesType.Name} : null
            }).ToPagedListAsync(request.PageNumber, request.PageSize);


        return new SysResult<PagingResponse<GetDemandsDTO>>
        {
            IsSuccess = true,   
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result
        };
    }
}
