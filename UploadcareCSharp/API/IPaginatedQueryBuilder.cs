using System.Collections.Generic;

namespace UploadcareCSharp.API
{
    public interface IPaginatedQueryBuilder<T>
    {
        /// <summary>
        /// Returns a resource iterable for lazy loading.
        /// </summary>
        IEnumerable<T> AsIterable();

        /// <summary>
        /// Iterates through all resources and returns a complete list.
        /// </summary>
        List<T> AsList();
    }
}