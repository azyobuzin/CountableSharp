using System;
using System.Collections.Generic;
using System.Linq;
using CountableSharp.Impl;

namespace CountableSharp
{
    /// <summary>
    /// <see cref="System.Collections.Generic.IReadOnlyCollection{T}"/> 向け LINQ 実装です。
    /// 各メソッドの使い方は <see cref="System.Linq.Enumerable"/> を参照してください。
    /// </summary>
    public static class Countable
    {
        internal static AnonymousReadOnlyCollection<T> Create<T>(IEnumerable<T> enumerable, Func<int> count)
        {
            return new AnonymousReadOnlyCollection<T>(enumerable, count);
        }

        private static AnonymousOrderedReadOnlyCollection<T> CreateOrdered<T>(IOrderedEnumerable<T> enumerable, Func<int> count)
        {
            return new AnonymousOrderedReadOnlyCollection<T>(enumerable, count);
        }

        public static IReadOnlyCollection<TSource> AsReadOnlyCollection<TSource>(this ICollection<TSource> source)
        {
            return source as IReadOnlyCollection<TSource> ?? new CollectionWrapper<TSource>(source);
        }

        public static IReadOnlyList<int> Range(int start, int count)
        {
            return new Range(start, count);
        }

        public static IReadOnlyList<TResult> Repeat<TResult>(TResult element, int count)
        {
            return new Repeat<TResult>(element, count);
        }

        public static IReadOnlyCollection<TSource> Concat<TSource>(this IReadOnlyCollection<TSource> first, IReadOnlyCollection<TSource> second)
        {
            return Create(Enumerable.Concat(first, second), () => first.Count + second.Count);
        }

        public static IReadOnlyCollection<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IReadOnlyCollection<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
        {
            return Create(Enumerable.GroupJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector), () => outer.Count);
        }

        public static IReadOnlyCollection<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IReadOnlyCollection<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            return Create(Enumerable.GroupJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer), () => outer.Count);
        }

        public static IReadOnlyCollection<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IReadOnlyCollection<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            return Create(Enumerable.Join(outer, inner, outerKeySelector, innerKeySelector, resultSelector), () => inner.Count);
        }

        public static IReadOnlyCollection<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IReadOnlyCollection<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            return Create(Enumerable.Join(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer), () => inner.Count);
        }

        public static IOrderedReadOnlyCollection<TSource> OrderBy<TSource, TKey>(this IReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            return CreateOrdered(Enumerable.OrderBy(source, keySelector), () => source.Count);
        }

        public static IOrderedReadOnlyCollection<TSource> OrderBy<TSource, TKey>(this IReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return CreateOrdered(Enumerable.OrderBy(source, keySelector, comparer), () => source.Count);
        }

        public static IOrderedReadOnlyCollection<TSource> OrderByDescending<TSource, TKey>(this IReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            return CreateOrdered(Enumerable.OrderByDescending(source, keySelector), () => source.Count);
        }

        public static IOrderedReadOnlyCollection<TSource> OrderByDescending<TSource, TKey>(this IReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return CreateOrdered(Enumerable.OrderByDescending(source, keySelector, comparer), () => source.Count);
        }

        public static IReadOnlyCollection<TSource> Reverse<TSource>(this IReadOnlyCollection<TSource> source)
        {
            return Create(Enumerable.Reverse(source), () => source.Count);
        }

        public static IReadOnlyCollection<TResult> Select<TSource, TResult>(this IReadOnlyCollection<TSource> source, Func<TSource, TResult> selector)
        {
            return Create(Enumerable.Select(source, selector), () => source.Count);
        }

        public static IReadOnlyCollection<TResult> Select<TSource, TResult>(this IReadOnlyCollection<TSource> source, Func<TSource, int, TResult> selector)
        {
            return Create(Enumerable.Select(source, selector), () => source.Count);
        }

        public static IReadOnlyCollection<TSource> Skip<TSource>(this IReadOnlyCollection<TSource> source, int count)
        {
            return Create(Enumerable.Skip(source, count), () => Math.Max(source.Count - count, 0));
        }

        public static IReadOnlyCollection<TSource> Take<TSource>(this IReadOnlyCollection<TSource> source, int count)
        {
            return Create(Enumerable.Take(source, count), () => Math.Min(source.Count, count));
        }

        public static IOrderedReadOnlyCollection<TSource> ThenBy<TSource, TKey>(this IOrderedReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.CreateOrderedReadOnlyCollection(keySelector, null, false);
        }

        public static IOrderedReadOnlyCollection<TSource> ThenBy<TSource, TKey>(this IOrderedReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return source.CreateOrderedReadOnlyCollection(keySelector, comparer, false);
        }

        public static IOrderedReadOnlyCollection<TSource> ThenByDescending<TSource, TKey>(this IOrderedReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.CreateOrderedReadOnlyCollection(keySelector, null, true);
        }

        public static IOrderedReadOnlyCollection<TSource> ThenByDescending<TSource, TKey>(this IOrderedReadOnlyCollection<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return source.CreateOrderedReadOnlyCollection(keySelector, comparer, true);
        }

        public static IReadOnlyCollection<TResult> Zip<TFirst, TSecond, TResult>(this IReadOnlyCollection<TFirst> first, IReadOnlyCollection<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            return Create(Enumerable.Zip(first, second, resultSelector), () => Math.Min(first.Count, second.Count));
        }

        public static TSource[] ToArray<TSource>(this IReadOnlyCollection<TSource> source)
        {
            var collection = source as ICollection<TSource>;
            if (collection != null) return ToArray(collection);

            var buffer = new TSource[source.Count];
            var i = 0;
            foreach (var item in source)
                buffer[i++] = item;
            return buffer;
        }

        public static TSource[] ToArray<TSource>(this IReadOnlyList<TSource> source)
        {
            var collection = source as ICollection<TSource>;
            if (collection != null) return ToArray(collection);

            // The indexer is faster than the enumerator
            var buffer = new TSource[source.Count];
            for (var i = 0; i < buffer.Length; i++)
                buffer[i] = source[i];
            return buffer;
        }

        public static TSource[] ToArray<TSource>(this ICollection<TSource> source)
        {
            var buffer = new TSource[source.Count];
            source.CopyTo(buffer, 0);
            return buffer;
        }

        public static List<TSource> ToList<TSource>(this IReadOnlyCollection<TSource> source)
        {
            var collection = source as ICollection<TSource>;
            if (collection != null) return new List<TSource>(collection);

            var list = new List<TSource>(source.Count);
            foreach (var item in source)
                list.Add(item);
            return list;
        }
    }
}
