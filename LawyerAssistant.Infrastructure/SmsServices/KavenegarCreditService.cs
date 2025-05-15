using Application.Enums;
using LawyerAssistant.Application.Contracts.Common;
using LawyerAssistant.Application.Contracts.Infrastructure;
using LawyerAssistant.Application.DTOs;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Infrastructure.Objects.Kavenegar;
using Microsoft.Extensions.Options;


namespace Infrastructure.SmsServices
{
    public class KavenegarCreditService : IScoped, IKavenegarCreditService
    {
        IOptions<AppConfig> _appConfigOptions;
        IHttpRestClient _restClient;
        string baseUrl = "https://api.kavenegar.com";
        //******************************************************************************************************************** 
        public KavenegarCreditService(IOptions<AppConfig> appConfigOptions, IHttpRestClient restClient)
        {
            _appConfigOptions = appConfigOptions;
            _restClient = restClient;
        }
        //******************************************************************************************************************** 
        public async Task<SmsCreditInquiryDTOModel> Inquiry(string apiKey)
        {
            var response = await _restClient.Send<KavenegarCreditObject>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                Url = baseUrl + $"/v1/{apiKey}/account/info.json",
                Data = null,
                HttpRequestDataType = HttpRequestDataType.None,

            }, errorHandler: (apiResponse) =>
            {
                throw new Exception("خطا در استعلام اعتبار پیامک");
            });
            return ResponseToSystemDTOModelConvert(response);
        }
        //******************************************************************************************************************** 
        SmsCreditInquiryDTOModel ResponseToSystemDTOModelConvert(ApiResponse<KavenegarCreditObject> response)
        {
            var model = new SmsCreditInquiryDTOModel()
            {
                Credit = response.Response.entries.remaincredit,
            };
            return model;
        }
    }
}
