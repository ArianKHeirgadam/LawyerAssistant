using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Handlers.Commands;

public class CreateActionTypesCommandHandler : IRequestHandler<CreateActionTypesCommand, SysResult>
{
    private readonly IRepository<ActionTypesModel> _repository;

    public CreateActionTypesCommandHandler(IRepository<ActionTypesModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult> Handle(CreateActionTypesCommand request, CancellationToken cancellationToken)
    {
        var action = new ActionTypesModel(request.Title, request.Priority, request.RememberTime);
        await _repository.AddAsync(action);
        await _repository.SaveChangesAsync();
        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}

