using Microsoft.EntityFrameworkCore;
using Drsfan.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Drsfan.DataAcess.Data
{
    public class DrsfanDbContext : IdentityDbContext<IdentityUser>
    {
        public DrsfanDbContext(DbContextOptions<DrsfanDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


        // OnModelCreating method to seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data for Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Washing Machines", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Refrigerators", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Microwaves", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Fans", DisplayOrder = 4 }
            );

            // Seed Data for Products 
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "LG 8kg Washing Machine",
                    Description = "Front load washing machine, inverter technology, energy-efficient.",
                    Brand = "LG",
                    ModelNumber = "LG1234",
                    CategoryId = 1, // Category "Washing Machines"
                    ListPrice = 349.99, 
                    DiscountPrice = 299.99,
                    WarrantyPeriod = "24 months",
                    Features = "Automatic wash mode, quick wash, energy-saving",
                    PowerConsumption = "500W",
                    ImageUrl = "/images/products/lggiat.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Samsung 250L Refrigerator",
                    Description = "Double-door refrigerator, 250L capacity, energy-saving technology.",
                    Brand = "Samsung",
                    ModelNumber = "Samsung250L",
                    CategoryId = 2, // Category "Refrigerators"
                    ListPrice = 499.99, 
                    DiscountPrice = 459.99,
                    WarrantyPeriod = "12 months",
                    Features = "Quick cooling, energy-efficient",
                    PowerConsumption = "150W",
                    ImageUrl = "/images/products/samsungtu.jpg"
                },
                new Product
                {
                    Id = 3,
                    Name = "Sharp 23L Microwave",
                    Description = "Sharp 23L microwave with grilling function, time-saving cooking.",
                    Brand = "Sharp",
                    ModelNumber = "Sharp23L",
                    CategoryId = 3, // Category "Microwaves"
                    ListPrice = 119.99, 
                    DiscountPrice = 99.99,
                    WarrantyPeriod = "12 months",
                    Features = "Grilling and microwave functions, easy to use",
                    PowerConsumption = "800W",
                    ImageUrl = "/images/products/sharplosong.jpg"
                },
                new Product
                {
                    Id = 4,
                    Name = "Panasonic 16-inch Fan",
                    Description = "Panasonic stand fan with 3 wind modes, durable motor.",
                    Brand = "Panasonic",
                    ModelNumber = "Panasonic16",
                    CategoryId = 4, // Category "Fans"
                    ListPrice = 39.99, 
                    DiscountPrice = 29.99,
                    WarrantyPeriod = "12 months",
                    Features = "Strong wind modes, energy-saving",
                    PowerConsumption = "50W",
                    ImageUrl = "/images/products/panasonicquat.jpg"
                }
            );
        }
    }
}
