using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications;

public interface ISpecification<T> where T : BaseEntity
{
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDsc { get; }
    bool IsDistinct { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPaginationEnabled { get; }
    IQueryable<T> ApplyCriteria(IQueryable<T> query);

}

public interface ISpecification<T, TResult> : ISpecification<T> where T : BaseEntity
{
    Expression<Func<T, TResult>>? Select { get; }
}
