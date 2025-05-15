namespace LawyerAssistant.Application.Objects;

public class PagedDataBuilderRequest : PagingRequest
{
    public PagedDataBuilderRequest()
    {
    }

    public string SearchWith   { get; set; }

     public string SearchValue   { get; set; }
}
