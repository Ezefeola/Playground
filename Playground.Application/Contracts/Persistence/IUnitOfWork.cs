using Playground.Domain.Entities;

namespace Playground.Application.Contracts.Persistence;
public interface IUnitOfWork
{
    IRepository<Product> ProductRepository { get; }
    IRepository<CodeBar> CodeBarRepository { get; }
}