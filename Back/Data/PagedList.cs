using s10.Back.DTO;
using System.ComponentModel.DataAnnotations;

namespace s10.Back.Data;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public bool HasPrevious => (CurrentPage > 1);
    public bool HasNext => (CurrentPage < TotalPages);

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)PageSize);
        AddRange(items);
    }

    public static PagedList<T> Create(IQueryable<T> source, int pageNumber = 1, int pageSize = 10)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}

public static class PagedListExtensions
{
    public static PagedListResponse<TResult> ToPagedListResponse<TResult, TOrigin>(this PagedList<TOrigin> data,
        Func<PagedList<TOrigin>, List<TResult>> mapper, string url) where TResult : class
    {
        //TODO will require a mapper
        var pagedResponse = new PagedListResponse<TResult>();
        pagedResponse.TotalPages = data.TotalPages;
        pagedResponse.CurrentPage = data.CurrentPage;
        pagedResponse.PageSize = data.PageSize;
        pagedResponse.Next = data.HasNext ? $"{url}?" +
        $"{nameof(PagedListRequestParams.PageNumber)}={data.CurrentPage + 1}" +
               $"&{nameof(PagedListRequestParams.PageSize)}={data.PageSize}" : "";

        pagedResponse.Prev = (data.Count > 0 && data.HasPrevious) ? $"{url}?" +
        $"{nameof(PagedListRequestParams.PageNumber)}={data.CurrentPage - 1}" +
            $"&{nameof(PagedListRequestParams.PageSize)}={data.PageSize}" : "";


        pagedResponse.Data = mapper(data);
        return pagedResponse;
    }

}
public class PagedListResponse<T> where T : class
{
    public string? Next { get; set; }
    public string? Prev { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int TotalItems { get; set; }
    public IList<T>? Data { get; set; } // cuidado, le agregué el nullable

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data">PagedList<typeparamref name="T"/></param>
    /// <param name="url">The base url for next and prev </param>
    public PagedListResponse(PagedList<T> data, string url)
    {
        PageSize = data.PageSize;
        TotalPages = data.TotalPages;
        TotalItems = data.TotalCount;
        CurrentPage = data.CurrentPage;
        TotalPages = data.TotalPages;
        Data = data;

        Next = data.HasNext ? $"{url}?" +
               $"{nameof(PagedListRequestParams.PageNumber)}={CurrentPage + 1}" +
               $"&{nameof(PagedListRequestParams.PageSize)}={PageSize}" : "";

        Prev = (data.Count > 0 && data.HasPrevious) ? $"{url}?" +
            $"{nameof(PagedListRequestParams.PageNumber)}={CurrentPage - 1}" +
            $"&{nameof(PagedListRequestParams.PageSize)}={PageSize}" : "";
    }
    public PagedListResponse() { }
}


public class PagedListRequestParams
{
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;
    [Range(1, 1000)]
    public int PageSize { get; set; } = 10;
}