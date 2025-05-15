namespace LawyerAssistant.Application.Objects;

public class PagingRequest
{
    private int _pageNumber = 1;

    public int PageNumber
    {
        get
        {
            return _pageNumber;
        }

        set
        {
            _pageNumber = value <= 1 ? 1 : value;
        }
    }

    public int PageSize { get; set; } = 10;
}
