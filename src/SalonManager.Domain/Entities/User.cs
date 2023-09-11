using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonManager.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }

    }
}
