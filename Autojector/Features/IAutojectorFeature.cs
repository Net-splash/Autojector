using Microsoft.Extensions.DependencyInjection;

namespace Autojector.Features;
interface IAutojectorFeature
{
    public void ConfigureServices(IServiceCollection services);
    internal AutojectorFeaturesEnum Priority { get; }
}
