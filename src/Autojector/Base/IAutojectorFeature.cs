using Autojector.Features;
using Autojector.Features;

namespace Autojector.Base;
interface IAutojectorFeature : ITypeConfigurator
{
    internal AutojectorFeaturesEnum FeatureType { get; }
}
