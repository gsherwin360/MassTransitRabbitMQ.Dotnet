using Core.Primitives;

namespace Inventory.Domain.Interfaces;

public interface IInventoryService
{
	Task<Result<Guid>> AddInventoryItemAsync(Guid productId, string name, int numberOfStocks);
	Task<Result<IEnumerable<InventoryItem>>> GetInventoryItemsAsync();
	Task<bool> CheckInventoryItemExistsAsync(Guid productId);
}