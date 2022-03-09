
using Autojector.Base;
using Autojector.Registers.Base;
using System;
using System.Collections.Generic;

namespace Autojector.Registers.Configs.ImplementationVersions;

internal abstract class BaseConfigImplementationVersion : IFeatureImplementationVersion
{
    public BaseConfigImplementationVersion(IEnumerable<Type> TypesFromAssemblies, IConfigRegisterStrategy configRegisterStrategy)
    {
        this.TypesFromAssemblies = TypesFromAssemblies;
        ConfigRegisterStrategy = configRegisterStrategy;
    }

    protected IEnumerable<Type> TypesFromAssemblies { get; }
    protected IConfigRegisterStrategy ConfigRegisterStrategy { get; }

    public abstract IEnumerable<ITypeConfigurator> GetTypeConfigurators();
}
