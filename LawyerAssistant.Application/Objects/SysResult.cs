namespace LawyerAssistant.Application.Objects;

public class SysResult
{
    public bool IsSuccess { get; set; }
    public object Value { get; set; }
    public string Message { get; set; }
}
//===============================================================
public class SysResult<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T Value { get; set; }
}
//===============================================================