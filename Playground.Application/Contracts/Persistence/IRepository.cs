
using System.Linq.Expressions;

namespace Playground.Application.Contracts.Persistence;

public interface IRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);
    Task<int> CountAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken cancellationToken);
    Task<List<TResult>> GetAllAsNoTrackingAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query, Expression<Func<TEntity, object?>>[] includes, CancellationToken cancellationToken);
    Task<TResult?> GetAsNoTrackingAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query, Expression<Func<TEntity, object?>>[] includes, CancellationToken cancellationToken);
    Task<TResult?> GetAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query, Expression<Func<TEntity, object?>>[] includes, CancellationToken cancellationToken);
}