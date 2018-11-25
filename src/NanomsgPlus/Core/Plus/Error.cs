using NanomsgPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NanomsgPlus.Core
{
    public static class Error
    {
        public static int GetErrorCode() => Interop.nn_errno();
        public static string StrError(int errnum) => Marshal.PtrToStringAnsi(Interop.nn_strerror(errnum));

        public const int NN_HAUSNUMERO = 156384712;

        public const int EPERM = 1;
        public const int EBADF = 9;
        public const int ENOTSUP = 129;
        public const int EAGAIN = 11;
        public const int EINTR = 4;
        public const int ETIMEDOUT = 138;

        public const int ETERM = (NN_HAUSNUMERO + 53);
        public const int EFSM = (NN_HAUSNUMERO + 54);
    }
}
