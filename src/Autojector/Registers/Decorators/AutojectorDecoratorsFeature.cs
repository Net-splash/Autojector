using Autojector.Base;
using Autojector.Features.Decorators.ImplementationVersion;
using Autojector.Registers;
using Autojector.Registers.Decorators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Autojector.Features.Decorators;
internal record DecoratorWithDecoratedType(Type Decorator, Type Decorated);
internal class AutojectorDecoratorsFeature : BaseVersionedAutojectorFeature
{
    private IDecoratorRegisterStrategy DecoratorRegisterStrategy { get; }
    public override AutojectorFeaturesEnum Priority => AutojectorFeaturesEnum.Decorators;
    public AutojectorDecoratorsFeature(
        IEnumerable<Assembly> assemblies,
        IDecoratorRegisterStrategy decoratorRegisterStrategy
        ) : base(assemblies)
    {
        DecoratorRegisterStrategy = decoratorRegisterStrategy;
    }

    protected override IEnumerable<IFeatureImplementationVersion> GetImplementationVersions()
    {
        return new List<IFeatureImplementationVersion>()
        {
            new DecoratorAttributeVersion(NonAbstractClassesFromAssemblies,DecoratorRegisterStrategy),
            new DecoratorInterfaceVersion(NonAbstractClassesFromAssemblies,DecoratorRegisterStrategy)
        };
    }
}
