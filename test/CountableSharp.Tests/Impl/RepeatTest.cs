using System;
using CountableSharp.Impl;
using Xunit;

namespace CountableSharp.Tests.Impl
{
    public class RepeatTest
    {
        [Fact]
        public void IndexerAndEnumeratorTest()
        {
            const int value = 42;

            Utils.IndexerAndEnumeratorTestCore(
                new Repeat<int>(value, 0),
                Array.Empty<int>()
            );

            Utils.IndexerAndEnumeratorTestCore(
                new Repeat<int>(value, 3),
                new[] { value, value, value }
            );
        }
    }
}
