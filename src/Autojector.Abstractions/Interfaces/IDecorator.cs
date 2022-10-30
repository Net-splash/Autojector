namespace Autojector.Abstractions
{
    /// <summary>
    /// This interface should be use to mark any class as decorator for the service T
    /// In order to be picked up by the Autojector please make sure that the Decorators was added as a feature in the configureOption
    /// or that the method AddAutojector was called. 
    /// Make sure that the implementation also implements T
    /// </summary>
    /// <typeparam name="T"> The service T which will be replace by the current implementation of the class that implements this interface.</typeparam>
    public interface IDecorator<T> : IInjectable
    {
    }
}