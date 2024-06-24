using Core.Models;

namespace Inventory.Domain;

// manages the stock levels of products and ensures that product information is up-to-date.
public class InventoryItem : BaseEntity
{
	public Guid ProductId { get; private set; }
	public string ProductName { get; private set; } = string.Empty;
	public int NumberOfStocks { get; private set; }

	private InventoryItem() { }

	public static InventoryItem Create(Guid id, string productName, int numberOfStocks, Guid productId)
	{
		if (id == Guid.Empty)
			throw new ArgumentException($"'{nameof(id)}' cannot be null.", nameof(id));

		if (string.IsNullOrEmpty(productName))
			throw new ArgumentException($"'{nameof(productName)}' cannot be null or empty.", nameof(productName));

		if (numberOfStocks < 0)
			throw new ArgumentException($"'{nameof(numberOfStocks)}' cannot be a negative value.", nameof(numberOfStocks));

		if (productId == Guid.Empty)
			throw new ArgumentException($"'{nameof(productId)}' cannot be null.", nameof(productId));

		return new InventoryItem
		{
			Id = id,
			ProductName = productName,
			NumberOfStocks = numberOfStocks,
			ProductId = productId,
			DateCreated = DateTime.UtcNow,
			LastModified = DateTime.UtcNow
		};
	}
}