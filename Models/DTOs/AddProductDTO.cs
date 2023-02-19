using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class AddProductDTO
    {
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public string CategoryKey { get; set; }
        public IEnumerable<ProductLanguageDetailsDTO> ProductLanguageDetails { get; set; }

    }
}
