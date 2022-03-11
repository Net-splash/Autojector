using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.Base;
internal record TypeWithAttributes<T>(Type Type)
    where T : Attribute
{
    public bool HasAttributes => Attributes.Any();
    public IEnumerable<T> Attributes
    {
        get
        {
            var allAttributes = Type.GetCustomAttributes()
                .Where(attribute => attribute is T)
                .Select(attribute => (T)attribute);
            return allAttributes;
        }
    }
}

