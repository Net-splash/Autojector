

using Autojector.Abstractions;

namespace Autojector.Tests.Decorators;

internal interface IUndecoratedService
{

}

internal class UndecoratedService : ITransient<IUndecoratedService>, IUndecoratedService
{
}

internal class DecoratedService : IDecorator<IUndecoratedService>, IUndecoratedService
{
    public DecoratedService(IUndecoratedService undecoratedService)
    {
        UndecoratedService = undecoratedService;
    }

    public IUndecoratedService UndecoratedService { get; }
}
