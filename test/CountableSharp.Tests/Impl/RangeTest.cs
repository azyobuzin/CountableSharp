using System;
using System.Linq;
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

        [Theory]
        [InlineData(int.MaxValue - 1, 2)]
        [InlineData(int.MinValue, 2)]
        public void Contains(int start, int count)
        {
            var range = new Range(start, count);

            Assert.False(range.Contains(start - 1));

            for (var i = 0; i < count; i++)
                Assert.True(range.Contains(start + i));

            Assert.False(range.Contains(start + count));
        }

        [Fact]
        public void Empty()
        {
            Utils.IndexerAndEnumeratorTestCore(
                new Range(0, 0),
                Array.Empty<int>()
            );
        }

        [Theory]
        [InlineData(int.MinValue, 3)]
        [InlineData(int.MaxValue - 2, 3)]
        public void IndexerAndEnumerator(int start, int count)
        {
            Utils.IndexerAndEnumeratorTestCore(
                new Range(start, count),
                Enumerable.Range(start, count).ToArray()
            );
        }
    }
}
