using Autojector.Base;
using Autojector.DependencyInjector.Public;
using Autojector.Extensions;
using System;

namespace Autojector.Registers.Configs.TypeOperators;

internal record InterfaceConfigTypeOperator(Type InterfaceType, IConfigRegisterStrategy ConfigRegisterStrategy, string Key = null) :
    ITypeConfigurator
{

    public void ConfigureServices()
    {
        var classBuilder = new ModelClassBuilder(InterfaceType);
        var classType = classBuilder.BuildType();
        ConfigRegisterStrategy.Add(InterfaceType, classType, new string []
        {
            InterfaceType.Name.RemoveInterfacePrefix(),
            Key,
        });
    }
}
