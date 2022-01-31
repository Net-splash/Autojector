using System;

namespace Autojector.Registers.Chains.Exceptions;
internal class UnableToHandleTheRequestException : Exception
{
    public UnableToHandleTheRequestException() : base("Unable to hande the request")
    {
    }
}
