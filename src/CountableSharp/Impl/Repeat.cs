using System;
using System.Collections;
using System.Collections.Generic;

namespace CountableSharp.Impl
{
    internal class Repeat<T> : IReadOnlyList<T>, ICollection<T>
    {
        private readonly T _element;
        private readonly int _count;

        public Repeat(T element, int count)
        {
            _element = element;
            _count = count;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return _element;
            }
        }

        public int Count => _count;

        public bool IsReadOnly => true;

        public void Add(T item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(T item)
        {
            return EqualityComparer<T>.Default.Equals(_element, item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (array.Length < _count + arrayIndex) throw new ArgumentException("The array is not long enough.");

            for (var i = 0; i < _count; i++)
                array[arrayIndex + i] = _element;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(_element, _count);
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private class Enumerator : IEnumerator<T>
        {
            private readonly int _count;
            private int _index = 0;

            public Enumerator(T element, int count)
            {
                this.Current = element;
                _count = count;
            }

            public T Current { get; }

            object IEnumerator.Current => this.Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                if (_index >= _count) return false;

                _index++;
                return true;
            }

            public void Reset()
            {
                _index = 0;
            }
        }
    }
}
