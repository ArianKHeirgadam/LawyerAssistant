using LawyerAssistant.Application.Objects;
using Microsoft.EntityFrameworkCore;

namespace LawyerAssistant.Application.Extentions;

public static class IOrderedQueryableExtensions
{
    public static IQueryable<T> ApplyPaging<T>(this IOrderedQueryable<T> source, int pageIndex, int pageSize)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        } 
        return  source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }

    public static async Task<PagingResponse<T>> ToPagedListAsync<T>(this IOrderedQueryable<T> source, int pageNumber, int pageSize)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }
        int totalCount = await source.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        List<T> items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
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
