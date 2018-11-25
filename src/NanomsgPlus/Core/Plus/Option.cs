using NanomsgPlus.Core;
using NanomsgPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanomsgPlus.Core
{
    public static class Option
    {
        public const int NN_RCVMAXSIZE = 16;
        const int INFINITE_SIZE = -1;

        public static void SetReceiveMaxSize(this NanomsgSocketBase socket, int size)
        {
            NanomsgSocketOptions.SetInt(socket.SocketID, SocketOptionLevel.Default, (SocketOption)NN_RCVMAXSIZE, size);
        }

        public static void SetInfiniteReceiveMaxSize(this NanomsgSocketBase socket)
        {
            NanomsgSocketOptions.SetInt(socket.SocketID, SocketOptionLevel.Default, (SocketOption)NN_RCVMAXSIZE, INFINITE_SIZE);
        }
    }
}
