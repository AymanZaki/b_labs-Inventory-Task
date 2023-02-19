using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PageModel<T> where T : class
    {
        public int TotalCount { get; set; }
        public int PagesCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
