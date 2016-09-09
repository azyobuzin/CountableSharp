using System;
using CountableSharp.Impl;
using Xunit;

namespace CountableSharp.Tests.Impl
{
    public class RangeTest
    {
        [Fact]
        public void Constructor()
        {
            var dummy = new Range(int.MaxValue, 1);
            dummy = new Range(int.MinValue, 1);
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => new Range(int.MaxValue, 2));
        }

        [Fact]
        public void Contains()
        {
            var range = new Range(int.MaxValue - 1, 2);
            Assert.False(range.Contains(int.MaxValue - 2));
            Assert.True(range.Contains(int.MaxValue - 1));
            Assert.True(range.Contains(int.MaxValue));
            Assert.False(range.Contains(unchecked(int.MaxValue + 1)));
        }

        [Fact]
        public void IndexerAndEnumeratorTest()
        {
            Utils.IndexerAndEnumeratorTestCore(
                new Range(0, 0),
                Array.Empty<int>()
            );

            Utils.IndexerAndEnumeratorTestCore(
                new Range(int.MinValue, 3),
                new[] { int.MinValue, int.MinValue + 1, int.MinValue + 2 }
            );

            Utils.IndexerAndEnumeratorTestCore(
                new Range(int.MaxValue - 2, 3),
                new[] { int.MaxValue - 2, int.MaxValue - 1, int.MaxValue }
            );
        }
    }
}
