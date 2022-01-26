using Autojector.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autojector.Tests.AsyncFactoryInjectable;

interface ITestTransientAsyncService { }
class TestTransientAsyncService : ITestTransientAsyncService { }

internal class TestTransientAsyncFactory : IAsyncTransientFactoryInjectable<ITestTransientAsyncService>
{
    public async Task<ITestTransientAsyncService> GetServiceAsync()
    {
        await Task.Delay(1);
        return new TestTransientAsyncService();
    }
}
