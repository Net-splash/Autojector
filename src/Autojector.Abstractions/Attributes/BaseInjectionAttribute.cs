using System;

namespace Autojector.Abstractions
{
    /// <summary>
    /// This is the base attribute used by Transient,Scope and Singleton attributes
    /// It should not be used anywhere externally
    /// </summary>
    public abstract class BaseInjectionAttribute : Attribute
    {
        /// <summary>
        /// This is the constructor for the BaseAttribute
        /// It should not be used anywhere externally
        /// </summary>
        /// <param name="abstractionType"></param>
        public BaseInjectionAttribute(Type abstractionType)
        {
            AbstractionType = abstractionType;
        }

        /// <summary>
        /// It should not be used anywhere externally
        /// </summary>
        public Type AbstractionType { get; }
    }

}

