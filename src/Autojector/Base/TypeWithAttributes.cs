using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autojector.Base
{
    internal class TypeWithAttributes<T>
        where T : Attribute
    {
        public TypeWithAttributes(Type Type)
        {
            this.Type = Type;
        }
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

        public Type Type { get; }
    }

}
