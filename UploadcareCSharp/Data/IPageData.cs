using System.Collections.Generic;

namespace UploadcareCSharp.Data
{
    public interface IPageData<U>
    {
        List<U> GetResults();
        bool HasMore();
    }
}
