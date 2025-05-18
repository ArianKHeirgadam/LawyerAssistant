using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Complexes.Commands;

public class DeleteComplexCommand : IRequest<SysResult>
{
    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    [Display(Name = "شناسه")]
    public int Id { get; set; }
}