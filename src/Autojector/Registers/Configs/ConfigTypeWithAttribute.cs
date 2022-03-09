
using Autojector.Abstractions;
using Autojector.Base;
using System;
using System.Linq;

namespace Autojector.Registers.Configs;

internal record ConfigTypeWithAttribute : TypeWithAttributes<ConfigAttribute>
{
    public ConfigTypeWithAttribute(Type Type) : base(Type)
    {
    }

    public string AttributeKey
    {
        get
        {
            var attribute = Attributes.First();
            return attribute.Key;
        }
    }
}
