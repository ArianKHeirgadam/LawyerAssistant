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

public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, SysResult<PagingResponse<GetCityDTO>>>
{
    private readonly IRepository<CitiesModel> _repository;
    public GetCitiesQueryHandler(IRepository<CitiesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<PagingResponse<GetCityDTO>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository
            .Where(c => !string.IsNullOrEmpty(request.Title) ? c.Name.Contains(request.Title) : true)
            .Where(c =>  request.ProvinceId != null && request.ProvinceId != 0 ? c.ProvinceId == request.ProvinceId : true)
            .Include(c => c.Province).Select(c => new GetCityDTO
            {
                Id = c.Id,
                Title = c.Name,
                //Province = new GenericDTO() { Id = c.Province.Id, Title = c.Province.Name }
            }).ToPagedListAsync(request.PageNumber, request.PageSize);


        return new SysResult<PagingResponse<GetCityDTO>>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = result
        };
    }
}
