using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.Identities.Legals.Commands;

public class DeleteLegalCommand : IRequest<SysResult>
{
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    public List<int> Ids { get; set; }
}