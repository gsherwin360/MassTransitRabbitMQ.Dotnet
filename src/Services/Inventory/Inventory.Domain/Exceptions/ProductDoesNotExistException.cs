namespace Inventory.Domain.Exceptions;

public class ProductDoesNotExistException : Exception
{
	public ProductDoesNotExistException(string message) : base(message)
	{
	}

	public ProductDoesNotExistException(string message, Exception innerException) : base(message, innerException)
	{
	}

    public ProductDoesNotExistException()
    {
    }
}

