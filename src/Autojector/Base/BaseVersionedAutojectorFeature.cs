using Autojector.Registers.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.Base;

internal abstract class BaseVersionedAutojectorFeature : BaseAutojectorFeature
{
    protected BaseVersionedAutojectorFeature(IEnumerable<Assembly> assemblies) : base(assemblies)
    {
    }

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        var implementationVersion = GetImplementationVersions();
        return implementationVersion.SelectMany(i => i.GetTypeConfigurators());
    }

    protected abstract IEnumerable<IFeatureImplementationVersion> GetImplementationVersions();
}
