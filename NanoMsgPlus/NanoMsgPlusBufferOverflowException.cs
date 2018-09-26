using NanomsgPlus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanomsgPlus
{
    public class NanomsgPlusBufferOverflowException : NanomsgPlusException
    {
        public NanomsgPlusBufferOverflowException() : base("buffer offset overflow") { }
    }
}
