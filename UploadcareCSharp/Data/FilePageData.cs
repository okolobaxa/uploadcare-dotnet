using System.Collections.Generic;

namespace UploadcareCSharp.Data
{
    public class FilePageData : IPageData<FileData>
    {
        public int page;
        public int pages;
        public List<FileData> results;

        public List<FileData> GetResults()
        {
            return results;
        }

        public bool HasMore()
        {
            return page < pages;
        }
    }
}
