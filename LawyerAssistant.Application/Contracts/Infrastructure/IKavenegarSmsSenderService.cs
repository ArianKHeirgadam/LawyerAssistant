using LawyerAssistant.Application.Objects;

namespace LawyerAssistant.Application.Contracts.Infrastructure;

public interface IKavenegarSmsSenderService
{
    Task<SysResult> Send(string apiKey , string senderLine ,  List<string> MobileNumbers, string Message, long? unixDateTime);
    Task<SysResult> Send(string apiKey, string senderLine, string MobileNumber, string Message);
    Task<SysResult> SendWithTemplate(string apiKey, string MobileNumber, string Templete, string Token_1, string Token_10 = null, string Token_20 = null, string Token_2 = null, string Token_3 = null);
}
