using System;

namespace edrsys.Utils.Attributes
{
    /// <summary>
    /// Attibute for code.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class CodeAttribute : Attribute
    {
        /// <summary>
        /// Store value attribute.
        /// </summary>
        private string code;

        /// <summary>
        /// Code Attribute.
        /// </summary>
        /// <param name="code">Code in string.</param>
        public CodeAttribute(string code)
        {
            this.code = code;
        }

        /// <summary>
        /// Code value.
        /// </summary>
        public virtual string Code
        {
            get { return code; }
        }
    }
}