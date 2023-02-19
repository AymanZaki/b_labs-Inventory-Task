using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    public class ProductLanguageDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductDetailsId { get; set; }
        public int ProductId { get; set; }
        public int LanguageId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }

        [ForeignKey("ProductId")] 
        public Product Product { get; set; }
    }
}
