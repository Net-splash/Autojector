using Autojector.DependencyInjector.Public;
using Autojector.Registers.AsyncFactories;
using Autojector.Registers.Chains;
using Autojector.Registers.Configs;
using Autojector.Registers.Decorators;
using Autojector.Registers.Factories;
using Autojector.Registers.SimpleInjection;
using Microsoft.Extensions.DependencyInjection;
namespace Autojector.DependencyInjector;
internal class MicrosoftDependencyInjectorProvider : IDependencyInjectorProvider
{
    public MicrosoftDependencyInjectorProvider(IServiceCollection services)
    {
        Services = services;
    }

    private IServiceCollection Services { get; }

    public IAsyncFactoryRegisterStrategyFactory GetAsyncFactoryRegisterStrategyFactory() => new AsyncFactoryRegisterStrategyFactory(Services);

    public IChainRegisterStrategy GetChainRegisterStrategy() => new ChainRegisterStrategy(Services);

    public IConfigRegisterStrategy GetConfigRegisterStrategy() => new ConfigRegisterStrategy(Services);

    public IDecoratorRegisterStrategy GetDecoratorRegisterStrategy()  => new DecoratorRegisterStrategy(Services);

    public IFactoryRegisterStrategyFactory GetFactoryRegisterStrategyFactory() => new FactoryRegisterStrategyFactory(Services);

    public ISimpleRegisterStrategyFactory GetSimpleRegisterStrategyFactory() => new SimpleRegisterStrategyFactory(Services);
}
