using NanomsgPlus.Core;
using NanomsgPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanomsgPlus
{
    public class NanomsgPlusException : Exception
    {
        public NanomsgPlusException() : base("Unkown Error occurs.") { }
        public NanomsgPlusException(string msg) : base(msg) { }
        public NanomsgPlusException(string msg, Exception ex) : base(msg, ex) { }
    }
}
