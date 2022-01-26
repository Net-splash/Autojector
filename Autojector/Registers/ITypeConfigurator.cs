using Microsoft.Extensions.DependencyInjection;

namespace Autojector.Registers;
internal interface ITypeConfigurator
{
    IServiceCollection ConfigureServices(IServiceCollection services);
}
