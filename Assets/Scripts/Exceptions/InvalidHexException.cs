﻿using System;

public class InvalidHexException : Exception
{
    public InvalidHexException() { }

    public InvalidHexException(string message) : base(message) { }

	public InvalidHexException(string message, Exception inner) : base(message, inner) { }
}
