using Autojector.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autojector;
internal class AsyncDependency<T> : Lazy<Task<T>>, IAsyncDependency<T>
{
    public AsyncDependency(Func<Task<T>> valueFactory) : base(valueFactory, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication) { }

}
