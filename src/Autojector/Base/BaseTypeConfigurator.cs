
using Autojector.Registers.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Autojector.Base;
internal abstract class BaseTypeConfigurator : ITypeConfigurator
{
    public abstract void ConfigureServices();
}
