using System.Net;

namespace LawyerAssistant.Application.Objects;

public class ApiResponse<TResponseResult>  
{
    //==============================================================
    /// <summary>
    /// 
    /// </summary>
    public List<KeyValuePair<string, string>> Headers { get; set; } = new List<KeyValuePair<string, string>>(); 
    //==============================================================
    /// <summary>
    /// 
    /// </summary>
    public HttpStatusCode? HttpStatusCode { get; set; }
    //==============================================================
    /// <summary>
    /// 
    /// </summary>
    public string Content { get; set; }
    //==============================================================
    /// <summary>
    /// 
    /// </summary>
    public TResponseResult  Response { get; set; }
    //==============================================================
}
