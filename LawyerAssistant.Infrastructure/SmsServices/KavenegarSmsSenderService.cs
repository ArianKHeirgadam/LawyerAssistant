using Application.Enums;
using LawyerAssistant.Application.Contracts.Common;
using LawyerAssistant.Application.Contracts.Infrastructure;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Infrastructure.Objects.Kavenegar;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace Infrastructure.SmsServices
{
    public class KavenegarSmsSenderService : IKavenegarSmsSenderService, IScoped
    {
        private readonly IHttpRestClient _restClient;
        string baseUrl = "https://api.kavenegar.com/v1/";
        private readonly IOptions<AppConfig> _options;
        //******************************************************************************************************************** 
        public KavenegarSmsSenderService(IHttpRestClient restClient, IOptions<AppConfig> options)
        {
            _restClient = restClient;
            _options = options;
        }
        //******************************************************************************************************************** 
        public async Task<SysResult> Send(string Message, long? unixDateTime)
        {
            var response = await _restClient.Send<object>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                Data = SmsResquestGenarator(_options.Value.smsSenderLine, _options.Value.MobileNumber, Message, unixDateTime),
                Url = baseUrl + _options.Value.smsApiKey + "/sms/send.json",
                HttpRequestDataType = HttpRequestDataType.XFormUrlEncoded,

            });
            return ResponseToSysResultConvert(response);
        }
        public async Task<SysResult> Cancel(string messageid)
        {
            var response = await _restClient.Send<object>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                Url = baseUrl + _options.Value.smsApiKey + $"/sms/cancel.json?messageid={messageid}",
                HttpRequestDataType = HttpRequestDataType.QueryString,

            });
            return ResponseToSysResultConvert(response);
        }
        public async Task<SysResult> SendSMS(string clientName, string actionTitle, string actionTime, string requestTitle, DateTime SendDate)
        {
            string message = $"یادآوری: موکل گرامی " +
                $"{clientName}، لطفاً جهت اقدام «{actionTitle}» مربوط به خواسته " +
                $"«{requestTitle}» در ساعت {actionTime} اقدامات لازم را انجام دهید.";

            var sms = await Send(message, SendDate.ToUnixDateTime());
            return sms;
        }
        ////******************************************************************************************************************** 
        //public async Task<SysResult> Send(string apiKey, string senderLine, List<string> MobileNumbers, string Message, long? unixDateTime)
        //{
        //    var response = await _restClient.Send<object>(new ApiRequest()
        //    {
        //        ApiType = ApiType.POST,
        //        Data = SmsResquestGenarator(senderLine, MobileNumbers, Message, unixDateTime),
        //        Url = baseUrl + apiKey + "/sms/sendarray.json",
        //        HttpRequestDataType = HttpRequestDataType.XFormUrlEncoded,

        //    });
        //    return ResponseToSysResultConvert(response);
        //}
        ////******************************************************************************************************************** 
        //public async Task<SysResult> SendWithTemplate(string apiKey, string MobileNumber, string Templete, string Token_1, string Token_10 = null, string Token_20 = null, string Token_2 = null, string Token_3 = null)
        //{
        //    var response = await _restClient.Send<object>(new ApiRequest()
        //    {
        //        ApiType = ApiType.POST,
        //        Data = SmsResquestGenarator(MobileNumber, Templete, Token_1, Token_10, Token_20, Token_2, Token_3),
        //        Url = baseUrl + apiKey + "/verify/lookup.json"+$"?{SmsResquestGenaratorString(MobileNumber, Templete, Token_1, Token_10, Token_20, Token_2, Token_3)}",
        //        HttpRequestDataType = HttpRequestDataType.XFormUrlEncoded,

        //    });
        //    return ResponseToSysResultConvert(response);
        //}
        //******************************************************************************************************************** 
        KavenegarRequest SmsResquestGenarator(string senderLine, string MobileNumber, string Message, long? unixDateTime)
        {
            return new KavenegarRequest()
            {
                message = Message,
                receptor = MobileNumber,
                sender = senderLine,
                unixDateTime = unixDateTime
            };
        }
        //******************************************************************************************************************** 
        //KavenegarRequest SmsResquestGenarator(string senderLine, List<string> MobileNumbers, string Message, long? unixDateTime)
        //{
        //    var obj = new KavenegarGroupSmsObject() { message = new List<string>(), sender = new List<string>(), receptor = new List<string>() };
        //    foreach (var mobileNumber in MobileNumbers)
        //    {
        //        obj.receptor.Add(mobileNumber);
        //        obj.sender.Add(senderLine);
        //        obj.message.Add(Message);
        //    }
        //    return new KavenegarRequest()
        //    {
        //        receptor = JsonConvert.SerializeObject(obj.receptor),
        //        sender = JsonConvert.SerializeObject(obj.sender),
        //        message = JsonConvert.SerializeObject(obj.message),
        //    };
        //}
        ////******************************************************************************************************************** 
        //KavenegarTemplateBaseRequest SmsResquestGenarator(string MobileNumber, string Templete, string Token_1, string Token_10, string Token_20, string Token_2, string Token_3)
        //{
        //    var request = new KavenegarTemplateBaseRequest
        //    {
        //        receptor = MobileNumber,
        //        template = Templete
        //    };

        //    if (!string.IsNullOrEmpty(Token_1))
        //        request.token1 = Token_1;

        //    if (!string.IsNullOrEmpty(Token_10))
        //        request.token10 = Token_10;

        //    if (!string.IsNullOrEmpty(Token_20))
        //        request.token20 = Token_20;

        //    if (!string.IsNullOrEmpty(Token_2))
        //        request.token2 = Token_2;

        //    if (!string.IsNullOrEmpty(Token_3))
        //        request.token3 = Token_3;

        //    return request;
        //}
        string SmsResquestGenaratorString(string MobileNumber, string Templete, string Token_1, string Token_10, string Token_20, string Token_2, string Token_3)
        {
            var str = $"receptor={MobileNumber}&token={Token_1}&template={Templete}";
            if (!string.IsNullOrEmpty(Token_10))
                str += $"&token10={Token_10}";
            if (!string.IsNullOrEmpty(Token_20))
                str += $"&token20={Token_20}";
            if (!string.IsNullOrEmpty(Token_2))
                str += $"&token2={Token_2}";
            if (!string.IsNullOrEmpty(Token_3))
                str += $"&token3={Token_3}";
            return str;
        }
        //********************************************************************************************************************  
        SysResult ResponseToSysResultConvert(ApiResponse<object> response)
        {
            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new Exception("خطا در ارسال پیامک");
            return new SysResult()
            {
                IsSuccess = true,
                Message = "پیامک با موفقیت ارسال گردید",
                Value = response.Content
            };
        }

    }
}
