using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> specification);
    Task<T?> GetByIdAsync(Guid id, ISpecification<T> specification);
    Task<IReadOnlyList<TResult>> GetAllAsync<TResult>(ISpecification<T, TResult> specification);
    Task<TResult?> GetByIdAsync<TResult>(Guid id, ISpecification<T, TResult> specification);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    bool IsExists(Guid id);
    Task<bool> SaveChangesAsync();

    Task<int> CountAsync(ISpecification<T> specification);

}
