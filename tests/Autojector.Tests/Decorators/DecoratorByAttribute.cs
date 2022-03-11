using Autojector.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autojector.Tests.Decorators
{
    internal interface IDecoratedByAttribute
    {
    }

    internal class ServiceDecoratedByAttribute : IDecoratedByAttribute,ITransient<IDecoratedByAttribute>
    {
    }

    [Decorator(typeof(IDecoratedByAttribute))]
    internal class DecoratorByAttribute : IDecoratedByAttribute
    {
        public DecoratorByAttribute(IDecoratedByAttribute ServiceDecoratedByAttribute)
        {
            this.ServiceDecoratedByAttribute = ServiceDecoratedByAttribute;
        }

        public IDecoratedByAttribute ServiceDecoratedByAttribute { get; }
    }
}
