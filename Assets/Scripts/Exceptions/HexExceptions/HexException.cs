using System;

public class HexException : Exception
{
    public HexException() { }

    public HexException(string message) : base(message) { }

	public HexException(string message, Exception inner) : base(message, inner) { }
}
