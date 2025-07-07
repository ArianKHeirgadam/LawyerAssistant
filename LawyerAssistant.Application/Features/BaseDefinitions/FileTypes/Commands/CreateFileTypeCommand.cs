using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;

public class CreateFileTypeCommand : IRequest<SysResult<GenericDTO>>
{
    [Display(Name ="عنوان")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    public string Title { get; set; }
}
