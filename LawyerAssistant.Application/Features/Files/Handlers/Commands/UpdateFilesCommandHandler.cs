using Application.Exceptions;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Features.Files.Commands;
using LawyerAssistant.Application.Features.Files.Queries;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Domain.Aggregates;
using MediatR;

namespace LawyerAssistant.Application.Features.Files.Handlers.Commands;

public class UpdateFilesCommandHandler : IRequestHandler<UpdateFilesCommand, SysResult<FilesDetailsDto>>
{
    private readonly IRepository<FilesModel> _repository;
    private readonly ISender _sender;
    public UpdateFilesCommandHandler(IRepository<FilesModel> repository, ISender sender)
    {
        _repository = repository;
        _sender = sender;
    }

    public async Task<SysResult<FilesDetailsDto>> Handle(UpdateFilesCommand request, CancellationToken cancellationToken)
    {
        var file = await _repository.FirstOrDefaultAsync(f => f.Id == request.Dto.Id);
        if (file == null) throw new CustomException(SystemCommonMessage.DataWasNotFound);

        file.Edit(
            request.Dto.Title,
            request.Dto.DemandId,
            request.Dto.CustomerId,
            request.Dto.LegalId,
            request.Dto.IsLegal
        );
        _repository.Update(file);
        await _repository.SaveChangesAsync();

        return await _sender.Send(new GetFilesByIdQuery() { Id = file.Id });
    }
}
