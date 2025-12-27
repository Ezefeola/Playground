using Playground.Application.Contracts.Persistence;
using Playground.Application.Extensions;
using Playground.Domain.Entities;

namespace Playground.Application.UseCases.Products;
public sealed class GetAllProducts
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllProducts(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        string? sku = null;
        sku = "ABC123";
        
        Product? product = await _unitOfWork.ProductRepository.GetAsync(query: q => q.WhereIf(!string.IsNullOrEmpty(sku), p => p.Sku == sku)
                                                                                     .Where(x => x.Id == 1)
                                                                                     .OrderByDescending(x => x.Sku),
                                                                        includes: [
                                                                            i => i.CodeBar
                                                                        ],
                                                                        cancellationToken);
    }
}