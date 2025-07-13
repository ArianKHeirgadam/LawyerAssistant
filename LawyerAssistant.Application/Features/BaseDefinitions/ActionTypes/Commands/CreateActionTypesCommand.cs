using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.ActionTypes.Commands;
public class CreateActionTypesCommand : IRequest<SysResult<ActionDto>>
{
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [MaxLength(200, ErrorMessage = ValidationCommonMessages.MaxLength)]
    public string Title { get; set; }

    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Range(0, int.MaxValue, ErrorMessage = ValidationCommonMessages.Range)]
    public int Priority { get; set; }
/*
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    [Range(1, 168, ErrorMessage = ValidationCommonMessages.Range)] // up to 1 week
    public int RememberTime { get; set; }*/
}
