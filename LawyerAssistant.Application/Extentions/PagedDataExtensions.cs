using Application.Utilities;
using LawyerAssistant.Application.Objects;
using LawyerAssistant.Application.Utilities;

namespace LawyerAssistant.Application.Extentions;

public static class PagedDataExtensions
{

    public static async Task< SysResult> GetPagedData<T>(this SysResult result, PagedDataBuilderRequest request) where T : class
    {
        if (result.Value == null)
        {
            var resultData = new PagedDataBuilder<T>(request);
            result.Value = resultData.EmptyResponse();
        }
        else
        {

            if (result.Value.GetType().ToString().StartsWith("System.Collections.Generic.List"))
            {
                var resultData = new PagedDataBuilder<T>((List<T>)result.Value, request);
                result.Value =await resultData.ExecuteQuery();
            }
            else
            {
                var resultData = new PagedDataBuilder<T>((IQueryable<T>)result.Value, request);
                result.Value = await resultData.ExecuteQuery();
            }
        }
        return result;
    }

}
