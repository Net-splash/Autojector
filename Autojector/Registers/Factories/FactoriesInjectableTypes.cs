using Autojector.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autojector.Registers.Factories
{
    internal static class FactoriesInjectableTypes
    {
        public static Type FactoryType = typeof(IFactory<>);
    }
}
