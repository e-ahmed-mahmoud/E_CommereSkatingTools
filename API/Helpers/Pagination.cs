using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers;

public class Pagination<T>(IReadOnlyList<T> items, int pageSize, int pageNumber, int count)
{
    public IReadOnlyList<T> Items { get; set; } = items;
    public int PageNumber { get; set; } = pageNumber;
    public int Count { get; set; } = count;
    public int PageSize { get; set; } = pageSize;
}
