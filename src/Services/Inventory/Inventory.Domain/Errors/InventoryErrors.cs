using Core.Primitives;

namespace Inventory.Domain.Errors;

public static class InventoryErrors
{
	public static Error NotFound => new Error(
		"InventoryItemNotFound",
		"The Inventory item does not exist.");
}
