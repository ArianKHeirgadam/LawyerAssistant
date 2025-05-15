namespace LawyerAssistant.Application.Objects;

public class PagingResponse<T>
{
    //=============================================================================
    /// <summary>
    /// 
    /// </summary>
    public int TotalCount { get; set; }
    //=============================================================================
    /// <summary>
    /// 
    /// </summary>
    public int PageNumber { get; set; }
    //=============================================================================
    /// <summary>
    /// 
    /// </summary>
    public int TotalPages { get; set; }
    //=============================================================================
    /// <summary>
    /// 
    /// </summary>
    public bool HasPreviousPage { get; set; }
    //=============================================================================
    /// <summary>
    /// 
    /// </summary>
    public bool HasNextPage { get; set; }
    //=============================================================================
    /// <summary>
    /// 
    /// </summary>
    public List<T> Data { get; set; }
    //=============================================================================
}
