using System;

namespace QlThietBi.DTO.Request
{
    public class QueryThietBiHistoryRequest
    {
        public int ThietBiId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}

