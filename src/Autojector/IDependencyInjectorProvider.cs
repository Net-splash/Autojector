using Autojector.DependencyInjector.Public;
using Autojector.Registers.Factories;
using Autojector.Registers.SimpleInjection;

namespace Autojector;
internal interface IDependencyInjectorProvider
{
    ISimpleRegisterStrategyFactory GetSimpleRegisterStrategyFactory();
    IFactoryRegisterStrategyFactory GetFactoryRegisterStrategyFactory();
    IChainRegisterStrategy GetChainRegisterStrategy();
    IAsyncFactoryRegisterStrategyFactory GetAsyncFactoryRegisterStrategyFactory();
    IDecoratorRegisterStrategy GetDecoratorRegisterStrategy();
    IConfigRegisterStrategy GetConfigRegisterStrategy();
}
