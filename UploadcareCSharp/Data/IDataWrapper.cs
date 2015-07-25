namespace UploadcareCSharp.Data
{
    internal interface IDataWrapper<out T, in TU>
    {
        T Wrap(TU data);
    }
}
