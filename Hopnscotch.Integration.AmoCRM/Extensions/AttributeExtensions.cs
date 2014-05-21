using System;

namespace Hopnscotch.Portal.Integration.AmoCRM.Extensions
{
    public static class AttributeExtensions
    {
        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            var attributeType = typeof(T);
            return (T)Attribute.GetCustomAttribute(type, attributeType);
        }
    }
}
