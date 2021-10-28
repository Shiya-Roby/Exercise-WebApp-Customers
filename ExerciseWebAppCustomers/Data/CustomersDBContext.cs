using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExerciseWebAppCustomers.Models;

namespace ExerciseWebAppCustomers.Data
{
    public class CustomersDBContext : DbContext
    {
        public CustomersDBContext (DbContextOptions<CustomersDBContext> options)
            : base(options)
        {
        }

        public DbSet<ExerciseWebAppCustomers.Models.Customer> Customer { get; set; }
    }
}
