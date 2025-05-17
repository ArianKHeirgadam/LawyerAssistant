using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Provinces.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Provinces.Handlers.Commands;

public class UpdateProvinceCommandHandler : IRequestHandler<UpdateProvinceCommand, SysResult>
{
    private readonly IRepository<ProvincesModel> _repository;
    public UpdateProvinceCommandHandler(IRepository<ProvincesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult> Handle(UpdateProvinceCommand request, CancellationToken cancellationToken)
    {
        var province = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (province is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        province.Edit(request.Title);

        _repository.Update(province);
        await _repository.SaveChangesAsync();

        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully};
    }
}
