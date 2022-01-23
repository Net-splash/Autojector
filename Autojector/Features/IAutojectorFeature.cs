using Microsoft.Extensions.DependencyInjection;

namespace Autojector.Features;
interface IAutojectorFeature
{
    public IServiceCollection ConfigureServices(IServiceCollection services);
    internal AutojectorFeaturesEnum Priority { get; }
}
