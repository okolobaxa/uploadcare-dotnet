using System.Collections.Generic;

namespace UploadcareCSharp.Data
{
    internal interface IPageData<TU>
    {
        List<TU> GetResults();
        bool HasMore();
    }
}
