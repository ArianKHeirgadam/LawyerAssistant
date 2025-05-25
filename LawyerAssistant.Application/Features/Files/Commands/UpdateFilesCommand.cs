using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.Files.Commands;

public class UpdateFilesCommand : IRequest<SysResult>
{
    public FilesUpdateDto Dto { get; set; }
}
