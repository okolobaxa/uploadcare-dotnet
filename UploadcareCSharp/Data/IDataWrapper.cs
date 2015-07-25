namespace UploadcareCSharp.Data
{
    public interface IDataWrapper<T, U>
    {
        T Wrap(U data);
    }
}
