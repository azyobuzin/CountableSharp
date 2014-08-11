using System.Collections;
using System.Collections.Generic;

namespace CountableSharp
{
    internal class CollectionWrapper<T> : IReadOnlyCollection<T>, ICollection<T>
    {
        public CollectionWrapper(ICollection<T> collection)
        {
            this.collection = collection;
        }

        private readonly ICollection<T> collection;

        public int Count
        {
            get
            {
                return this.collection.Count;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        // 最適化コードのため ICollection<T> を実装
        public void Add(T item)
        {
            this.collection.Add(item);
        }

        public void Clear()
        {
            this.collection.Clear();
        }

        public bool Contains(T item)
        {
            return this.collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.collection.CopyTo(array, arrayIndex);
        }

        public bool IsReadOnly
        {
            get
            {
                return this.collection.IsReadOnly;
            }
        }

        public bool Remove(T item)
        {
            return this.collection.Remove(item);
        }
    }
}
