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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }

            );

            modelBuilder.Entity<Company>().HasData(
            new Company
            {
                Id = 1,
                Name = "Tech Solutions",
                Address = "123 Tech Avenue",
                City = "Techville",
                State = "CA",
                PostalCode = "90001",
                PhoneNumber = "555-123-4567"
            },
            new Company
            {
                Id = 2,
                Name = "Global Innovations",
                Address = "456 Global Street",
                City = "Innovate City",
                State = "TX",
                PostalCode = "73301",
                PhoneNumber = "555-987-6543"
            },
            new Company
            {
                Id = 3,
                Name = "EcoWorld Corp",
                Address = "789 Green Lane",
                City = "EcoCity",
                State = "NY",
                PostalCode = "10001",
                PhoneNumber = "555-321-6789"
            }
            );


            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Description = "A novel written by F. Scott Fitzgerald.",
                    ISBN = "978-0743273565",
                    Author = "F. Scott Fitzgerald",
                    ListPrice = 10.99,
                    Price = 9.99,
                    Price50 = 8.99,
                    Price100 = 7.99,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Description = "A novel by Harper Lee published in 1960.",
                    ISBN = "978-0061120084",
                    Author = "Harper Lee",
                    ListPrice = 14.99,
                    Price = 13.99,
                    Price50 = 12.99,
                    Price100 = 11.99,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "1984",
                    Description = "A dystopian novel by George Orwell.",
                    ISBN = "978-0451524935",
                    Author = "George Orwell",
                    ListPrice = 9.99,
                    Price = 8.99,
                    Price50 = 7.99,
                    Price100 = 6.99,
                    CategoryId = 3,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "The Catcher in the Rye",
                    Description = "A novel by J.D. Salinger.",
                    ISBN = "978-0316769488",
                    Author = "J.D. Salinger",
                    ListPrice = 8.99,
                    Price = 7.99,
                    Price50 = 6.99,
                    Price100 = 5.99,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Pride and Prejudice",
                    Description = "A romantic novel by Jane Austen.",
                    ISBN = "978-1503290563",
                    Author = "Jane Austen",
                    ListPrice = 11.99,
                    Price = 10.99,
                    Price50 = 9.99,
                    Price100 = 8.99,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 6,
                    Title = "The Hobbit",
                    Description = "A fantasy novel by J.R.R. Tolkien.",
                    ISBN = "978-0547928227",
                    Author = "J.R.R. Tolkien",
                    ListPrice = 12.99,
                    Price = 11.99,
                    Price50 = 10.99,
                    Price100 = 9.99,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 7,
                    Title = "Moby Dick",
                    Description = "A novel by Herman Melville.",
                    ISBN = "978-1503280786",
                    Author = "Herman Melville",
                    ListPrice = 13.99,
                    Price = 12.99,
                    Price50 = 11.99,
                    Price100 = 10.99,
                    CategoryId = 3,
                    ImageUrl = ""
                }
            );

        }
    }
}
