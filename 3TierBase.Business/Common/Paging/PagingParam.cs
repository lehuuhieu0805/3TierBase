using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _3TierBase.Business.Commons.Paging
{
    public class PagingParam<T> where T : System.Enum
    {
        private int _pageIndex = PagingConstant.FixedPagingConstant.DefaultPageIndex;

        [FromQuery(Name = "page-index")]
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value;
        }

#nullable enable
        [FromQuery(Name = "page-size")]
        [DefaultValue(PagingConstant.FixedPagingConstant.DefaultPageSize)]
        public int? PageSize { get; set; }

#nullable enable
        [FromQuery(Name = "sort-key")]
        public T? SortKey { get; set; }

        [FromQuery(Name = "sort-order")]
        [EnumDataType(typeof(PagingConstant.OrderCriteria))]
        [JsonConverter(typeof(StringEnumConverter))]
        public PagingConstant.OrderCriteria SortOrder { get; set; }
    }
}
