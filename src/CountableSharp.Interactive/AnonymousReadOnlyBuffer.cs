using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CountableSharp
{
    internal class AnonymousReadOnlyBuffer<T> : IReadOnlyBuffer<T>
    {
        public AnonymousReadOnlyBuffer(IBuffer<T> buffer, Func<int> count)
        {
            this.buffer = buffer;
            this.count = count;
        }

        private readonly IBuffer<T> buffer;
        private readonly Func<int> count;

        public IEnumerator<T> GetEnumerator()
        {
            return this.buffer.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Dispose()
        {
            this.buffer.Dispose();
        }

        public int Count
        {
            get
            {
                return this.count();
            }
        }
    }
}
