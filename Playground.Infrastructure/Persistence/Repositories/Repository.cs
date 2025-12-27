using Microsoft.EntityFrameworkCore;
using Playground.Application.Contracts.Persistence;
using System.Linq.Expressions;

namespace Plaground.Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;
    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<TResult?> GetAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query,
        Expression<Func<TEntity, object?>>[] includes,
        CancellationToken cancellationToken)
    {
        IQueryable<TEntity> finalQuery = _dbSet;
        AddIncludesIfHasAny<TResult>(includes, finalQuery);

        return await query(finalQuery).FirstOrDefaultAsync(cancellationToken);
    }
   
    public async Task<List<TResult>> GetAllAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query,
        Expression<Func<TEntity, object?>>[] includes,
        CancellationToken cancellationToken)
    {
        IQueryable<TEntity> finalQuery = _dbSet;
        AddIncludesIfHasAny<TResult>(includes, finalQuery);

        return await query(finalQuery).ToListAsync(cancellationToken);
    }

    public async Task<TResult?> GetAsNoTrackingAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query, 
        Expression<Func<TEntity, object?>>[] includes, CancellationToken cancellationToken)
    {
        IQueryable<TEntity> finalQuery = _dbSet.AsNoTracking();
        AddIncludesIfHasAny<TResult>(includes, finalQuery);

        return await query(finalQuery).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<TResult>> GetAllAsNoTrackingAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query,
        Expression<Func<TEntity, object?>>[] includes, CancellationToken cancellationToken)
    {
        IQueryable<TEntity> finalQuery = _dbSet.AsNoTracking();
        AddIncludesIfHasAny<TResult>(includes, finalQuery);

        return await query(finalQuery).ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken cancellationToken)
    {
        return await query(_dbSet).CountAsync(cancellationToken);
    }

    public IQueryable<TEntity> Query()
    {
        return _dbSet.AsQueryable();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    private static void AddIncludesIfHasAny<TResult>(Expression<Func<TEntity, object?>>[] includes, IQueryable<TEntity> finalQuery)
    {
        if (includes.Length > 0)
        {
            foreach (var include in includes)
            {
                finalQuery = finalQuery.Include(include);
            }
        }
    }
}