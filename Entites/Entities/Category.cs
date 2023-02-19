using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    public class Category : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [MaxLength(64)]
        public string CategoryKey { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<CategoryLanguageDetails> CategoryDetails { get; set; }
    }
}
