using Autojector.Abstractions;

namespace InjectableClassesAssembly;

public interface IExternalInjectableSingleton: ISingletonInjectable<IExternalInjectableSingleton>
{
}

internal class ExternalInjectableSingleton : IExternalInjectableSingleton
{
}
