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

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPaginationEnabled { get; private set; }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) => OrderBy = orderByExpression;
    protected void AddOrderByDesc(Expression<Func<T, object>> orderByDscExpression) => OrderByDsc = orderByDscExpression;
    protected void AddDistinct() => IsDistinct = true;

    protected void AddPagination(int take, int skip)
    {
        Skip = skip;
        Take = take;
        IsPaginationEnabled = true;
    }

    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if(Criteria != null)
            query = query.Where(Criteria);

        return query;
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria) : BaseSpecification<T>(criteria)
    , ISpecification<T, TResult> where T : BaseEntity
{
    protected BaseSpecification() : this(null!) { }
    public Expression<Func<T, TResult>>? Select { get; private set; }
    protected void AddSelect(Expression<Func<T, TResult>> expression) => Select = expression;
}
