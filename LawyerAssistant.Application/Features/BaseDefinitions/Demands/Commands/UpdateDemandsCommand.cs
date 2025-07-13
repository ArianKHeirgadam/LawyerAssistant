using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;

public class UpdateDemandsCommand : IRequest<SysResult<GetDemandsDTO>>
{
    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    public int Id { get; set; }
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    public string Title { get; set; }
    [Display(Name = "نوع پرونده")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    public int FileTypeId { get; set; }
}
