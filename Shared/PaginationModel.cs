namespace Shared;

public class PaginationModel<T>
{
    public int PageCount { get; set; }
    public int TotalSize { get; set; }
    public List<T> Data { get; set; } = [];

}

public class FilterModel
{
    public int PageSize { get; set; } = 30;
    public int PageNumber { get; set; } = 1;
}
