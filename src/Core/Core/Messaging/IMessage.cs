namespace Core.Messaging;

/// <summary>
/// Represents a generic message with common properties that are applicable to all messages in the system.
/// </summary>
public interface IMessage
{
	public Guid Id { get; }

	public DateTime Timestamp { get; }
}