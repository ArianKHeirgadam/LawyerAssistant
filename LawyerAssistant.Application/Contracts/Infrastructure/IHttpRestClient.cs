using LawyerAssistant.Application.Objects;

namespace LawyerAssistant.Application.Contracts.Infrastructure;

public interface IHttpRestClient
{
    Task<ApiResponse<TResponseResult>> Send<TResponseResult>(ApiRequest apiRequest , Action<ApiResponse<TResponseResult>> errorHandler = null);
}