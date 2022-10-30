using System;

namespace Autojector.Abstractions
{
    /// <summary>
    /// Use this attribute on any class that implements IDecorator in order to set the order in which the decorators will be called
    /// </summary>
    public class DecoratorOrderAttribute : Attribute
    {
        /// <summary>
        /// This is the constructor for the DecoratorOrderAttribute
        /// </summary>
        /// <param name="order">The order in which the decoretors will be injected</param>
        public DecoratorOrderAttribute(int order)
        {
            Order = order;
        }

        /// <summary>
        /// The order in which the decoretors will be injected
        /// </summary>
        public int Order { get; }
    }
}

