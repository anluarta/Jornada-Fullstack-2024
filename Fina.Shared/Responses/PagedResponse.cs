using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fina.Shared.Responses
{
    public class PagedResponse<TData> : Response<TData>
    {

        [JsonConstructor]
        public PagedResponse(TData? data, int totalCount, int currentPage = 1, int pageSize = Configuration.DefaultPageSize) : base(data)
        {
            Data = data;
            TotalRecords = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public PagedResponse(TData? data, int code = Configuration.DefaultStatusCode, string? message = null) : base(data, code, message)
        {

        }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);

        public int PageSize { get; set; } = Configuration.DefaultPageSize;

        public int TotalRecords { get; set; }
    }
}
