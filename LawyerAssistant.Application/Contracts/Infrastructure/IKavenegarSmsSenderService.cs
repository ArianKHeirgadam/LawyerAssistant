using LawyerAssistant.Application.Objects;

namespace LawyerAssistant.Application.Contracts.Infrastructure;

public interface IKavenegarSmsSenderService
{
    Task<SysResult> Send(string Message, long? unixDateTime);
    Task<SysResult> Cancel(string messageid);
    Task<SysResult> SendSMS(string clientName, string actionTitle, string actionTime, string requestTitle, DateTime SendDate);
}
