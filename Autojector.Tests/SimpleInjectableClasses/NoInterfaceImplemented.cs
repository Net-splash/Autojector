using Autojector.Public;

namespace Autojector.Tests.SimpleInjectableClasses;

internal interface UnimplementedInterface { }
internal class NoInterfaceImplemented: ITransientInjectable<UnimplementedInterface> { }