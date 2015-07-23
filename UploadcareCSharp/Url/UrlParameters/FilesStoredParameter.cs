namespace UploadcareCSharp.Url.UrlParameters
{
    internal class FilesStoredParameter : IUrlParameter
    {
        private readonly bool _stored;

        public FilesStoredParameter(bool stored)
        {
            _stored = stored;
        }

        public string GetParam()
        {
            return "stored";
        }

        public string GetValue()
        {
            return _stored ? "true" : "false";
        }
    }
}
