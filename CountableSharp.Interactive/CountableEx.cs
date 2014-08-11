using System;
using System.Collections.Generic;
using System.Linq;

namespace CountableSharp
{
    public static class CountableEx
    {
        private static AnonymousReadOnlyBuffer<T> CreateBuffer<T>(IBuffer<T> buffer, Func<int> count)
        {
            return new AnonymousReadOnlyBuffer<T>(buffer, count);
        }

        public static IReadOnlyCollection<IList<TSource>> Buffer<TSource>(this IReadOnlyCollection<TSource> source, int count)
        {
            return Countable.Create(EnumerableEx.Buffer(source, count), () => (int)Math.Ceiling((double)source.Count / count));
        }

        public static IReadOnlyCollection<IList<TSource>> Buffer<TSource>(this IReadOnlyCollection<TSource> source, int count, int skip)
        {
            return Countable.Create(EnumerableEx.Buffer(source, count, skip), () => (int)Math.Ceiling((double)source.Count / skip));
        }

        public static IReadOnlyCollection<TSource> Concat<TSource>(params IReadOnlyCollection<TSource>[] sources)
        {
            return Countable.Create(EnumerableEx.Concat(sources), () => sources.Sum(s => s.Count));
        }

        public static IReadOnlyCollection<TSource> Do<TSource>(this IReadOnlyCollection<TSource> source, Action<TSource> onNext)
        {
            return Countable.Create(EnumerableEx.Do(source, onNext), () => source.Count);
        }

        public static IReadOnlyCollection<TSource> Do<TSource>(this IReadOnlyCollection<TSource> source, Action<TSource> onNext, Action onCompleted)
        {
            return Countable.Create(EnumerableEx.Do(source, onNext, onCompleted), () => source.Count);
        }

        public static IReadOnlyCollection<TSource> Do<TSource>(this IReadOnlyCollection<TSource> source, Action<TSource> onNext, Action<Exception> onError)
        {
            return Countable.Create(EnumerableEx.Do(source, onNext, onError), () => source.Count);
        }

        public static IReadOnlyCollection<TSource> Do<TSource>(this IReadOnlyCollection<TSource> source, Action<TSource> onNext, Action<Exception> onError, Action onCompleted)
        {
            return Countable.Create(EnumerableEx.Do(source, onNext, onError, onCompleted), () => source.Count);
        }

        public static IReadOnlyCollection<TSource> Do<TSource>(this IReadOnlyCollection<TSource> source, IObserver<TSource> observer)
        {
            return Countable.Create(EnumerableEx.Do(source, observer), () => source.Count);
        }

        public static IReadOnlyCollection<TSource> Finally<TSource>(this IReadOnlyCollection<TSource> source, Action finallyAction)
        {
            return Countable.Create(EnumerableEx.Finally(source, finallyAction), () => source.Count);
        }

        public static IReadOnlyCollection<TSource> Hide<TSource>(this IReadOnlyCollection<TSource> source)
        {
            return Countable.Create(EnumerableEx.Hide(source), () => source.Count);
        }
        
        public static IReadOnlyCollection<TSource> IgnoreElements<TSource>(this IReadOnlyCollection<TSource> source)
        {
            return source.IgnoreElementsCountable();
        }

        public static IReadOnlyCollection<TSource> IgnoreElementsCountable<TSource>(this IEnumerable<TSource> source)
        {
            return Countable.Create(EnumerableEx.IgnoreElements(source), () => 0);
        }

        public static IReadOnlyBuffer<TSource> Memoize<TSource>(this IReadOnlyCollection<TSource> source)
        {
            return CreateBuffer(EnumerableEx.Memoize(source), () => source.Count);
        }

        public static IReadOnlyBuffer<TSource> Memoize<TSource>(this IReadOnlyCollection<TSource> source, int readerCount)
        {
            return CreateBuffer(EnumerableEx.Memoize(source, readerCount), () => source.Count);
        }

        public static IReadOnlyCollection<TSource> Repeat<TSource>(this IReadOnlyCollection<TSource> source, int count)
        {
            return Countable.Create(EnumerableEx.Repeat(source.AsEnumerable(), count), () => source.Count * count);
        }

        public static IReadOnlyCollection<TResult> Return<TResult>(TResult value)
        {
            return Countable.Create(EnumerableEx.Return(value), () => 1);
        }

        public static IReadOnlyCollection<TAccumulate> Scan<TSource, TAccumulate>(this IReadOnlyCollection<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> accumulator)
        {
            return Countable.Create(EnumerableEx.Scan(source, seed, accumulator), () => source.Count);
        }

        public static IReadOnlyCollection<TSource> Scan<TSource>(this IReadOnlyCollection<TSource> source, Func<TSource, TSource, TSource> accumulator)
        {
            return Countable.Create(EnumerableEx.Scan(source, accumulator), () => source.Count - 1);
        }

        public static IReadOnlyCollection<TOther> SelectMany<TSource, TOther>(this IReadOnlyCollection<TSource> source, IReadOnlyCollection<TOther> other)
        {
            return Countable.Create(EnumerableEx.SelectMany(source, other), () => source.Count * other.Count);
        }

        public static IReadOnlyCollection<TSource> SkipLast<TSource>(this IReadOnlyCollection<TSource> source, int count)
        {
            return Countable.Create(EnumerableEx.SkipLast(source, count), () => Math.Max(source.Count - count, 0));
        }

        public static IReadOnlyCollection<TSource> StartWith<TSource>(this IReadOnlyCollection<TSource> source, params TSource[] values)
        {
            return Countable.Create(EnumerableEx.StartWith(source, values), () => source.Count + values.Length);
        }

        public static IReadOnlyCollection<TSource> TakeLast<TSource>(this IReadOnlyCollection<TSource> source, int count)
        {
            return Countable.Create(EnumerableEx.TakeLast(source, count), () => Math.Min(source.Count, count));
        }

        public static IReadOnlyCollection<TResult> Throw<TResult>(Exception exception)
        {
            return Countable.Create(EnumerableEx.Throw<TResult>(exception), () => 0);
        }
    } 
}
