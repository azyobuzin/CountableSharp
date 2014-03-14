using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CountableSharp
{
    internal class AnonymousOrderedReadOnlyCollection<T> : IOrderedReadOnlyCollection<T>
    {
        public AnonymousOrderedReadOnlyCollection(IOrderedEnumerable<T> enumerable, int count)
        {
            this.enumerable = enumerable;
            this.count = count;
        }

        private IOrderedEnumerable<T> enumerable;
        private int count;

        public int Count
        {
            get
            {
                return count;
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

        public IOrderedReadOnlyCollection<T> CreateOrderedReadOnlyCollection<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            return new AnonymousOrderedReadOnlyCollection<T>(this.enumerable.CreateOrderedEnumerable(keySelector, comparer, descending), this.Count);
        }

        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            return this.CreateOrderedReadOnlyCollection(keySelector, comparer, descending);
        }
    }
}
