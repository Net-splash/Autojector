using Autojector.Abstractions;

namespace Autojector.Tests.SimpleInjectable;

internal class NoInterfaceTransient: ITransient<NoInterfaceTransient>
{
}
