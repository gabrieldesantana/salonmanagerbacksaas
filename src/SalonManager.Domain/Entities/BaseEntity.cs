using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonManager.Domain.Entities
{
    public class BaseEntity
    {

        public BaseEntity()
        {
            CreatedAt= DateTime.Now;
            Actived = true;
        }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Actived { get; set; }
    }
}
