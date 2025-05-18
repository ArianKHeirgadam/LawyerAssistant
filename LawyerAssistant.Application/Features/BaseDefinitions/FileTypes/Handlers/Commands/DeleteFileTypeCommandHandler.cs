using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using MediatR;

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
        var fileType = await _repository.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (fileType is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        _repository.Delete(fileType);
        await _repository.SaveChangesAsync();

        return new SysResult() { IsSuccess = true, Message = SystemCommonMessage.OperationDoneSuccessfully };
    }
}
