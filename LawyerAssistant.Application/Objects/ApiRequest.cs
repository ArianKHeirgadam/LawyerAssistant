using Application.Enums;

namespace LawyerAssistant.Application.Objects;

public class ApiRequest
{
    /// <summary>
    /// 
    /// </summary>
    public int? Timeout { get; set; }
    //========================================
    /// <summary>
    /// 
    /// </summary>
    public ApiType ApiType { get; set; }
    //========================================
    /// <summary>
    /// 
    /// </summary>
    public  List<KeyValuePair<string, string>> Headers { get; set; } = new List<KeyValuePair<string, string>>();
    //========================================
    /// <summary>
    /// 
    /// </summary>
    public List<KeyValuePair<string, string>> Cookies { get; set; } = new List<KeyValuePair<string, string>>();

    /// <summary>
    /// 
    /// </summary>
    public string  Url { get; set; }
    //========================================
    /// <summary>
    /// 
    /// </summary>
    public object  Data { get; set; }
    //========================================
    /// <summary>
    /// 
    /// </summary>
  public  HttpRequestDataType HttpRequestDataType { get; set; }
}
