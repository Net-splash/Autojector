namespace Autojector.Base
{
    interface IAutojectorFeature : ITypeConfigurator
    {
        AutojectorFeaturesEnum FeatureType { get; }
    }
}