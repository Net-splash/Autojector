using Autojector.Registers;
using Autojector.Registers.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Autojector.Features.Validators;
internal class AutojectorValidatorsFeatures : BaseAutojectorFeature
{
    public AutojectorValidatorsFeatures(
        AutojectorOptions.AutojectorValidatorsOptions autojectorValidatorsOptions, 
        IEnumerable<Assembly> assemblies,
        IServiceCollection services) : 
        base(assemblies, services)
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
