using Autojector.Abstractions;

namespace Autojector.Tests.SimpleInjectableClasses;

internal class NoInterfaceTransient: ITransientInjectable<NoInterfaceTransient>
{
}
