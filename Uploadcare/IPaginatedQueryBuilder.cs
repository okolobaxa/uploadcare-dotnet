using System.Collections.Generic;

namespace Uploadcare
{
    internal interface IPaginatedQueryBuilder<T>
    {
        /// <summary>
        /// Returns a resource iterable for lazy loading.
        /// </summary>
        IEnumerable<T> AsEnumerable();

        /// <summary>
        /// Iterates through all resources and returns a complete list.
        /// </summary>
        List<T> AsList();
    }
}