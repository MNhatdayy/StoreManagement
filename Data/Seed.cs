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

                            Name = "Owner",
                        },
                        new Role()
                        {

                            Name = "Customer",
                        },
                    });
                    context.SaveChanges();
                }
                var a = context.Roles.Where(obj => obj.Name.Equals("Admin")).Select(obj => obj.Id).FirstOrDefault();
                var b = context.Roles.Where(obj => obj.Name.Equals("Owner")).Select(obj => obj.Id).FirstOrDefault();
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
                if (!context.Stores.Any())
                {
                    context.Stores.AddRange(new List<Store>
                    {
                        new Store {StoreName = "Gogi", UserId = 2, AddressStore = "Bình Dương", IsDeleted = false},
                    });
                }
                /*if (!context.FoodCategories.Any())
                {
                    context.FoodCategories.AddRange(new List<FoodCategory>
                    {
                        new FoodCategory { Name = "Đồ nướng" , StoreId = 1, IsDeleted = false},
                        new FoodCategory { Name = "Lẩu" , StoreId = 1, IsDeleted = false },
                        new FoodCategory { Name = "Nước ngọt" , StoreId = 1, IsDeleted = false },
                    });
                    context.SaveChanges();
                }
                if (!context.FoodItems.Any())
                {
                    var category1 = context.FoodCategories.Where(obj => obj.Name.Equals("Đồ nướng")).Select(obj => obj.Id).FirstOrDefault();
                    var category2 = context.FoodCategories.Where(obj => obj.Name.Equals("Lẩu")).Select(obj => obj.Id).FirstOrDefault();
                    var category3 = context.FoodCategories.Where(obj => obj.Name.Equals("Nước ngọt")).Select(obj => obj.Id).FirstOrDefault(); ;
                    context.FoodItems.AddRange(new List<FoodItem>
                    {
                        new FoodItem()
                        {
                            Name = "Bò nướng",
                            Description = "Ngon",
                            Price = 20.99,
                            ImageUrl = "/images/food/donuong/1.jpg",
                            FoodCategoryId = category1,
                            IsDeleted = false,
                        },
                        new FoodItem()
                        {
                            Name = "Heo nướng",
                            Description = "Ngon",
                            Price = 15.99,
                            ImageUrl = "/images/food/donuong/2.jpg",
                            FoodCategoryId = category1,
                            IsDeleted = false,
                        },
                        new FoodItem()
                        {
                            Name = "Vịt nướng",
                            Description = "Ngon",
                            Price = 16.99,
                            ImageUrl = "/images/food/donuong/3.jpg",
                            FoodCategoryId = category1,
                            IsDeleted = false,
                        },
                        new FoodItem()
                        {
                            Name = "Lẩu thái",
                            Description = "Ngon",
                            Price = 50.99,
                            ImageUrl = "/images/food/lau/1.jpg",
                            FoodCategoryId = category2,
                            IsDeleted = false,
                        },
                        new FoodItem()
                        {
                            Name = "Lẩu cá",
                            Description = "Ngon",
                            Price = 50.99,
                            ImageUrl = "/images/food/lau/1.jpg",
                            FoodCategoryId = category2,
                            IsDeleted = false,
                        },
                        new FoodItem()
                        {
                            Name = "Lẩu hải sản",
                            Description = "Ngon",
                            Price = 50.99,
                            ImageUrl = "/images/food/lau/1.jpg",
                            FoodCategoryId = category2,
                            IsDeleted = false,
                        },
                        new FoodItem()
                        {
                            Name = "Lẩu bò",
                            Description = "Ngon",
                            Price = 50.99,
                            ImageUrl = "/images/food/lau/1.jpg",
                            FoodCategoryId = category2,
                            IsDeleted = false,
                        },
                        new FoodItem()
                        {
                            Name = "Coca-Cola",
                            Description = "Ngon",
                            Price = 7.99,
                            ImageUrl = "/images/food/nuoc/1.jpg",
                            FoodCategoryId = category3,
                            IsDeleted = false,
                        },
                        new FoodItem()
                        {
                            Name = "7Up",
                            Description = "Ngon",
                            Price = 7.99,
                            ImageUrl = "/images/food/nuoc/2.jpg",
                            FoodCategoryId = category3,
                            IsDeleted = false,
                        },
                        new FoodItem()
                        {
                            Name = "Pepsi",
                            Description = "Ngon",
                            Price = 7.99,
                            ImageUrl = "/images/food/nuoc/3.jpg",
                            FoodCategoryId = category3,
                            IsDeleted = false,
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.Menus.Any())
                {
                    var foodItem1 = context.FoodItems.Where(obj => obj.Name.Equals("Bò nướng")).Select(obj => obj.Id).FirstOrDefault();
                    var foodItem2 = context.FoodItems.Where(obj => obj.Name.Equals("Heo nướng")).Select(obj => obj.Id).FirstOrDefault();
                    var foodItem3 = context.FoodItems.Where(obj => obj.Name.Equals("Vịt nướng")).Select(obj => obj.Id).FirstOrDefault();
                    var foodItem4 = context.FoodItems.Where(obj => obj.Name.Equals("Lẩu thái")).Select(obj => obj.Id).FirstOrDefault();
                    var foodItem5 = context.FoodItems.Where(obj => obj.Name.Equals("Lẩu cá")).Select(obj => obj.Id).FirstOrDefault();
                    var foodItem6 = context.FoodItems.Where(obj => obj.Name.Equals("Lẩu hải sản")).Select(obj => obj.Id).FirstOrDefault();
                    var foodItem7 = context.FoodItems.Where(obj => obj.Name.Equals("Lẩu bò")).Select(obj => obj.Id).FirstOrDefault();
                    var foodItem8 = context.FoodItems.Where(obj => obj.Name.Equals("Coca-Cola")).Select(obj => obj.Id).FirstOrDefault();
                    var foodItem9 = context.FoodItems.Where(obj => obj.Name.Equals("7Up")).Select(obj => obj.Id).FirstOrDefault();
                    var foodItem10 = context.FoodItems.Where(obj => obj.Name.Equals("Pepsi")).Select(obj => obj.Id).FirstOrDefault();

                    context.Menus.AddRange(new List<Menu>
                    {
                        new Menu()
                        {
                            CreateTime = DateTime.Now,
                            StoreId = 1,
                            IsDeleted = false,
                        },
                        
                    });
                    context.SaveChanges();
                }*/

            }
        }
    }
}
