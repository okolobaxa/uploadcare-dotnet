namespace UploadcareCSharp.Url.UrlParameters
{
    internal class FilesRemovedParameter: IUrlParameter
    {
        private readonly bool _removed;

        public FilesRemovedParameter(bool removed)
        {
            _removed = removed;
        }

        public string GetParam()
        {
            return "removed";
        }

        public string GetValue()
        {
            return _removed ? "true" : "false";
        }
    }
}
