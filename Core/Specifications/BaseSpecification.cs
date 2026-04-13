using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>> criteria) : ISpecification<T> where T : BaseEntity
{
    protected BaseSpecification() : this(null!) { }
    public Expression<Func<T, bool>>? Criteria => criteria;
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDsc { get; private set; }

    public bool IsDistinct { get; private set; }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) => OrderBy = orderByExpression;
    protected void AddOrderByDesc(Expression<Func<T, object>> orderByDscExpression) => OrderByDsc = orderByDscExpression;
    protected void AddDistinct() => IsDistinct = true;

}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria) : BaseSpecification<T>(criteria)
    , ISpecification<T, TResult> where T : BaseEntity
{
    protected BaseSpecification() : this(null!) { }
    public Expression<Func<T, TResult>>? Select { get; private set; }

    protected void AddSelect(Expression<Func<T, TResult>> expression) => Select = expression;
}
