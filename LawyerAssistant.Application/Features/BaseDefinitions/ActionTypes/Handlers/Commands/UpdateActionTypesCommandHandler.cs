using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Handlers.Commands;

public class UpdateActionTypesCommandHandler : IRequestHandler<UpdateActionTypesCommand, SysResult>
{
    private readonly IRepository<ActionTypesModel> _repository;

    public UpdateActionTypesCommandHandler(IRepository<ActionTypesModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult> Handle(UpdateActionTypesCommand request, CancellationToken cancellationToken)
    {
        var action = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (action == null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        action.Edit(request.Title, request.Priority, request.RememberTime);
        await _repository.SaveChangesAsync();

        return new SysResult
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully
        };
    }
}
