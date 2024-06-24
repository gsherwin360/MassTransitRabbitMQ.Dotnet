using Core.Messaging;

namespace Integration.Contracts.Events.Products;

public interface IProductCreatedEvent : IIntegrationEvent
{
    Guid ProductId { get; }
    string ProductName { get; }
    int NumberOfStocks { get; }
}
