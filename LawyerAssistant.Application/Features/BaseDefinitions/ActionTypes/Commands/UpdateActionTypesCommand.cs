using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Commands;

public class UpdateActionTypesCommand : IRequest<SysResult>
{
    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    public int Id { get; set; }

    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [MaxLength(200, ErrorMessage = ValidationCommonMessages.MaxLength)]
    [Display(Name = "عنوان")]
    public string Title { get; set; }

    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "اولویت")]
    public int Priority { get; set; }
}