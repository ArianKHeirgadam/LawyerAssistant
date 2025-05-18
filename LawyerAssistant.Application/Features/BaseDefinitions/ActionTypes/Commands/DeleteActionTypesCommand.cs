using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Commands;

public class DeleteActionTypesCommand : IRequest<SysResult>
{
    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    public int Id { get; set; }
}