using System;
using System.Collections.Generic;
using System.Linq;
using static Autojector.Base.Types;

namespace Autojector.Features.SimpleInjection.Operators;

internal record BaseSimpleInjectableOperator(Type Type)
{
    protected void ValidateNotImplementedInterface(IEnumerable<Type> nonImplementedInterfaceFromLifetype)
    {
        if (nonImplementedInterfaceFromLifetype.Any())
        {
            var interfacesNames = nonImplementedInterfaceFromLifetype.Select(i => i.Name);
            var interfacesListNames = string.Join(",", interfacesNames);
            throw new InvalidOperationException($@"The interfaces {interfacesListNames} are not implemented by {Type} but are registered as injectable");
        }
    }
    protected void ValidateUnknownLifetype(IEnumerable<Type> lifetypeInterfaces)
    {
        if (!lifetypeInterfaces.Any())
        {
            var lifetypeInterfacesNames = SimpleLifetypeInterfaces.Select(c => c.Name);
            throw new InvalidOperationException(@$"
                            The class {Type.Name} doesn't implement a LifeType interface.
                            LifeTypeInterfacess allowed {string.Join(",", lifetypeInterfacesNames)}
                        ");
        }
    }
}