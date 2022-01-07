using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Autojector.Features
{
    internal class AutojectorValidatorsFeatures : BaseAutojectorFeature
    {
        public AutojectorValidatorsFeatures(AutojectorValidatorsOptions autojectorValidatorsOptions, IEnumerable<Assembly> assemblies = null) : base(assemblies)
        {
            AutojectorValidatorsOptions = autojectorValidatorsOptions;
        }

        public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.Validators;

        protected AutojectorValidatorsOptions AutojectorValidatorsOptions { get; }

        public override void ConfigureServices(IServiceCollection services)
        {
           
        }
    }
}
