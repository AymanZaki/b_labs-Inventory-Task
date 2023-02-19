using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserKey { get; set; }
        public string FullName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
