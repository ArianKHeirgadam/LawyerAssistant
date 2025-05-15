using Application.Enums;
using LawyerAssistant.Application.Contracts.Common;
using LawyerAssistant.Application.Contracts.Infrastructure;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Objects;
using Newtonsoft.Json;
using RestSharp;

namespace Infrastructure.RestClients
{
    public class HttpRestClient : IHttpRestClient, IScoped
    {
        public async Task<ApiResponse<TResponseResult>> Send<TResponseResult>(ApiRequest apiRequest, Action<ApiResponse<TResponseResult>> errorHandler = null)
        {
            using (var client = new RestClient(apiRequest.Url))
            {
                var request = new RestRequest(string.Empty, ConvertApiTypeToMethod(apiRequest.ApiType));

                foreach (var item in apiRequest.Cookies)
                    request.AddCookie(item.Key, item.Value, "/", new Uri(apiRequest.Url).Host);

                foreach (var item in apiRequest.Headers)
                    request.AddHeader(item.Key, item.Value);

                if (apiRequest.HttpRequestDataType == HttpRequestDataType.XFormUrlEncoded)
                {
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                    request.AddParameter("application/x-www-form-urlencoded", await ToFormUrlEncodedContent(apiRequest.Data), ParameterType.RequestBody);
                }
                if (apiRequest.HttpRequestDataType == HttpRequestDataType.Json)
                {
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("application/json", JsonConvert.SerializeObject(apiRequest.Data), ParameterType.RequestBody);
                }

                if (apiRequest.HttpRequestDataType == HttpRequestDataType.QueryString)
                    foreach (var queryParam in apiRequest.Data.ConvertToKeyValuePairs(ignoreNullOrWhiteSpaceString: true))
                        request.AddQueryParameter(queryParam.Key, queryParam.Value);

                var response = await client.ExecuteAsync(request);
                var apiResponse = new ApiResponse<TResponseResult>()
                {
                    Content = response.Content,
                    Headers = ConvertResponseHeadersToList(response.Headers),
                    HttpStatusCode = response.StatusCode,
                };
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    apiResponse.Response = JsonConvert.DeserializeObject<TResponseResult>(response.Content);
                else
                    errorHandler?.Invoke(apiResponse);
                return apiResponse;
            }
        }
        //==============================================================
        Method ConvertApiTypeToMethod(ApiType apiType)
        {
            Method method = Method.Get;
            switch (apiType)
            {
                case ApiType.DELETE:
                    method = Method.Delete;
                    break;
                case ApiType.GET:
                    method = Method.Get;
                    break;
                case ApiType.POST:
                    method = Method.Post;
                    break;
                case ApiType.PUT:
                    method = Method.Put;
                    break;
            }
            return method;
        }
        //==============================================================
        List<KeyValuePair<string, string>> ConvertResponseHeadersToList(IReadOnlyCollection<HeaderParameter> headers)
        {
            var headerList = new List<KeyValuePair<string, string>>();
            foreach (var header in headers.Select(header => new KeyValuePair<string, string>(header.Name.ToString(), header.Value.ToString())).ToList())
            {
                headerList.Add(new KeyValuePair<string, string>(header.Key, string.Join(",", header.Value)));
            }
            return headerList;
        }
        //==============================================================
        async Task<string> ToFormUrlEncodedContent(object data)
        {
            var keyValues = data.ConvertToKeyValuePairs(ignoreNullOrWhiteSpaceString: true);
            var content = new FormUrlEncodedContent(keyValues);
            return await content.ReadAsStringAsync();
        }

        //==============================================================
    }
}
