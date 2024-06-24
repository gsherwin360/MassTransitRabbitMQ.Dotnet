using Core.Primitives;
using Infrastructure.MongoDb;
using Inventory.Domain;
using Inventory.Domain.Interfaces;

namespace Inventory.Infrastructure.Implementations;

public class InventoryService : IInventoryService
{
	private readonly IMongoRepository<InventoryItem> _inventoryRepository;

    public InventoryService(IMongoRepository<InventoryItem> inventoryRepository)
    {
        _inventoryRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));
    }

    public async Task<Result<Guid>> AddInventoryItemAsync(Guid productId, string name, int numberOfStocks)
	{
        var inventoryItem = InventoryItem.Create(Guid.NewGuid(), name, numberOfStocks, productId);

        await _inventoryRepository.CreateAsync(inventoryItem);

        return Result<Guid>.Success(inventoryItem.Id);
	}

	public async Task<Result<IEnumerable<InventoryItem>>> GetInventoryItemsAsync()
	{
        var inventoryItems = await _inventoryRepository.GetAllAsync();

        return Result<IEnumerable<InventoryItem>>.Success(inventoryItems);
	}

	public async Task<bool> CheckInventoryItemExistsAsync(Guid productId)
	{
		var inventoryItem = await _inventoryRepository.GetByIdAsync(productId);
		return inventoryItem is not null;
	}
}
