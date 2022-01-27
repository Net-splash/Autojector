using System;
using System.Collections.Generic;

namespace Autojector.Registers;
public record BaseTypeOperator(Type Type) : BaseOperator
{
    protected IEnumerable<Type> GetInterfacesFromTree(Func<Type, bool> filter)
    {
        return GetInterfacesFromTree(Type, filter);
    }
}
