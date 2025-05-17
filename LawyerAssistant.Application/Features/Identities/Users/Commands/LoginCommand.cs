using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.Identities.Users.Commands;

public class LoginCommand : IRequest<SysResult<UserDTO>>
{
    public LoginDTO Dto { get; set; }
}
