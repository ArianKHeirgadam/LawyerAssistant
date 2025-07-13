using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;

public class CreateDemandsCommand : IRequest<SysResult<GetDemandsDTO>>
{
    [Display(Name ="عنوان")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    public string Title { get; set; }
    [Display(Name = "نوع پرونده")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    public int FileTypeId { get; set; }
}
