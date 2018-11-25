using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanomsgPlus
{
    public class NanomsgPlusSocketNullOrDisposedException: NanomsgPlusException
    {
        public NanomsgPlusSocketNullOrDisposedException() : base("socket may be disposed.") { }
    }
}
