using System;
using System.Collections.Generic;

namespace Autojector.DependencyInjector.Public;
internal interface IConfigRegisterStrategy
{
    void Add(Type interfaceType, Type classType, IEnumerable<string> keys);
}
