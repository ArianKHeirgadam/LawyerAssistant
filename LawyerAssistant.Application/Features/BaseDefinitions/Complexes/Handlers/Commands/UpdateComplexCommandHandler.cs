using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Handlers.Commands;

public class UpdateComplexCommandHandler : IRequestHandler<UpdateComplexCommand, SysResult<GetComplexDTO>>
{
    private readonly IRepository<ComplexesModel> _repository;
    private readonly IRepository<CitiesModel> _cityRepository;

    public UpdateComplexCommandHandler(IRepository<ComplexesModel> repository, IRepository<CitiesModel> cityRepository)
    {
        _repository = repository;
        _cityRepository = cityRepository;
    }

    public async Task<SysResult<GetComplexDTO>> Handle(UpdateComplexCommand request, CancellationToken cancellationToken)
    {
        var complex = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (complex == null)
            throw new CustomException(SystemCommonMessage.ComplexeIsNotFound);

        var city = await ValidateAndReturnComplexe(request);

        complex.Edit(request.Title, request.CityId);

        _repository.Update(complex);
        await _repository.SaveChangesAsync();

        return new SysResult<GetComplexDTO>  
        {
            Value = new GetComplexDTO
            {
                Id = complex.Id,
                Title = complex.Title,
                City = complex.City != null ? new GenericDTO() { Id = city.Id, Title = city.Name } : null,
                Province = complex.City != null ? new GenericDTO() { Id = city.Province.Id, Title = city.Province.Name } : null,
            },
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully
        };
    }


    public async Task<CitiesModel> ValidateAndReturnComplexe(UpdateComplexCommand request)
    {
        var city = await _cityRepository.FirstOrDefaultAsync(b => b.Id == request.CityId , c => c.Province);

        if (city is null) throw new CustomException(SystemCommonMessage.CityIsNotFound);

        return city;
    }
}