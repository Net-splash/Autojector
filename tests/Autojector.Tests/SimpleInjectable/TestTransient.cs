
using Autojector.Abstractions;
using System;

namespace Autojector.Tests.SimpleInjectable;

internal interface ITestTransient : ITransient<ITestTransient> { }
internal class TestTransient : ITestTransient, ITransient<TestTransient> { }


