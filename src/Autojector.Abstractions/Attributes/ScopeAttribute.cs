using System;

namespace Autojector.Abstractions
{
    /// <summary>
    /// This attribute should be use to mark any class as an injectable scope service.
    /// In order to be picked up by the Autojector please make sure that the SimpleInjectionByAttribute was added as a feature in the configureOption
    /// or that the method AddAutojector was called. 
    /// </summary>
    public class ScopeAttribute : BaseInjectionAttribute
    {
        /// <summary>
        /// Constructor for the ScopeAttribute
        /// </summary>
        /// <param name="abstractionType">
        /// The type of the abstraction that this class needs to be insterted to 
        /// This is the type of the service that you expect to be injected as. This can be an abstract class or an interface.
        /// Make sure that your class will also implement that interface or extend that class.
        /// </param>
        public ScopeAttribute(Type abstractionType) : base(abstractionType)
        {
        }
    }

}