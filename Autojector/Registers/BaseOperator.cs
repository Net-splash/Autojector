using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Registers;

public record BaseOperator() {
    protected IEnumerable<Type> GetInterfacesFromTree(Type type, Func<Type, bool> filter)
    {
        var allInterfaces = type.GetInterfaces();
        var groupedInterfaces = allInterfaces.ToLookup(filter);
        var lifeTypeInterface = groupedInterfaces[true];
        var otherInterfaces = groupedInterfaces[false];
        var lifeTypeInterfacesFromOtherInterfaces = otherInterfaces.SelectMany(x => GetInterfacesFromTree(x, filter));
        return lifeTypeInterface.Concat(lifeTypeInterfacesFromOtherInterfaces).Distinct();
    }
}
