using Mapster;
using Microsoft.EntityFrameworkCore;
namespace Shared;

public static class Mapper
{
    public static TDestination MapTo<TDestination>(this object source)
    {
        return source.Adapt<TDestination>();
    }

    public static IQueryable<TDestination> MapTo<TDestination>(this IQueryable source)
    {
        return source.ProjectToType<TDestination>();
    }
    
    public static async Task<PaginationModel<TData>> ToPaginationModelAsync<TData>(this IQueryable<TData> source, FilterModel filter)
        where TData : class
    {
        var pagination = new PaginationModel<TData>
        {
            TotalSize = await source.CountAsync(),
            Data = await source.Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync()
        };

        pagination.PageCount = pagination.TotalSize > 0 ? (int)Math.Ceiling(pagination.TotalSize / (double)filter.PageSize) : 0;

        return pagination;
    }
}
