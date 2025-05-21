using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.ReActions.Commands;

public class CreateReActionCommand : IRequest<SysResult>
{
    public CreateReActionDto Dto { get; set; }
}
