using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryKey { get; set; }
        public CategoryLanguageDetailsDTO CategoryLanguageDetails { get; set; }
    }
}
