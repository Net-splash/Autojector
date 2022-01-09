﻿using Autojector.Public;

namespace Autojector.Tests.SimpleInjectableClasses;

internal interface ITestSingleton : ISingletonInjectable<ITestSingleton> { }
internal class TestSingleton : ITestSingleton, ISingletonInjectable<TestSingleton> { }
