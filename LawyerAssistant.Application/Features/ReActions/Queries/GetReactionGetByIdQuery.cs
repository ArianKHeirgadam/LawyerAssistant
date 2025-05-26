using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.ReActions.Queries;

public class GetReactionGetByIdQuery : IRequest<SysResult<ReactionGetDTO>>
{
    [Required(ErrorMessage = ValidationCommonMessages.IdentifierRequired)]
    public int Id { get; set; }
}
