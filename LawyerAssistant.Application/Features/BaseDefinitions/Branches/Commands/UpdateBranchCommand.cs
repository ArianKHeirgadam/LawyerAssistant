using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Branches.Commands;

public class UpdateBranchCommand : IRequest<SysResult>
{
    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    [Display(Name = "شناسه")]
    public int Id { get; set; }

    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [MaxLength(100, ErrorMessage = ValidationCommonMessages.MaxLength)]
    [Display(Name = "عنوان شعبه")]
    public string Title { get; set; }

    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    [Display(Name = "شناسه مجتمع")]
    public int ComplexId { get; set; }
}