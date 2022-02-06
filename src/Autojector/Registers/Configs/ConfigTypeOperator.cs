using Autojector.Registers.Base;
using System;

namespace Autojector.Registers.Configs;
internal record ConfigTypeOperator(Type Config, IConfigRegisterStrategy ConfigRegisterStrategy) :
    ITypeConfigurator
{
    public void ConfigureServices() => ConfigRegisterStrategy.Add(Config);
}
