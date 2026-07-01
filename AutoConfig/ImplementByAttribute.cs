using System;

namespace QlThietBi.AutoConfig
{
    public class ImplementByAttribute : Attribute
    {
        public Type Type { get; }

        public ImplementByAttribute(Type type)
        {
            Type = type;
        }
    }
}