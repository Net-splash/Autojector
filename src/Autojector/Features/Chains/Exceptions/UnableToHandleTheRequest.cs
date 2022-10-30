using System;

namespace Autojector.Features.Chains.Exceptions
{
    internal class UnableToHandleTheRequestException : Exception
    {
        public UnableToHandleTheRequestException() : base("Unable to hande the request")
        {
        }
    }
}