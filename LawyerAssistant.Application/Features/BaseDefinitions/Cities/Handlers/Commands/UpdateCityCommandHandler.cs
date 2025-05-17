using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers.Commands;

public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, SysResult>
{
    private readonly IRepository<CitiesModel> _repository;
    public UpdateCityCommandHandler(IRepository<CitiesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        var city = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (city is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        city.Edit(request.Title, request.ProvinceId);

        _repository.Update(city);
        await _repository.SaveChangesAsync();

        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
