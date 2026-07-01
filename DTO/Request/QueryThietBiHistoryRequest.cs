using System;

namespace QlThietBi.DTO.Request
{
    public class QueryThietBiHistoryRequest
    {
        public Guid ThietBiId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}