using Core.Messaging;

namespace Integration.Contracts.Requests.Products;

public interface IValidateProductRequest : IMessage
{
	Guid ProductId { get; }
}

public interface IValidateProductResult : IMessage
{
	bool IsValid { get; }
}