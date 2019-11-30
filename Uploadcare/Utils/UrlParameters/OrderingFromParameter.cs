using System;

namespace Uploadcare.Utils.UrlParameters
{
    internal class OrderingFromSizeParameter : IUrlParameter
    {
        private readonly long _size;

        public OrderingFromSizeParameter(long size)
        {
            _size = size;
        }

        public string GetParam()
        {
            return "from";
        }

        public string GetValue()
        {
            return _size.ToString();
        }
    }

    internal class OrderingFromDateParameter : IUrlParameter
    {
        private readonly DateTime _timestamp;

        public OrderingFromDateParameter(DateTime timestamp)
        {
            _timestamp = timestamp;
        }

        public string GetParam()
        {
            return "from";
        }

        public string GetValue()
        {
            return _timestamp.ToString("o");
        }
    }
}
