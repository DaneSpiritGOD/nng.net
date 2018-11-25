using System;
using System.Collections.Generic;
using System.Text;
using System.Buffers;

namespace NanomsgPlus
{
    internal class SpecifiedLengthMemoryPool<T> : MemoryPool<T>
    {
        public override int MaxBufferSize => throw new NotImplementedException();

        public override IMemoryOwner<T> Rent(int minBufferSize = -1)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }
    }
}
