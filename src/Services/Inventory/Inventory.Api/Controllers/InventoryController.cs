using Inventory.Api.Models.DTO;
using Inventory.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController(IInventoryService inventoryService) : ControllerBase
{
	private readonly IInventoryService _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));

	[HttpGet]
	[AllowAnonymous]
	[ProducesResponseType(typeof(IEnumerable<InventoryItemDTO>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetInventoryItems()
	{
		var result = await _inventoryService.GetInventoryItemsAsync();

		if (result.Value is not null && result.Value.Any())
		{
			return Ok(InventoryItemDTO.ToInventoryItemDTOMapList(result.Value));
		}

		return NotFound();
	}
}