using System.Collections;
using System.Collections.Generic;

namespace CountableSharp
{
    internal class AnonymousReadOnlyCollection<T> : IReadOnlyCollection<T>
    {
        public AnonymousReadOnlyCollection(IEnumerable<T> enumerable, int count)
        {
            this.enumerable = enumerable;
            this.count = count;
        }

        private IEnumerable<T> enumerable;
        private int count;

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
