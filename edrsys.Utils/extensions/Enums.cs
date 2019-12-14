using edrsys.Utils.Attributes;
using System;

namespace edrsys.Utils.extensions
{
    public static class Enums
    {
        private static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0
              ? (T)attributes[0]
              : null;
        }

        public static string GetCode(this Enum value)
        {
            var attribute = value.GetAttribute<CodeAttribute>();
            return attribute == null ? string.Empty : attribute.Code;
        }
    }
}