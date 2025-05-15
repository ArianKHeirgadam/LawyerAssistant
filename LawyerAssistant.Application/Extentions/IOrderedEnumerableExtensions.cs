using LawyerAssistant.Application.Objects;

namespace LawyerAssistant.Application.Extentions;

public static class IOrderedEnumerableExtensions
{
    public static IEnumerable<T> ApplyPaging<T>(this IOrderedEnumerable<T> source, int pageIndex, int pageSize)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }
        return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }


    public static PagingResponse<T> ToPagedList<T>(this IOrderedEnumerable<T> source, int pageNumber, int pageSize)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }
        int totalCount = source.Count();
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        List<T> items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return new PagingResponse<T>
        {
            TotalCount = totalCount,
            PageNumber = pageNumber,
            TotalPages = totalPages,
            HasPreviousPage = pageNumber > 1,
            HasNextPage = pageNumber < totalPages,
            Data = items
        };
    }

}
