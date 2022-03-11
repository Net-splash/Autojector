using System;
using System.Collections.Generic;

namespace Autojector.DependencyInjector.Public;
internal interface IChainRegisterStrategy
{
    void Add(Type requestType, Type responseType, IEnumerable<Type> chainLinks);
}

