using System;

namespace Autojector.Abstractions
{
    /// <summary>
    /// This interface should be use to mark any class as decorator for the service T
    /// In order to be picked up by the Autojector please make sure that the Decorators was added as a feature in the configureOption
    /// or that the method AddAutojector was called. 
    /// Make sure that the implementation also implements T
    /// </summary>
    public class DecoratorAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        /// <param name="decoratedType">
        /// The decoratedType which will be replace by the current implementation of the class that implements this interface.
        /// </param>
        public DecoratorAttribute(Type decoratedType)
        {
            DecoratedType = decoratedType;
        }

        /// <summary>
        /// The type that you are decorating from you class
        /// </summary>
        public Type DecoratedType { get; }
    }
}