using System;

public class ImmutableHexComponentException : HexException
{
    public ImmutableHexComponentException() { }

    public ImmutableHexComponentException(string message) : base(message) { }
    
    public ImmutableHexComponentException(string message, Exception inner) : base(message, inner) { }
}
