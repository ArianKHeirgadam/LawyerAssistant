using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.Files.Commands;

public class CreateFilesCommand : IRequest<SysResult<FilesDetailsDto>>
{
    public CreateFileDto Dto { get; set; }
}
