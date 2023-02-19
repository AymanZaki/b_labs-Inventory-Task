using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CategoryLanguageDetailsDTO
    {
        public int LanguageId { get; set; }
        public string CateogryName { get; set; }
        public string CateogryUrl { get; set; }
    }
}
