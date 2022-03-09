using Autojector.Registers.Base;
using System;

namespace Autojector.Registers.Configs;
internal record ConfigTypeOperator(Type Config, IConfigRegisterStrategy ConfigRegisterStrategy, string Key = null) :
    ITypeConfigurator
{
    public void ConfigureServices() => ConfigRegisterStrategy.Add(Config, Config, new string[]
    {
        Key
    });
}
