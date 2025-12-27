using Playground.Application.Contracts.Persistence;
using Playground.Domain.Entities;

namespace Plaground.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext,
        IRepository<Product> productRepository,
        IRepository<CodeBar> codeBarRepository)
    {
        _dbContext = dbContext;
        ProductRepository = productRepository;
        CodeBarRepository = codeBarRepository;
    }

    public IRepository<Product> ProductRepository { get; }

    public IRepository<CodeBar> CodeBarRepository { get; }


    public async Task<int> CompleteAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}