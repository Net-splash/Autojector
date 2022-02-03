

using Autojector.Abstractions;

namespace Autojector.Tests.Decorators;


internal interface IUndecoratedFactoryProvidedService
{

}

internal class UndecoratedFactoryProvidedService : IUndecoratedFactoryProvidedService
{
}


internal class UndecoratedFactoryProvidedServiceFactory : ITransientFactory<IUndecoratedFactoryProvidedService>
{
    public UndecoratedFactoryProvidedServiceFactory()
    {
    }

    public IUndecoratedFactoryProvidedService GetService()
    {
        return new UndecoratedFactoryProvidedService();
    }
}


internal class UndecoratedFactoryProvidedServiceDecorator : IDecorator<IUndecoratedFactoryProvidedService>, 
    IUndecoratedFactoryProvidedService
{
    public UndecoratedFactoryProvidedServiceDecorator(IUndecoratedFactoryProvidedService undecoratedFactoryProvidedService)
    {
        UndecoratedFactoryProvidedService = undecoratedFactoryProvidedService;
    }

    public IUndecoratedFactoryProvidedService UndecoratedFactoryProvidedService { get; }
}

