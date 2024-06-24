namespace Core.Models;

public abstract class BaseEntity
{
	public Guid Id { get; protected set; }

	public DateTime DateCreated { get; set; }

	public DateTime LastModified { get; set; }
}