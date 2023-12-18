using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _appDbContext;

    public ProductRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.Products.ToArrayAsync(cancellationToken);
    }

    public async Task DeleteProductByGuidAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
        if (product != null)
        {
            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
        var currentProduct = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == product.Id, cancellationToken);
        if (currentProduct != null)
        {
            currentProduct.Name = product.Name;
            _appDbContext.Products.Update(currentProduct);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<Guid> InsertProductAsync(Product product, CancellationToken cancellationToken)
    {
        await _appDbContext.Products.AddAsync(product,cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return await Task.FromResult(product.Id);
    }
}