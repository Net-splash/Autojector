using Autojector.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Autojector
{
    public class AutojectorValidatorsOptions { 
        internal AutojectorValidatorsOptions()
        {

        }
    }

    public class AutojectorOptions
    {
        internal AutojectorOptions()
        {
            Features = new List<IAutojectorFeature>();
        }
        private List<IAutojectorFeature> Features { get; }

        public AutojectorOptions UseAutojectorSimpleInjection(IEnumerable<Assembly> assemblies = null)
        {
            Features.Add(new AutojectorSimpleInjectionFeature(assemblies));
            return this;
        }
        
        public AutojectorOptions UseAutojectorFactories(IEnumerable<Assembly> assemblies = null)
        {
            Features.Add(new AutojectorFactoriesFeature(assemblies));
            return this;
        }

        public AutojectorOptions UseAutojectorAsyncFactories()
        {
            return this;
        }

        public AutojectorOptions UseAutojectorDecorator()
        {
            return this;
        }

        public AutojectorOptions UseAutojectorValidators(Action<AutojectorValidatorsOptions> validatorsConfigureOptions, IEnumerable<Assembly> assemblies = null)
        {
            var autojectorValidatorsOptions = new AutojectorValidatorsOptions();
            validatorsConfigureOptions(autojectorValidatorsOptions);
            Features.Add(new AutojectorValidatorsFeatures(autojectorValidatorsOptions, assemblies));
            return this;
        }

        internal IEnumerable<IAutojectorFeature> GetAutojectorFeatures()
        {
            var orderedFeatures = Features.OrderBy(feature => feature.Priority);
            return orderedFeatures;
        }
    }
}
