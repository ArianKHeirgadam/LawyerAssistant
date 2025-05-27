using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.ReActions.Commands;

public class UpdateReactionCommand : IRequest<SysResult>
{
    public UpdateReactionDTO  Dto { get; set; }
}
