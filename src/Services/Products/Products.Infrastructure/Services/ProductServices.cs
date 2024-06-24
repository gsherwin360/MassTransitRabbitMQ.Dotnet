using Products.Domain.Interfaces;
using Products.Infrastructure.Implementations;

namespace Microsoft.Extensions.DependencyInjection;

public static class ProductServices
{
	public static void AddProductServices(this IServiceCollection services)
	{
		ArgumentNullException.ThrowIfNull(services);

		services.AddScoped<IProductService, ProductService>();
	}
}

