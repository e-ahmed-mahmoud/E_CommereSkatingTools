using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class GenericRepository<T>(StoreDbContext dbContext) : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
    }
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }
    public void Add(T entity)
    {
        _dbContext.Add<T>(entity);
    }
    public void Update(T entity)
    {
        _dbContext.Set<T>().Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }
    public void Delete(T entity)
    {
        _dbContext.Remove<T>(entity);
    }
    public bool IsExists(Guid id)
    {
        return _dbContext.Set<T>().Any(e => e.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() > 1;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }
    public async Task<T?> GetByIdAsync(Guid id, ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TResult>> GetAllAsync<TResult>(ISpecification<T, TResult> specification) =>
        await ApplySpecification<TResult>(specification).ToListAsync();

    public async Task<TResult?> GetByIdAsync<TResult>(Guid id, ISpecification<T, TResult> specification) =>
            await ApplySpecification<TResult>(specification).FirstOrDefaultAsync();
    private IQueryable<T> ApplySpecification(ISpecification<T> spec) =>
                    SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable<T>(), spec);
    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec) =>
                    SpecificationEvaluator<T>.GetQuery<T, TResult>(_dbContext.Set<T>().AsQueryable<T>(), spec);

    public async Task<int> CountAsync(ISpecification<T> specification)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        query = specification.ApplyCriteria(query);
        return await query.CountAsync();

    }

}
