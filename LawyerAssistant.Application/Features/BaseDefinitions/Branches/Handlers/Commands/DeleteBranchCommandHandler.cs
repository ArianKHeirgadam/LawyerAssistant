using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Branches.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Branches.Handlers.Commands;


public class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand, SysResult>
{
    private readonly IRepository<BranchesModel> _repository;

    public DeleteBranchCommandHandler(IRepository<BranchesModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await _repository.FirstOrDefaultAsync(b => b.Id == request.Id);

        if (branch is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        _repository.Delete(branch);
        await _repository.SaveChangesAsync();

        return new SysResult
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully
        };
    }
}