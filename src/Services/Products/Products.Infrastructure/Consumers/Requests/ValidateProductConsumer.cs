using Integration.Contracts.Requests.Products;
using MassTransit;
using Products.Domain.Interfaces;

namespace Products.Infrastructure.Consumers.Requests;

public class ValidateProductConsumer : IConsumer<IValidateProductRequest>
{
	private readonly IProductService _productService;
    public ValidateProductConsumer(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    public async Task Consume(ConsumeContext<IValidateProductRequest> context)
	{
        var result = await _productService.CheckProductExistsAsync(context.Message.ProductId);

        await context.RespondAsync<IValidateProductResult>(new
		{
			IsValid = result,
			InVar.Id,
			InVar.Timestamp
		});
	}
}