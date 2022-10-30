using System;

namespace Autojector.Abstractions
{
    /// <summary>
    /// Use this attribute on any class that implements IChainLInk\f[TRequest,TResponse\f] in order to set the order in which the ChainLinks will be called
    /// </summary>
    public class ChainLinkOrderAttribute : Attribute
    {

        /// <summary>
        /// The constructor for the order attribute.
        /// </summary>
        /// <param name="order">The order in which the current chain link will be executed</param>
        public ChainLinkOrderAttribute(int order)
        {
            Order = order;
        }

        /// <summary>
        /// The order in which the current chain link will be executed
        /// </summary>
        public int Order { get; }
    }

}