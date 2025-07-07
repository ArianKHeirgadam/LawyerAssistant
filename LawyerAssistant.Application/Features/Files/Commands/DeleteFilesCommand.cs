using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.Files.Commands;

public class DeleteFilesCommand : IRequest<SysResult>
{
    [Required(ErrorMessage =ValidationCommonMessages.IdentifierRequired)]
    public List<int> Ids { get; set; }
}
