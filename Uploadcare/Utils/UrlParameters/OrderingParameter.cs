using System;

namespace Uploadcare.Utils.UrlParameters
{
    internal abstract class BaseOrderingParameter : IUrlParameter
    {
        public string GetParam()
        {
            return "ordering";
        }

        public abstract string GetValue();
    }

    internal class FileOrderingParameter : BaseOrderingParameter
    {
        private readonly EFileOrderBy _orderBy;

        public FileOrderingParameter(EFileOrderBy orderBy)
        {
            _orderBy = orderBy;
        }


        public override string GetValue()
        {
            switch (_orderBy)
            {
                case EFileOrderBy.DatetimeUploaded: return "datetime_uploaded";
                case EFileOrderBy.DatetimeUploadedDesc: return "-datetime_uploaded";
                case EFileOrderBy.Size: return "size";
                case EFileOrderBy.SizeDesc: return "-size";
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }

    internal class GroupOrderingParameter : BaseOrderingParameter
    {
        private readonly EGroupOrderBy _orderBy;

        public GroupOrderingParameter(EGroupOrderBy orderBy)
        {
            _orderBy = orderBy;
        }

        public override string GetValue()
        {
            switch (_orderBy)
            {
                case EGroupOrderBy.DatetimeCreated: return "datetime_created";
                case EGroupOrderBy.DatetimeCreatedDesc: return "-datetime_created";
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
