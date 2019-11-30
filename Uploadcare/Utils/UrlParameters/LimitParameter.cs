namespace Uploadcare.Utils.UrlParameters
{
    internal class LimitParameter : IUrlParameter
    {
        private readonly int _limit;

        public LimitParameter(int limit)
        {
            _limit = limit;
        }

        public string GetParam()
        {
            return "limit";
        }

        public string GetValue()
        {
            return _limit.ToString();
        }
    }
}
