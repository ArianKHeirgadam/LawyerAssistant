using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Cities.Commands;

public class UpdateCityCommand : IRequest<SysResult>
{
    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    public int Id { get; set; }
    [Display(Name = "عنوان شهر")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    public string Title { get; set; }
    [Display(Name = "شناسه استان")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    public int ProvinceId { get; set; }
}
