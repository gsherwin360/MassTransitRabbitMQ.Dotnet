namespace Core.Messaging;

/// <summary>
/// Represents an integration event that is published to notify other services that something has happened. 
/// </summary>
public interface IIntegrationEvent : IMessage
{
}
