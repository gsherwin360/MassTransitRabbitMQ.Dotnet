using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Implementations;

namespace Microsoft.Extensions.DependencyInjection;

public static class InventoryServices
{
	public static void AddInventoryServices(this IServiceCollection services)
	{
		ArgumentNullException.ThrowIfNull(services);
		services.AddScoped<IInventoryService, InventoryService>();
	}
}