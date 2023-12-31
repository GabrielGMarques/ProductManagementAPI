﻿
namespace ProductManagement.Domain.Shared.Responses
{
    public class PaginatedResult<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
