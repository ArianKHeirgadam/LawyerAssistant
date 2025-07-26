using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.Files.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Features.Files.Handlers.Commands;

public class DeleteFilesCommandHandler : IRequestHandler<DeleteFilesCommand, SysResult>
{
    private readonly IRepository<FilesModel> _repository;

    public DeleteFilesCommandHandler(IRepository<FilesModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult> Handle(DeleteFilesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var actions = await _repository
            .Where(x => request.Ids.Contains(x.Id))
            .ToListAsync();

            if (actions.Count != request.Ids.Count)
                throw new CustomException(SystemCommonMessage.DataWasNotFound);

            _repository.DeleteRange(actions);
            await _repository.SaveChangesAsync();

            return new SysResult
            {
                IsSuccess = true,
                Message = SystemCommonMessage.OperationDoneSuccessfully
            };

        }
        catch (Exception ex)
        {
            throw new CustomException(SystemCommonMessage.CantRemoveBecauseThereIsDependy);
        }
    }
}