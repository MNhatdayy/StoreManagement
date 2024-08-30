
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sneaker_ECommerce.Infrastructure.Data;

namespace SneakerECommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
            });

            return services;
        }
    }
}
