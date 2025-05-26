using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.Objects;

namespace LawyerAssistant.Application.Contracts.Infrastructure;

public interface IKavenegarSmsSenderService
{
    Task<SysResult<SmsMessageDto>> Send(string Message, long? unixDateTime);
    Task<SysResult<SmsMessageDto>> Cancel(string messageid);
    Task<SysResult<SmsMessageDto>> SendSMS(string clientName, string demandTitle, string actionTime, string actionTypeTitle, DateTime SendDate);
}
