using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.Features.Files.Commands;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;

namespace LawyerAssistant.Application.Features.Files.Handlers.Commands;

public class CreateFilesCommandHandler : IRequestHandler<CreateFilesCommand, SysResult>
{
    private readonly IRepository<FilesModel> _repository;
    public CreateFilesCommandHandler(IRepository<FilesModel> repository) => _repository = repository;
    public async Task<SysResult> Handle(CreateFilesCommand request, CancellationToken cancellationToken)
    {

        var filesModel = new FilesModel
        (
            request.Dto.Title,
            request.Dto.DemandId,
            request.Dto.FileTypeId,
            request.Dto.CustomerId,
            request.Dto.LegalId,
            request.Dto.IsLegal
        );

        await _repository.AddAsync(filesModel);
        await _repository.SaveChangesAsync();

        return new SysResult
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully
        };
    }
}