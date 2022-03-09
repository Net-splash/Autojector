using Autojector.Base;
using Autojector.Registers.Base;
using System;

namespace Autojector.Registers.Configs.TypeOperators;

internal record InterfaceConfigTypeOperator(Type InterfaceType, IConfigRegisterStrategy ConfigRegisterStrategy, string Key = null) :
    ITypeConfigurator
{

    public void ConfigureServices()
    {
        var classBuilder = new ClassBuilder(InterfaceType);
        var classType = classBuilder.BuildType();
        ConfigRegisterStrategy.Add(InterfaceType, classType, new string []
        {
            InterfaceType.Name.RemoveInterfacePrefix(),
            Key,
        });
    }
}
