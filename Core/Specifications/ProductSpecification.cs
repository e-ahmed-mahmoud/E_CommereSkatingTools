using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DTOs.Product;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product, ProductRespons>
{
    public ProductSpecification(ProductSpecParams specParams) : base(x =>
        (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search.ToLower())) &&
        (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.Brand)) &&
        (specParams.Types.Count == 0 || specParams.Types.Contains(x.Type))
        )
    {

        AddPagination(specParams.PageSize, specParams.PageSize * (specParams.PageNumber - 1));

        AddSelect(p => new ProductRespons(p.Name, p.Description, p.Price, p.PictureUrl, p.Type, p.Brand, p.QuantityInStock));

        switch (specParams.Sort)
        {
            case "priceAsc":
                AddOrderBy(p => p.Price);
                break;
            case "priceDesc":
                AddOrderByDesc(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
    }


}
