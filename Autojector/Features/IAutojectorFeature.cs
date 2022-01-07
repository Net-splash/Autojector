using Microsoft.Extensions.DependencyInjection;

namespace Autojector.Features
{
    interface IAutojectorFeature
    {
        public void ConfigureServices(IServiceCollection services);
        public AutojectorFeaturesEnum Priority { get; }
    }
}
