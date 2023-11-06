using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SalonManager.Domain.Entities;

namespace SalonManager.Infra.Data.Context
{
    public class SalonManagerDbContext : DbContext
    { 

        public SalonManagerDbContext(DbContextOptions<SalonManagerDbContext> options)
        : base(options)
        {
            //Customers = new List<Customer>();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<SalonService> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
