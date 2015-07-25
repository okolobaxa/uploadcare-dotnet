using UploadcareCSharp.API;

namespace UploadcareCSharp.Data
{
    public class FileDataWrapper : IDataWrapper<UploadcareFile, FileData>
    {
        private readonly Client _client;

        public FileDataWrapper(Client client)
        {
            _client = client;
        }

        public UploadcareFile Wrap(FileData data)
        {
            return new UploadcareFile(_client, data);
        }
    }
}
