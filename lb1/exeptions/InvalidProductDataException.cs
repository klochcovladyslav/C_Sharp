using System;
namespace pr1.exeptions;


public class InvalidProductDataException : Exception
{
    public InvalidProductDataException(string message) : base(message) { }
}