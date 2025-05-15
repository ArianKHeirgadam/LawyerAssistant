using System.Linq.Dynamic.Core;
using LawyerAssistant.Application.Extentions;
using LawyerAssistant.Application.Objects;

namespace LawyerAssistant.Application.Utilities;

public class PagedDataBuilder<TViewModel> where TViewModel : class
{
    //********************************************************************************************************************
    private IQueryable<TViewModel> ItemSoruce { get; set; }

    private PagedDataBuilderRequest _pagedDataBuilderRequest { get; }
    //********************************************************************************************************************
    public PagedDataBuilder(List<TViewModel> itemSoruce, PagedDataBuilderRequest pagedDataBuilderRequest)
    {
        ItemSoruce = itemSoruce.AsQueryable();
        _pagedDataBuilderRequest = pagedDataBuilderRequest;
    }
    //********************************************************************************************************************
    public PagedDataBuilder(PagedDataBuilderRequest dataTableRequest)
    {
        _pagedDataBuilderRequest = dataTableRequest;
    }
    //********************************************************************************************************************
    public PagedDataBuilder(IEnumerable<TViewModel> itemSoruce, PagedDataBuilderRequest pagedDataBuilderRequest)
    {
        ItemSoruce = itemSoruce.AsQueryable();
        _pagedDataBuilderRequest = pagedDataBuilderRequest;
    }
    //********************************************************************************************************************
    public PagedDataBuilder(IQueryable<TViewModel> itemSoruce, PagedDataBuilderRequest pagedDataBuilderRequest)
    {
        ItemSoruce = itemSoruce;
        _pagedDataBuilderRequest = pagedDataBuilderRequest;
    }
    //********************************************************************************************************************
    IQueryable<TViewModel> Search()
    {
        return string.IsNullOrEmpty(_pagedDataBuilderRequest.SearchValue) || string.IsNullOrEmpty(_pagedDataBuilderRequest.SearchWith) ?
                                    ItemSoruce.AsQueryable() :
                                    ItemSoruce.Where(_pagedDataBuilderRequest.SearchWith + ".Contains(@0)", _pagedDataBuilderRequest.SearchValue).AsQueryable();
    }
    //********************************************************************************************************************
    //private IQueryable<TViewModel> ColumnOrder()
    //{
    //    if (DataTableRequest.order == null || string.IsNullOrEmpty(DataTableRequest.order[0].column))
    //    {
    //        return ItemSoruce;
    //    }

    //    var index = Convert.ToInt32(DataTableRequest.order[0].column);
    //    return DataTableRequest.order[0].dir == "desc" ?
    //        ItemSoruce.OrderBy(TableColumns[index] + " descending") :
    //        ItemSoruce.OrderBy(TableColumns[index]);
    //}
    //********************************************************************************************************************
    //public void SelectColumn(List<string> columnName)
    //{
    //    TableColumns = columnName.ToList<string>();
    //    foreach (var name in TableColumns)
    //    {
    //        var temp = typeof(TViewModel).GetProperties().Single(c => c.Name == name);
    //        ListPropertyInfo.Add(temp);
    //    }
    //}
    public async Task<PagingResponse<TViewModel>> ExecuteQuery()
    {
        ItemSoruce = Search();
        var result = await ItemSoruce.ToPagedListAsync(_pagedDataBuilderRequest.PageNumber, _pagedDataBuilderRequest.PageSize);
        return result;
        //ItemSoruce = Search();
        //ItemSoruce = ColumnOrder();

        //var result = await  ItemSoruce.Skip(PagedDataBuilderRequest.Start).Take(PagedDataBuilderRequest.Length).ToListAsync();
        //var rows = new List<object>();
        //foreach (var entity in result)
        //{
        //    dynamic dynamicData = new ExpandoObject();
        //    var dynamicObject = dynamicData as IDictionary<string, object>;
        //    foreach (var propertyInfo in ListPropertyInfo)
        //    {
        //        if (propertyInfo.GetValue(entity) != null)
        //        {
        //            dynamicObject[propertyInfo.Name] = propertyInfo.GetValue(entity).ToString();
        //        }
        //        else
        //        {
        //            dynamicObject[propertyInfo.Name] = string.Empty;
        //        }
        //    }
        //    rows.Add(dynamicObject);
        //}

        //var responseTableConfig = new DataTableResponse
        //{
        //    draw = PagedDataBuilderRequest.Draw + 1,
        //    recordsTotal = recordTotal,
        //    recordsFiltered = ItemSoruce.Count(),
        //    data = rows
        //};
        //return responseTableConfig;
    }
    //********************************************************************************************************************
    public PagingResponse<TViewModel> EmptyResponse()
    {
        var responseTableConfig = new PagingResponse<TViewModel>
        {
            HasNextPage = false,
            HasPreviousPage = false,
            TotalCount = 0,
            PageNumber = 0,
            TotalPages = 0,
            Data = new List<TViewModel>()
        };
        return responseTableConfig;
    }
    //********************************************************************************************************************
}
