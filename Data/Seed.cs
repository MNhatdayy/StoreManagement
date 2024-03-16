using StoreManagement.Models;

namespace StoreManagement.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new List<Role>()
                    {
                        new Role()
                        {
                            Name = "Admin",
                        },
                        new Role()
                        {

                            Name = "Staff",
                        },
                        new Role()
                        {

                            Name = "Customer",
                        },
                    });
                    context.SaveChanges();
                }
                var a = context.Roles.Where(obj => obj.Name.Equals("Admin")).Select(obj => obj.Id).FirstOrDefault();
                var b = context.Roles.Where(obj => obj.Name.Equals("Staff")).Select(obj => obj.Id).FirstOrDefault();
                var c = context.Roles.Where(obj => obj.Name.Equals("Customer")).Select(obj => obj.Id).FirstOrDefault();
                if (!context.AppUsers.Any())
                {
                    context.AppUsers.AddRange(new List<AppUser>()
                    {
                        new AppUser()
                        {
                            Email = "nhatnm2003@gmail.com",
                            Name = "NhatVipPro",
                            Address = "Thu Duc",
                            Password = "Nhat2003@",
                            PhoneNumber = "1234567890",
                            IsDeleted = false,
                            RoleId = b,

                        },
                        new AppUser()
                        {
                            Email = "Thiennamky@gmail.com",
                            Name = "ThienNamkiki",
                            Address = "Nam Ki",
                            Password = "Thonlien@",
                            PhoneNumber = "1234567890",
                            IsDeleted = false,
                            RoleId = a,
                        },
                        new AppUser()
                        {
                            Email = "Tuandaubuoi@gmail.com",
                            Name = "Tuancak",
                            Address = "DakNong",
                            Password = "Tuandb@",
                            PhoneNumber = "1234567890",
                            IsDeleted = false,
                            RoleId = c,
                        }
                    });
                    context.SaveChanges();
                }

            }
        }
    }
}
