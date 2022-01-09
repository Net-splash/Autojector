using Microsoft.Extensions.DependencyInjection;
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

    public override void ConfigureServices(IServiceCollection services)
    {

    }
}
