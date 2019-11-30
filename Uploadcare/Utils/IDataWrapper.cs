namespace Uploadcare.Utils
{
    internal interface IDataWrapper<out T, in TU>
    {
        T Wrap(TU data);
    }
}
