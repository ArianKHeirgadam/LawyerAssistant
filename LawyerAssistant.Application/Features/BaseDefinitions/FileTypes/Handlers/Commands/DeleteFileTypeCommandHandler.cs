using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Handlers.Commands;

public class DeleteFileTypeCommandHandler : IRequestHandler<DeleteFileTypeCommand, SysResult>
{
    private readonly IRepository<FilesTypesModel> _repository;
    public DeleteFileTypeCommandHandler(IRepository<FilesTypesModel> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult> Handle(DeleteFileTypeCommand request, CancellationToken cancellationToken)
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
