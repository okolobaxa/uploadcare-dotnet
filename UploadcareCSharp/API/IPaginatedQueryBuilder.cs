using System.Collections.Generic;

namespace UploadcareCSharp.API
{
    public interface IPaginatedQueryBuilder<T>
    {
        /**
     * Returns a resource iterable for lazy loading.
     */
        IEnumerable<T> AsIterable();

        /**
     * Iterates through all resources and returns a complete list.
     */
        List<T> AsList();
    }
}