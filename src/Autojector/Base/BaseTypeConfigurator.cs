
using Autojector.Registers.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Autojector.Base;
internal abstract class BaseTypeConfigurator : ITypeConfigurator
{
    protected BaseTypeConfigurator(IServiceCollection serviceCollection)
    {
        Services = serviceCollection;
    }

    protected IServiceCollection Services { get; }

    public abstract void ConfigureServices();
}
