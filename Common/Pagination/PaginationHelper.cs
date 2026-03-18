using System;

namespace KitaTrackDemo.Api.Common.Pagination;

public static class PaginationHelper
{
    public static PagedResponse<T> Create<T>(
        IEnumerable<T> items,
        int totalRecords,
        int pageNumber,
        int pageSize)
    {
        var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        return new PagedResponse<T>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalRecords = totalRecords
        };
    }
}
