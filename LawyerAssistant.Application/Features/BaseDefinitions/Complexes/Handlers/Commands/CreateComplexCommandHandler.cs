using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Handlers.Commands;

public class CreateComplexCommandHandler : IRequestHandler<CreateComplexCommand, SysResult<GetComplexDTO>>
{
    private readonly IRepository<ComplexesModel> _repository;
    private readonly IRepository<CitiesModel> _cityRepository;
    public CreateComplexCommandHandler(IRepository<ComplexesModel> repository, IRepository<CitiesModel> cityRepository)
    {
        _repository = repository;
        _cityRepository = cityRepository;
    }

    public async Task<SysResult<GetComplexDTO>> Handle(CreateComplexCommand request, CancellationToken cancellationToken)
    {
        await ValidateCity(request.CityId);

        var complex = new ComplexesModel(request.Title, request.CityId);

        await _repository.AddAsync(complex);
        await _repository.SaveChangesAsync();

        return new SysResult<GetComplexDTO>
        {
            Value = new GetComplexDTO
            {
                Id = complex.Id,
                Title = complex.Title,
                City = complex.City != null ? new GenericDTO() { Id = complex.City.Id, Title = complex.City.Name } : null,
                Province = complex.City != null ? new GenericDTO() { Id = complex.City.Province.Id, Title = complex.City.Province.Name } : null,
            },
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully
        };
    }

    public async Task ValidateCity(int cityId)
    {
        var city = await _cityRepository.FirstOrDefaultAsync(b => b.Id == cityId);

        if (city is null) throw new CustomException(SystemCommonMessage.CityIsNotFound);
    }
}