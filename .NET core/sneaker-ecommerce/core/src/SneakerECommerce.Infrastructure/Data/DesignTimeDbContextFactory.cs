using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sneaker_ECommerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerECommerce.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=SneakerECommerce;TrustServerCertificate=Yes;Trusted_Connection=True");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
