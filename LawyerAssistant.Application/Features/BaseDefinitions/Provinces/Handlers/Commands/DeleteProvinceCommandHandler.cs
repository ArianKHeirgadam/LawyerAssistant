using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Provinces.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Provinces.Handlers.Commands;

public class DeleteProvinceCommandHandler : IRequestHandler<DeleteProvinceCommand, SysResult>
{
    private readonly IRepository<ProvincesModel> _repository;
    public DeleteProvinceCommandHandler(IRepository<ProvincesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult> Handle(DeleteProvinceCommand request, CancellationToken cancellationToken)
    {
        var province = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id, c => c.Cities);

        if (province is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        if (province is not null)
            if (province.Cities.Any()) throw new CustomException(SystemCommonMessage.CantRemoveBecauseThereIsDependy);
        

        _repository.Delete(province);
        await _repository.SaveChangesAsync();

        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
