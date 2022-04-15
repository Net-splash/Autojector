

using System;
using System.Collections.Generic;
using System.Linq;

namespace Autojector.Extensions;
internal static class SystemTypeExtensions
{
    public static IEnumerable<Type> GetInterfacesFromTree(this Type type, Func<Type, bool> filter)
    {
        var allInterfaces = type.GetInterfaces();
        var groupedInterfaces = allInterfaces.ToLookup(filter);
        var matchCondition = groupedInterfaces[true];
        var otherInterfaces = groupedInterfaces[false];
        var lifeTypeInterfacesFromOtherInterfaces = otherInterfaces.SelectMany(x => x.GetInterfacesFromTree(filter));
        return matchCondition.Concat(lifeTypeInterfacesFromOtherInterfaces).Distinct();
    }

    public static IEnumerable<Type> GetConcrateImplementationThatMatchGenericsDefinition(this Type type, IEnumerable<Type> genericBaseDefinitions)
    {
        return type.GetInterfacesFromTree(i => i.IsGenericType && genericBaseDefinitions.Contains(i.GetGenericTypeDefinition()));
    }

    public static bool HasAnyConcrateImplementationThatMatchGenericsDefinition(this Type type, IEnumerable<Type> genericBaseDefinitions)
    {
        return type.GetConcrateImplementationThatMatchGenericsDefinition(genericBaseDefinitions).Any();
    }


    public static Type GetFirstArgument(this Type type) => type.GetNthArgument(1);
    public static Type GetSecondArgument(this Type type) => type.GetNthArgument(2);
    public static Type GetNthArgument(this Type type, int argumentIndex)
    {
        var arguments = type.GetGenericArguments();
        if (arguments.Count() < argumentIndex)
        {
            throw new InvalidOperationException($"The type {type.Name} doesn't have {argumentIndex} arguments");
        }

        return arguments.Skip(argumentIndex - 1).First();
    }
}
