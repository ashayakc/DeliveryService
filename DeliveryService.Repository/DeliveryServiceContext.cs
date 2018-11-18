using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Models
{
    public class DeliveryServiceContext : DbContext
    {
        public DeliveryServiceContext (DbContextOptions<DeliveryServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
	}
}
