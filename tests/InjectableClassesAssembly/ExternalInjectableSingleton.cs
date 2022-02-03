using Autojector.Abstractions;

namespace InjectableClassesAssembly;

public interface IExternalInjectableSingleton: ISingleton<IExternalInjectableSingleton>
{
}

internal class ExternalInjectableSingleton : IExternalInjectableSingleton
{
}
