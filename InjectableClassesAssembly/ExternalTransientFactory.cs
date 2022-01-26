
using Autojector.Abstractions;

namespace InjectableClassesAssembly;

public interface IExternalTransientService { }
public class ExternalTransientService: IExternalTransientService { }

public interface IExternalTransientFactory : ITransientFactoryInjectable<IExternalTransientService>
{
}

internal class ExternalTransientFactory : IExternalTransientFactory
{
    public IExternalTransientService GetService() => new ExternalTransientService();
}