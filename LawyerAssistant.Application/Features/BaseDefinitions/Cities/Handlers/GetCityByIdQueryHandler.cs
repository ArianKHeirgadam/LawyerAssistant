using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers;

public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, SysResult<GetCityDTO>>
{
    private readonly IRepository<CitiesModel> _repository;
    public GetCityByIdQueryHandler(IRepository<CitiesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<GetCityDTO>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id, c => c.Province);

        if (city is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        return new SysResult<GetCityDTO>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = new GetCityDTO
            {
                Id = city.Id,
                Title = city.Name,
                Province = new GenericDTO() { Id = city.Province.Id, Title = city.Province.Name }
            }
        };
    }
}
