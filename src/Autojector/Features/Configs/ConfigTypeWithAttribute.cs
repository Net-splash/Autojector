
using Autojector.Abstractions;
using Autojector.Base;
using System;
using System.Linq;

namespace Autojector.Features.Configs
{
    internal class ConfigTypeWithAttribute : TypeWithAttributes<ConfigAttribute>
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
}
