using System;
using System.Diagnostics;

namespace Kipp.Framework.Models
{
    [DebuggerDisplay("{Value}")]
    public class Identity
    {
        public string Value { get; set; }
        public Identity(string value)
        {
            Value = value;
        }

        public static Identity New()
        {
            var random = new Random();
            var bytes = new Byte[30];
            random.NextBytes(bytes);

            var hexArray = Array.ConvertAll(bytes, x => x.ToString("X2"));
            var hexStr = String.Concat(hexArray);

            return new Identity(hexStr);
        }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object other)
        {
            if (other is null)
            {
                return false;
            }else if(other is Identity other_identity)
            {
                return Value == other_identity.Value;
            }else if(other is string other_string)
            {
                return Value == other_string;
            }

            return false;
        }
    }
}