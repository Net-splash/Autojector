using Autojector.Registers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Autojector.Features;
internal class AutojectorValidatorsFeatures : BaseAutojectorFeature
{
    public AutojectorValidatorsFeatures(AutojectorOptions.AutojectorValidatorsOptions autojectorValidatorsOptions, IEnumerable<Assembly> assemblies = null) : base(assemblies)
    {
        AutojectorValidatorsOptions = autojectorValidatorsOptions;
    }

    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.Validators;

    protected AutojectorOptions.AutojectorValidatorsOptions AutojectorValidatorsOptions { get; }

    protected override IEnumerable<ITypeConfigurator> GetTypeConfigurators()
    {
        return null;
    }
}
