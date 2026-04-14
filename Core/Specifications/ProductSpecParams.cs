using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications;

public class ProductSpecParams
{
    private readonly int _maxPageSize = 50;
    private int _pageSize = 5;
    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > _maxPageSize ? _maxPageSize : value;
    }

    private List<string> _brands = [];
    public List<string> Brands
    {
        get => _brands;
        set { _brands = [.. value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries))]; }
    }

    private List<string> _types = [];
    public List<string> Types
    {
        get => _types;
        set { _types = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList(); }
    }

    public string? Sort { get; set; } = string.Empty;

    public string? Search { get; set; } = string.Empty;

}
