using Autojector.Registers;
using Autojector.Registers.Base;

namespace Autojector.Features.Base;
interface IAutojectorFeature : ITypeConfigurator
{
    internal AutojectorFeaturesEnum Priority { get; }
}
