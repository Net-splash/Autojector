using Autojector.Base;
using Autojector.DependencyInjector.Public;
using System;

namespace Autojector.Features.Configs;
internal record ConfigTypeOperator(Type Config, IConfigRegisterStrategy ConfigRegisterStrategy, string Key = null) :
    ITypeConfigurator
{
    public void ConfigureServices() => ConfigRegisterStrategy.Add(Config, Config, new string[]
    {
        Key
    });
}
