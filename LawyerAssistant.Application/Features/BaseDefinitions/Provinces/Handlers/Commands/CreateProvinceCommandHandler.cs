using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Provinces.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Provinces.Handlers.Commands;

public class CreateProvinceCommandHandler : IRequestHandler<CreateProvinceCommand, SysResult>
{
    private readonly IRepository<ProvincesModel> _repository;
    public CreateProvinceCommandHandler(IRepository<ProvincesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult> Handle(CreateProvinceCommand request, CancellationToken cancellationToken)
    {
        var province = new ProvincesModel(request.Title);

        await _repository.AddAsync(province);
        await _repository.SaveChangesAsync();

        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
