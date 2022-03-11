using Autojector.DependencyInjector.Public;
using Autojector.Features.AsyncFactories;
using Autojector.Features.Chains;
using Autojector.Features.Configs;
using Autojector.Features.Decorators;
using Autojector.Features.Factories;
using Autojector.Features.SimpleInjection;
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
