using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.Files.Queries;

public class GetFilesByIdQuery : IRequest<SysResult<FilesDetailsDto>>
{
    [Required(ErrorMessage =ValidationCommonMessages.IdentifierRequired)]
    public int Id { get; set; }
}
