using Autojector.Registers.Base;
using System.Collections.Generic;

namespace Autojector.Base;

internal interface IFeatureImplementationVersion
{
    public IEnumerable<ITypeConfigurator> GetTypeConfigurators();
}
