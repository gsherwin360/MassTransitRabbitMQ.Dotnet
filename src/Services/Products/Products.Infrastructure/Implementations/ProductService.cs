using Core.Primitives;
using Infrastructure.MongoDb;
using Integration.Contracts.Events.Products;
using MassTransit;
using Products.Domain;
using Products.Domain.Interfaces;

namespace Products.Infrastructure.Implementations;

public class ProductService : IProductService
{
	private readonly IMongoRepository<Product> _productRepository;
	private readonly IBus _bus;

    public ProductService(IMongoRepository<Product> productRepository, IBus bus)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
		_bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task<Result<Guid>> CreateProductAsync(string name, decimal price, int numberOfStocks)
	{
		var product = Product.Create(Guid.NewGuid(), name, price);

		await _productRepository.CreateAsync(product);

		// Publishes a product creation event to notify subscribers
		await _bus.Publish<IProductCreatedEvent>(new
		{
			ProductId = product.Id,         // Unique identifier of the newly created product
			ProductName = product.Name,     // Name of the product
			NumberOfStocks = numberOfStocks,// Initial stock quantity
			InVar.Id,                       // Assigns a unique message identifier
			InVar.Timestamp                 // Assigns the current UTC timestamp
		});

		return Result<Guid>.Success(product.Id);
	}

	public async Task<Result<IEnumerable<Product>>> GetProductsAsync()
	{
		var products = await _productRepository.GetAllAsync();

		return Result<IEnumerable<Product>>.Success(products);
	}

	public async Task<bool> CheckProductExistsAsync(Guid productId)
	{
		var product = await _productRepository.GetByIdAsync(productId);
		return product is not null;
	}
}
