using Inventory.Domain;

namespace Inventory.Api.Models.DTO;

public class InventoryItemDTO
{
	public Guid Id { get; private set; }
	public Guid ProductId { get; private set; }
	public string ProductName { get; private set; } = string.Empty;
	public int NumberOfStocks { get; private set; }
	public DateTime DateCreated { get; private set; }

	public static IEnumerable<InventoryItemDTO> ToInventoryItemDTOMapList(IEnumerable<InventoryItem>? source)
	{
		if (source is null) return Enumerable.Empty<InventoryItemDTO>();

		return source.Select(item => new InventoryItemDTO
		{
			Id = item.Id,
			ProductId = item.ProductId,
			ProductName = item.ProductName,
			NumberOfStocks = item.NumberOfStocks,
			DateCreated = item.DateCreated
		});
	}
}