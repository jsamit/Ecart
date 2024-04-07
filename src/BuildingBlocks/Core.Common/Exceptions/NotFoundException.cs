namespace Core.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string obj,Guid key) : base($"{key} doesn't exist on {obj}") { }
}
