﻿using Autojector.Registers.Base;
using System;

namespace Autojector.Registers.Configs;
internal record ConfigTypeOperator(Type Config, IConfigRegisterStrategy ConfigRegisterStrategy) :
    BaseOperator,
    ITypeConfigurator
{
    public void ConfigureServices() => ConfigRegisterStrategy.Add(Config);
}