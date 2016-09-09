using System;
using System.Collections.Generic;
using Xunit;

namespace CountableSharp.Tests
{
    internal static class Utils
    {
        public static void IndexerAndEnumeratorTestCore<T>(IReadOnlyList<T> list, T[] expected)
        {
            using (var enumerator = list.GetEnumerator())
            {
                for (var i = 0; i < list.Count; i++)
                {
                    Assert.True(enumerator.MoveNext());
                    Assert.Equal(expected[i], enumerator.Current);
                    Assert.Equal(expected[i], list[i]);
                }

                Assert.False(enumerator.MoveNext());
                Assert.Throws(typeof(ArgumentOutOfRangeException), () => list[list.Count]);
            }
        }
    }
}
