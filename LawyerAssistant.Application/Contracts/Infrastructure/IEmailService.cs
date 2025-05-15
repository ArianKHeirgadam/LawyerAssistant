using LawyerAssistant.Application.Objects;

namespace LawyerAssistant.Application.Contracts.Infrastructure;

public interface IEmailService
{
    Task<SysResult> Send(EmailSettingObject _setting);
}