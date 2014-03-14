using System;
using System.Collections.Generic;
using System.Linq;

namespace CountableSharp
{
    /// <summary>
    /// 並べ替えられた有限コレクションを表します。
    /// </summary>
    /// <typeparam name="TElement">シーケンスの要素の型。</typeparam>
    public interface IOrderedReadOnlyCollection<TElement> : IReadOnlyCollection<TElement>, IOrderedEnumerable<TElement>
    {
        /// <summary>
        /// キーに従って <see cref="CountableSharp.IOrderedReadOnlyCollection{TElement}"/> の要素に対して後続の並べ替えを実行します。
        /// </summary>
        IOrderedReadOnlyCollection<TElement> CreateOrderedReadOnlyCollection<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending);
    }
}
