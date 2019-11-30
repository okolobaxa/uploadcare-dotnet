using System.Collections.Generic;

namespace Uploadcare.Utils
{
    internal interface IPageData<TU>
    {
        List<TU> GetResults();
        bool HasMore();

        int Total { get; }
    }
}
