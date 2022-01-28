
using Autojector.Abstractions;

namespace Autojector.Tests.SimpleInjectableClasses;

internal interface ITestTransient : ITransient<ITestTransient> { }
internal class TestTransient : ITestTransient, ITransient<TestTransient> { }


