using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    public class CategoryLanguageDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryDetailsId { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        [MaxLength(256)]
        public string CateogryName { get; set; }
        [MaxLength(256)]
        public string CateogryUrl { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
