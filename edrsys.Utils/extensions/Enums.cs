using edrsys.Utils.Attributes;
using System;

namespace edrsys.Utils.extensions
{
    public static class Enums
    {
        /// <summary>
        /// Get Custom Attribute.
        /// </summary>
        /// <typeparam name="T">Type attribute.</typeparam>
        /// <param name="value">Enum value to get attribute.</param>
        /// <returns>Custom attribute.Attribute</returns>
        private static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0
              ? (T)attributes[0]
              : null;
        }

        /// <summary>
        /// Get attribute Code in string.
        /// </summary>
        /// <param name="value">value of attribute.</param>
        /// <returns>String value attribute.</returns>
        public static string GetCode(this Enum value)
        {
            var attribute = value.GetAttribute<CodeAttribute>();
            return attribute == null ? string.Empty : attribute.Code;
        }
    }
}