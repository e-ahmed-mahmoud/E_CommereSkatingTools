using Core.Entities;

namespace Core.Specifications;

public class TypeListSpecification : BaseSpecification<Product, string>
{
    public TypeListSpecification()
    {
        this.AddSelect(p => p.Brand);
        this.AddDistinct();
    }
}
