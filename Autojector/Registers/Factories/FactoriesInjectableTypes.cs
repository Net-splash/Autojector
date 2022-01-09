using Autojector.Public;
using System;

namespace Autojector.Registers.Factories;
internal static class FactoriesInjectableTypes
{
    public static Type FactoryType = typeof(IFactory<>);
}
