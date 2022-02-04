using Autojector.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Services;
public interface ISimpleInjectedDecoratedService
{
    string GetData();
}
internal class SimpleInjectedDecoratedService : ISimpleInjectedDecoratedService, ITransient<ISimpleInjectedDecoratedService>
{
    public string GetData()
    {
        return "SimpleInjectedDecoratedServiceData";
    }
}

internal class DecoratedService : IDecorator<ISimpleInjectedDecoratedService>, ISimpleInjectedDecoratedService
{
    private ISimpleInjectedDecoratedService SimpleInjectedDecoratedService { get; }
    public DecoratedService(ISimpleInjectedDecoratedService simpleInjectedDecoratedService)
    {
        SimpleInjectedDecoratedService = simpleInjectedDecoratedService;
    }

    public string GetData()
    {
        return SimpleInjectedDecoratedService.GetData() + " Decorated";
    }
}