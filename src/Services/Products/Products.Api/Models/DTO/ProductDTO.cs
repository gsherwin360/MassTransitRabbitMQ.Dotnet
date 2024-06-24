using Products.Domain;

namespace Products.Api.Models.DTO;

public class ProductDTO
{
	public Guid Id { get; private set; }
	public string Name { get; private set; } = string.Empty;
	public decimal Price { get; private set; }
	public DateTime DateCreated { get; private set; }

	public static IEnumerable<ProductDTO> ToProductDTOMapList(IEnumerable<Product>? source)
	{
		if (source is null) return Enumerable.Empty<ProductDTO>();

		return source.Select(item => new ProductDTO
		{
			Id = item.Id,
			Name = item.Name,
			Price = item.Price,
			DateCreated = item.DateCreated
		});
	}
}
