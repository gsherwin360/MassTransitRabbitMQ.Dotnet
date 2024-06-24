using Integration.Contracts.Events.Products;
using Integration.Contracts.Requests.Products;
using Inventory.Domain.Exceptions;
using Inventory.Domain.Interfaces;
using MassTransit;

namespace Inventory.Infrastructure.Consumers.Events;

public class ProductCreatedEventConsumer : IConsumer<IProductCreatedEvent>
{
	private readonly IInventoryService _inventoryService;
	private readonly IRequestClient<IValidateProductRequest> _requestClient;

    public ProductCreatedEventConsumer(IInventoryService inventoryService, IRequestClient<IValidateProductRequest> requestClient)
    {
		_inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
		_requestClient = requestClient ?? throw new ArgumentNullException(nameof(requestClient));
	}

    public async Task Consume(ConsumeContext<IProductCreatedEvent> context)
	{
		var productCreatedEvent = context.Message;

		// Validate Product existence
		var validateProductResponse = await _requestClient.GetResponse<IValidateProductResult>(new
		{	
			ProductId = productCreatedEvent.ProductId,
			InVar.Id,
			InVar.Timestamp
		});

		// This exception does not terminate the application; instead, it allows MassTransit to handle retries
		// based on the configured retry policy.
		if (!validateProductResponse.Message.IsValid)
		{
			throw new ProductDoesNotExistException(nameof(productCreatedEvent.ProductId));
		}

		// Check if inventory item already exists (idempotency check)
		var inventoryItemExists = await _inventoryService.CheckInventoryItemExistsAsync(productCreatedEvent.ProductId);
		if (inventoryItemExists)
		{
			// If inventory item exists, do nothing (idempotency)
			return;
		}

		// Add Inventory Item if it doesn't already exist
		await _inventoryService.AddInventoryItemAsync(
			productCreatedEvent.ProductId,
			productCreatedEvent.ProductName,
			productCreatedEvent.NumberOfStocks);
	}
}
