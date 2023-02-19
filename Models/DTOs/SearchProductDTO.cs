using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class SearchProductDTO
    {
        public int Page { get; set; } = Constants.Page;
        public int PageSize { get; set; } = Constants.PageSize;
        public string SearchKeyword { get; set; }
        public int? UserId { get; set; }
    }
}
