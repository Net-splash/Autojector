﻿using Autojector.Abstractions;

namespace Autojector.Tests.FactoriesIntjectable;
interface ITestTransientService { }
class TestTransientService : ITestTransientService { }
internal class TestTransientFactory : ITransientFactory<ITestTransientService>
{
    public ITestTransientService GetService() => new TestTransientService();
}
