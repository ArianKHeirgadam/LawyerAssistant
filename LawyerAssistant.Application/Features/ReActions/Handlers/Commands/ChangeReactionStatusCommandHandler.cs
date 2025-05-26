using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.ReActions.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;

namespace LawyerAssistant.Application.Features.ReActions.Handlers.Commands;

public class ChangeReactionStatusCommandHandler : IRequestHandler<ChangeReactionStatusCommand, SysResult>
{
    private readonly IRepository<ReActionModel> _repository;

    public ChangeReactionStatusCommandHandler(IRepository<ReActionModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult> Handle(ChangeReactionStatusCommand request, CancellationToken cancellationToken)
    {
        var reaction = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (reaction is null) throw new CustomException(SystemCommonMessage.ReactionIsNotFound);

        reaction.IsComplete(request.IsCompleted);

        _repository.Update(reaction);
        await _repository.SaveChangesAsync();

        return new SysResult { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
