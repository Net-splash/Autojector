
using Autojector.Abstractions;

namespace Autojector.Tests.SimpleInjectable;
internal interface IAttributeTransient { }

[Transient(typeof(IAttributeTransient))]
internal class AttributeTransient : IAttributeTransient { }
