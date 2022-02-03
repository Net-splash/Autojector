namespace Autojector.Abstractions;
/// <summary>
/// This interface should be use to mark any class as an injectable scope service.
/// In order to be picked up by the Autojector please make sure that the SimpleInjection was added as a feature in the configureOption
/// or that the method AddAutojector was called. 
/// </summary>
/// <typeparam name="T">
/// This is the type of the service that you expect to be injected as. This can be an abstract class or an interface.
/// Make sure that your class will also implement that interface or extend that class.
/// </typeparam>
public interface IScope<T> : IInjectable
{
}
