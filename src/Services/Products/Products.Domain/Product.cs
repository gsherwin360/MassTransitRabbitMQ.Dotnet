using Core.Models;

namespace Products.Domain;

public class Product : BaseEntity
{
	public string Name { get; private set; } = string.Empty;

	public decimal Price { get; private set; }

	private Product() { }

	public static Product Create(Guid id, string name, decimal price)
	{
		if (id == Guid.Empty)
			throw new ArgumentException($"'{nameof(id)}' cannot be null.", nameof(id));

		if (string.IsNullOrEmpty(name))
			throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

		if (price < 0)
			throw new ArgumentException($"'{nameof(price)}' cannot be a non-negative value.", nameof(price));

		return new Product()
		{
			Id = id,
			Name = name,
			Price = price,
			DateCreated = DateTime.UtcNow,
			LastModified = DateTime.UtcNow
		};
	}
}