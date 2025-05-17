using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers.Commands;

public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, SysResult>
{
    private readonly IRepository<CitiesModel> _repository;
    public CreateCityCommandHandler(IRepository<CitiesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult> Handle(CreateCityCommand model, CancellationToken cancellationToken)
    {
        var city = new CitiesModel(model.Title, model.ProvinceId);

        await _repository.AddAsync(city);
        await _repository.SaveChangesAsync();

        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
