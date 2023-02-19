using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    public class Language
    {
        [Key]
        public int LanguageId { get; set; }
        [MaxLength(64)]
        public string LanguageName { get; set; }
        public bool IsActive { get; set; }
    }
}
