using Core.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Api.Models;
using Products.Api.Models.DTO;
using Products.Domain.Interfaces;

namespace Products.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
	private readonly IProductService _productService = productService ?? throw new ArgumentNullException(nameof(productService));

	[HttpPost]
	[AllowAnonymous]
	[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Create(CreateProductModel request)
	{
		var result = await _productService.CreateProductAsync(request.Name, request.Price, request.NumberOfStocks);

		return result.IsSuccess ? CreatedAtAction(nameof(this.Create), new { Id = result.Value }, result.Value)
			: BadRequest(result.Error);
	}

	[HttpGet]
	[AllowAnonymous]
	[ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetProducts()
	{
		var result = await _productService.GetProductsAsync();

		if (result.Value is not null && result.Value.Any())
		{
			var mappedProducts = ProductDTO.ToProductDTOMapList(result.Value);
			return Ok(mappedProducts);
		}

		return NotFound();
	}
}
