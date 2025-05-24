using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.ReActions.Commands;

public class ChangeReactionStatusCommand : IRequest<SysResult>
{
    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    public int Id { get; set; }
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "وضعیت")]
    public bool IsCompleted { get; set; }
}
