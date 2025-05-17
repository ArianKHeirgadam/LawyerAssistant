using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.BaseDefinitions.Provinces.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Provinces.Handlers;

public class GetProvincesQueryHandler : IRequestHandler<GetProvincesQuery, SysResult<PagingResponse<GenericDTO>>>
{
    private readonly IRepository<ProvincesModel> _repository;
    public GetProvincesQueryHandler(IRepository<ProvincesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<PagingResponse<GenericDTO>>> Handle(GetProvincesQuery request, CancellationToken cancellationToken)
    {
        var result =  await _repository.Where(c => !string.IsNullOrEmpty(request.Title) ? c.Name.Contains(request.Title) : true)
          .Select(c => new GenericDTO
          {
              Id = c.Id,
              Title = c.Name
          }).ToPagedListAsync(request.PageNumber, request.PageSize);

        return new SysResult<PagingResponse<GenericDTO>>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result
        };
    }
}
