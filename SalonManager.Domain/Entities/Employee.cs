using SalonManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonManager.Domain.Entities
{
    public class Employee : User
    {
        public int UserId { get; set; }


        //public string? Name { get; set; }
        //public string? CompanyName { get; set; }
        //public EUserRole Role { get; set; }
        //public string? Login { get; set; }
        //public string? Email { get; set; }
        //public string? Password { get; set; }
    }
}
