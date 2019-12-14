using System;

namespace edrsys.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CodeAttribute : Attribute
    {
        private string code;

        public CodeAttribute(string code)
        {
            this.code = code;
        }

        public virtual string Code
        {
            get { return code; }
        }
    }
}