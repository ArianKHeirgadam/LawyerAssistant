using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Features.Files.Commands;
using LawyerAssistant.Application.Features.Files.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;

namespace LawyerAssistant.Application.Features.Files.Handlers.Commands;

public class CreateFilesCommandHandler : IRequestHandler<CreateFilesCommand, SysResult<FilesDetailsDto>>
{
    private readonly IRepository<FilesModel> _repository;
    private readonly ISender _sender;
    public CreateFilesCommandHandler(IRepository<FilesModel> repository, ISender sender)
    {
        _repository = repository;
        _sender = sender;
    }

    public async Task<SysResult<FilesDetailsDto>> Handle(CreateFilesCommand request, CancellationToken cancellationToken)
    {

        var filesModel = new FilesModel
        (
            request.Dto.Title,
            request.Dto.DemandId,
            request.Dto.CustomerId,
            request.Dto.LegalId,
            request.Dto.IsLegal
        );

        await _repository.AddAsync(filesModel);
        await _repository.SaveChangesAsync();

        return await _sender.Send(new GetFilesByIdQuery() { Id = filesModel.Id });
    }
}