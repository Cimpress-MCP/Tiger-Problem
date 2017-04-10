using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using static System.StringComparison;

namespace Tiger.Problem.Test
{
    /// <summary>
    /// Determines the equality of two instances of <see cref="Problem"/>
    /// in a testing context.
    /// </summary>
    public class ProblemTestEqualityComparer
        : IEqualityComparer<Problem>
    {
        /// <inheritdoc />
        [SuppressMessage("ReSharper", "InvokeAsExtensionMethod")]
        public bool Equals([CanBeNull] Problem x, [CanBeNull] Problem y)
        {
            if (ReferenceEquals(x, y)) { return true; }
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            return x.Type == y.Type &&
                   x.Status == y.Status &&
                   string.Equals(x.Detail, y.Detail, Ordinal) &&
                   x.Instance == y.Instance &&
                   Enumerable.SequenceEqual(x.Extensions, y.Extensions);

        }

        /// <inheritdoc />
        public int GetHashCode([CanBeNull] Problem obj) =>
            // note(cosborn) The default is fine here.
            obj?.GetHashCode() ?? 0;
    }
}