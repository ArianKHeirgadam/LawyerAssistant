using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers.Commands;

public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, SysResult>
{
    private readonly IRepository<CitiesModel> _repository;
    public DeleteCityCommandHandler(IRepository<CitiesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (city is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        _repository.Delete(city);
        await _repository.SaveChangesAsync();

        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
