using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Commands;

public class CreateComplexCommand : IRequest<SysResult>
{
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Display(Name = "عنوان")]
    public string Title { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = ValidationCommonMessages.GreaterThan + " 0")]
    [Display(Name = "شناسه شهر")]
    public int CityId { get; set; }
}