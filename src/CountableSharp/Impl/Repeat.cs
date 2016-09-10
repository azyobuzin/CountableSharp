using System;
using System.Collections;
using System.Collections.Generic;

namespace CountableSharp.Impl
{
    internal sealed class Repeat<T> : IReadOnlyList<T>, IList<T>
    {
        private readonly T _element;
        private readonly int _count;

        public Repeat(T element, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

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
            set
            {
                throw new NotSupportedException();
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

        public int IndexOf(T item)
        {
            return this.Contains(item) ? 0 : -1;
        }

        public void Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        private sealed class Enumerator : IEnumerator<T>
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
