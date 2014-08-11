using System.Collections.Generic;
using System.Linq;

namespace CountableSharp
{
    public interface IReadOnlyBuffer<out T> : IBuffer<T>, IReadOnlyCollection<T>
    {
    }
}
