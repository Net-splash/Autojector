

using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Base;
internal static class SystemTypeExtensions
{
    public static IEnumerable<Type> GetInterfacesFromTree(this Type type, Func<Type, bool> filter)
    {
        var allInterfaces = type.GetInterfaces();
        var groupedInterfaces = allInterfaces.ToLookup(filter);
        var lifeTypeInterface = groupedInterfaces[true];
        var otherInterfaces = groupedInterfaces[false];
        var lifeTypeInterfacesFromOtherInterfaces = otherInterfaces.SelectMany(x => x.GetInterfacesFromTree(filter));
        return lifeTypeInterface.Concat(lifeTypeInterfacesFromOtherInterfaces).Distinct();
    }
}
