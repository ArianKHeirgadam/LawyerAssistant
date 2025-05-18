using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Queries;

public class GetFileTypeByIdQuery : IRequest<SysResult<GenericDTO>>
{
    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    public int Id { get; set; }
}
