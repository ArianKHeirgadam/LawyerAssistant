using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.Files.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;
using ZstdSharp.Unsafe;

namespace LawyerAssistant.Application.Features.Files.Handlers.Commands;

public class UpdateFilesCommandHandler : IRequestHandler<UpdateFilesCommand, SysResult>
{
    private readonly IRepository<FilesModel> _repository;

    public UpdateFilesCommandHandler(IRepository<FilesModel> repository)
    {
        _repository = repository;
    }

    public async Task<SysResult> Handle(UpdateFilesCommand request, CancellationToken cancellationToken)
    {
        var file = await _repository.FirstOrDefaultAsync(f => f.Id == request.Dto.Id);
        if (file == null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        file.Edit(
            request.Dto.Title,
            request.Dto.DemandId,
            request.Dto.FileTypeId,
            request.Dto.CustomerId,
            request.Dto.LegalId,
            request.Dto.IsLegal
        );
        _repository.Update(file);
        await _repository.SaveChangesAsync();

        return new SysResult
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully
        };
    }
}
