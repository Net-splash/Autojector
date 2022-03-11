using Autojector.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Services;
public interface ISimpleInjectedDecoratedByAttributeService
{
    string GetData();
}

[Transient(typeof(ISimpleInjectedDecoratedByAttributeService))]
internal class SimpleInjectedDecoratedByAttributeService : ISimpleInjectedDecoratedByAttributeService
{
    public string GetData()
    {
        return "SimpleInjectedDecoratedByAttributeService";
    }
}

[Decorator(typeof(ISimpleInjectedDecoratedByAttributeService))]
internal class DecoratorByAttributeService : ISimpleInjectedDecoratedByAttributeService
{
    private ISimpleInjectedDecoratedByAttributeService SimpleInjectedDecoratedByAttributeService { get; }
    public DecoratorByAttributeService(ISimpleInjectedDecoratedByAttributeService simpleInjectedDecoratedByAttributeService)
    {
        SimpleInjectedDecoratedByAttributeService = simpleInjectedDecoratedByAttributeService;
    }

    public string GetData()
    {
        return SimpleInjectedDecoratedByAttributeService.GetData() + " Decorated";
    }
}