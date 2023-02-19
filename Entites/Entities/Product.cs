using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    public class Product : BaseEntity
    {
        public int ProductId { get; set; }
        public string ProductKey { get; set; }
        public int CategoryId { get; set; }
        public int ProductStatusId { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [ForeignKey("ProductStatusId")]
        public ProductStatus ProductStatus{ get; set; }
        public IEnumerable<ProductLanguageDetails> ProductLanguageDetails { get; set; }
        public ProductRating ProductRating { get; set; }

    }
}
