namespace Autojector.Base;
interface IAutojectorFeature : ITypeConfigurator
{
    internal AutojectorFeaturesEnum FeatureType { get; }
}
