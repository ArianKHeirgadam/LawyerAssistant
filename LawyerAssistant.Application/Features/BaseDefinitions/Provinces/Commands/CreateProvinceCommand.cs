using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Provinces.Commands;

public class CreateProvinceCommand : IRequest<SysResult>
{
    [Display(Name ="عنوان استان")]
    [Required(ErrorMessage =ValidationCommonMessages.Required)]
    public string Title { get; set; }
}
