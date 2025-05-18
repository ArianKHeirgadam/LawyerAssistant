using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Handlers.Commands;

public class DeleteActionTypesCommandHandler : IRequestHandler<DeleteActionTypesCommand, SysResult>
{
    private readonly IRepository<ActionTypesModel> _repository;

    public DeleteActionTypesCommandHandler(IRepository<ActionTypesModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult> Handle(DeleteActionTypesCommand request, CancellationToken cancellationToken)
    {
        var action = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (action == null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        _repository.Delete(action);
        await _repository.SaveChangesAsync();

        return new SysResult
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully
        };
    }
}
