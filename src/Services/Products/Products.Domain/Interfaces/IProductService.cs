using Core.Primitives;

namespace Products.Domain.Interfaces;

public interface IProductService
{
	Task<Result<Guid>> CreateProductAsync(string name, decimal price, int numberOfStocks);

	Task<Result<IEnumerable<Product>>> GetProductsAsync();

	Task<bool> CheckProductExistsAsync(Guid productId);
}