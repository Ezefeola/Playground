namespace Playground.Application.Contracts.Persistence;
public interface ISpecification<TEntity>
{
    IQueryable<TEntity> Apply(IQueryable<TEntity> query);
}