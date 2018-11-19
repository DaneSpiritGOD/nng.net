using NanomsgPlus;
using NanomsgPlus.Core;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NanomsgPlus
{
    public static class ReceiveExtension
    {
        /// <summary>
        /// nn_recv with block mode that run in a background thread is not recommended.
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns> 
        public static Memory<byte> ReceiveFrom(this int fd)
        {
            NotTrueException.Assert(fd >= 0, nameof(fd));

            return readFromFd(fd, false);
        }

        public static Memory<byte> Poll(this int fd, int timeout)
        {
            NumberOutOfRangeException<int>.Assert(fd, -1, int.MaxValue, nameof(fd));

            var fds = new[] { fd };
            var results = Poll(fds, timeout);
            if (results.Count() != 1)
            {
                throw new NotImplementedException();
            }

            return results.Single();
        }

        public static IEnumerable<Memory<byte>> Poll(this int[] fds, int timeout)
        {
            var count = fds.Length;
            if (count == 0)
            {
                return new[] { Memory<byte>.Empty };
            }

            Span<nn_pollfd> pollFds = stackalloc nn_pollfd[count];
            for (var index = 0; index < count; ++index)
            {
                pollFds[index].fd = NumberOutOfRangeException<int>.Assert(fds[index], -1, int.MaxValue, nameof(fds));
                pollFds[index].events = PollEvent.NN_POLLIN;
            }

            return Poll(pollFds, timeout);
        }

        private static unsafe IEnumerable<Memory<byte>> Poll(this Span<nn_pollfd> pollFds, int timeout)
        {
            var rc = 0;
            fixed (nn_pollfd* pInfo = pollFds)
            {
                rc = Interop.nn_poll(pInfo, pollFds.Length, timeout);
            }

            if (rc == -1)
            {
                if (NN.Errno() == Core.Error.EPERM)
                {
                    throw new NanomsgPlusSocketNullOrDisposedException();//socket被disposed了
                }
                else
                {
                    throw new NanomsgException();
                }
            }
            if (rc == 0)
            {
                return new[] { Memory<byte>.Empty };
            }

            var mems = new List<Memory<byte>>();
            for (var index = 0; index < pollFds.Length; ++index)
            {
                if ((pollFds[index].revents & PollEvent.NN_POLLIN) != 0)
                {
                    mems.Add(readFromFd(pollFds[index].fd));
                }
            }
            return mems;
        }

        private static Memory<byte> readFromFd(int fd, bool async = true)
        {
            var zero = IntPtr.Zero;
            var num = Interop.nn_recv(fd, ref zero, SendRecv.NN_MSG, async ? SendRecv.NN_DONTWAIT : SendRecv.NN_WAIT);
            if (num < 0 || zero == IntPtr.Zero)
            {
                throw new NanomsgException();
            }

            var data = new byte[num];
            Marshal.Copy(zero, data, 0, num);

            if (Interop.nn_freemsg(zero) != 0)
            {
                throw new NanomsgException();
            }

            return data;
        }

        private static unsafe IMemoryOwner<byte> readFromFd2MemoryOwner(int fd)
        {
            var zero = IntPtr.Zero;
            var num = Interop.nn_recv(fd, ref zero, SendRecv.NN_MSG, SendRecv.NN_DONTWAIT);
            if (num < 0 || zero == IntPtr.Zero)
            {
                throw new NanomsgException();
            }

            var owner = MemoryPool<byte>.Shared.Rent(num);
            var mem = owner.Memory;
            using (var handle = mem.Pin())
            {
                Buffer.MemoryCopy(zero.ToPointer(), handle.Pointer, mem.Length, num);
            }

            if (Interop.nn_freemsg(zero) != 0)
            {
                throw new NanomsgException();
            }

            return owner;
        }
    }
}
