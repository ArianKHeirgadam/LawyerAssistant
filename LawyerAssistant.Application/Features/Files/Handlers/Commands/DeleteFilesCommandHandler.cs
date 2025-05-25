using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.Files.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;

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
        var file = await _repository.FirstOrDefaultAsync(f => f.Id == request.Id);

        if (file == null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        _repository.Delete(file);
        await _repository.SaveChangesAsync();

        return new SysResult
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully
        };
    }
}