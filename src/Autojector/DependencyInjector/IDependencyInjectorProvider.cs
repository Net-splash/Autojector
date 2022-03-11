using Autojector.DependencyInjector.Public;

namespace Autojector.DependencyInjector;
internal interface IDependencyInjectorProvider
{
    ISimpleRegisterStrategyFactory GetSimpleRegisterStrategyFactory();
    IFactoryRegisterStrategyFactory GetFactoryRegisterStrategyFactory();
    IChainRegisterStrategy GetChainRegisterStrategy();
    IAsyncFactoryRegisterStrategyFactory GetAsyncFactoryRegisterStrategyFactory();
    IDecoratorRegisterStrategy GetDecoratorRegisterStrategy();
    IConfigRegisterStrategy GetConfigRegisterStrategy();
}
