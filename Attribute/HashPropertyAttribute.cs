using System;

namespace HashProperty.Attribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class HashPropertyAttribute : System.Attribute
    {
    }
}
