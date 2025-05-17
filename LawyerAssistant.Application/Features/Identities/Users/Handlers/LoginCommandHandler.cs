using Application.Exceptions;
using Domain.Aggregates.Identities;
using LawyerAssistant.Application.Contracts.Persistence;
using LawyerAssistant.Application.DTOs.Identities;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Features.Identities.Users.Commands;
using LawyerAssistant.Application.Objects;
using MediatR;

namespace LawyerAssistant.Application.Features.Identities.Users.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, SysResult<UserDTO>>
{
    private readonly IRepository<UsersEntity> _repository;
    public LoginCommandHandler(IRepository<UsersEntity> repository)
    {
        _repository = repository;
    }
    public async Task<SysResult<UserDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.FirstOrDefaultAsync(c => c.UserName == request.Dto.Username);

        if (user is null) throw new CustomException(SystemCommonMessage.DataWasNotFound);


        if (user.PasswordHash != request.Dto.Password.HashMD5()) throw new CustomException(SystemCommonMessage.IdentifierIsNotValid);


        return new SysResult<UserDTO>
        {
            IsSuccess = true,
            Message = SystemCommonMessage.OperationDoneSuccessfully,
            Value = new UserDTO
            {
                UserId = user.Id,
                UserName = user.UserName,
                Token = ""
            }
        };
    }
}
