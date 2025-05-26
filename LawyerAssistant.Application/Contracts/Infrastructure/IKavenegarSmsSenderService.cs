using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Objects;

namespace LawyerAssistant.Application.Contracts.Infrastructure;

public interface IKavenegarSmsSenderService
{
    Task<SysResult<SmsMessageDto>> Send(string Message, long? unixDateTime);
    Task<SysResult<SmsMessageDto>> Cancel(string messageid);
    Task<SysResult<SmsMessageDto>> SendSMS(string clientName, string actionTitle, string actionTime, string requestTitle, DateTime SendDate);
}
