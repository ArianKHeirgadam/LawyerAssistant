using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;

public class CreateCityCommand : IRequest<SysResult>
{
    [Display(Name ="عنوان")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    public string Title { get; set; }
    [Display(Name = "شناسه استان")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    public int ProvinceId { get; set; }
}
