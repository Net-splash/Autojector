using Autojector.Public;
namespace InjectableClassesAssembly;

public interface IExternalInjectableSingleton: ISingletonInjectable<IExternalInjectableSingleton>
{
}

internal class ExternalInjectableSingleton : IExternalInjectableSingleton
{
}
