using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductKey { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public IEnumerable<ProductLanguageDetailsDTO> ProductLanguageDetails { get; set; }
        public double Rating { get; set; }
        public ProductStatusDTO ProductStatus { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
