using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.BaseDefinitions.Branches.Commands;

public class DeleteBranchCommand : IRequest<SysResult>
{
    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    [Display(Name = "شناسه")]
    public int Id { get; set; }
}