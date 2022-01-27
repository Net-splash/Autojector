using Autojector.Abstractions;

namespace Autojector.Tests.Decorators;

internal interface ISimpleService { }

internal class SimpleService : ITransientInjectable<ISimpleService>, ISimpleService
{
}


[DecoratorOrder(1)]
internal class FirstDecorator : IDecorator<ISimpleService>, ISimpleService
{
    public FirstDecorator(ISimpleService simpleService)
    {
        SimpleService = simpleService;
    }

    public ISimpleService SimpleService { get; }
}

[DecoratorOrder(2)]
internal class SecondDecorator : IDecorator<ISimpleService>, ISimpleService
{
    public SecondDecorator(ISimpleService simpleService)
    {
        SimpleService = simpleService;
    }

    public ISimpleService SimpleService { get; }
}
