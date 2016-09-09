using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CountableSharp.Impl
{
    internal sealed class Range : IReadOnlyList<int>, IList<int>
    {
        private readonly int _start;
        private readonly int _count;

        public Range(int start, int count)
        {
            // Check overflow
            var end = (long)start + count - 1;
            if (count < 0 || end > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(count));

            _start = start;
            _count = count;
        }

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return _start + index;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public int Count => _count;

        public bool IsReadOnly => true;

        public void Add(int item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(int item)
        {
            return item >= _start && item <= _start + _count - 1;
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (array.Length < _count + arrayIndex) throw new ArgumentException("The array is not long enough.");

            for (var i = 0; i < _count; i++)
                array[arrayIndex + i] = _start + i;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return _count == 0
                ? Enumerable.Empty<int>().GetEnumerator()
                : new Enumerator(_start, _start + _count - 1);
        }

        public bool Remove(int item)
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public int IndexOf(int item)
        {
            return this.Contains(item)
                ? item - _start
                : -1;
        }

        public void Insert(int index, int item)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        private class Enumerator : IEnumerator<int>
        {
            private readonly int _start;
            private readonly int _last;
            private bool _started;

            public Enumerator(int start, int last)
            {
                _start = start;
                _last = last;
            }

            public int Current { get; private set; }

            object IEnumerator.Current => this.Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                if (_started)
                {
                    if (this.Current >= _last) return false;

                    this.Current++;
                    return true;
                }

                _started = true;
                this.Current = _start;
                return true;
            }

            public void Reset()
            {
                _started = false;
            }
        }
    }
}
