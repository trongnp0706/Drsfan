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
        public DbSet<ProductImage> ProductImages { get; set; }
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
                new Category { Id = 3, Name = "Air Conditioners", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Microwave Ovens", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Vacuum Cleaners", DisplayOrder = 5 },
                new Category { Id = 6, Name = "Water Purifiers", DisplayOrder = 6 },
                new Category { Id = 7, Name = "Dishwashers", DisplayOrder = 7 },
                new Category { Id = 8, Name = "Electric Kettles", DisplayOrder = 8 },
                new Category { Id = 9, Name = "Coffee Makers", DisplayOrder = 9 },
                new Category { Id = 10, Name = "Rice Cookers", DisplayOrder = 10 }
            );


            // Seed Data for Products 
            modelBuilder.Entity<Product>().HasData(
                // Category 1: Washing Machines
                new Product
                {
                    Id = 1,
                    Name = "Samsung EcoBubble Front Load Washer",
                    Description = "The Samsung EcoBubble Washer offers powerful cleaning with its unique EcoBubble technology, which transforms detergent into bubbles for faster penetration into fabrics. It ensures effective cleaning even at low temperatures, saving energy while protecting delicate clothing.",
                    Brand = "Samsung",
                    ModelNumber = "WW70J3280KW",
                    ListPrice = 450.00,
                    DiscountPrice = 399.99,
                    WarrantyPeriod = "2 years",
                    CategoryId = 1,
                    Features = "Hygiene Steam, Digital Inverter, EcoBubble Technology",
                    PowerConsumption = "200W"
                },
                new Product
                {
                    Id = 2,
                    Name = "LG Smart Inverter Top Load Washer",
                    Description = "Designed for ultimate convenience, the LG Smart Inverter Washer delivers low noise and high energy efficiency. Its TurboDrum technology ensures powerful washing, while the Smart Diagnosis feature allows you to troubleshoot issues directly from your smartphone.",
                    Brand = "LG",
                    ModelNumber = "T80SJMB1Z",
                    ListPrice = 500.00,
                    DiscountPrice = 449.99,
                    WarrantyPeriod = "3 years",
                    CategoryId = 1,
                    Features = "TurboDrum, Smart Diagnosis, Smart Inverter Control",
                    PowerConsumption = "250W"
                },

                // Category 2: Refrigerators
                new Product
                {
                    Id = 3,
                    Name = "Samsung Double Door Refrigerator",
                    Description = "This spacious Samsung refrigerator features a frost-free design and a digital inverter compressor for long-lasting cooling. The Twin Cooling technology maintains optimal humidity levels, keeping fruits and vegetables fresh for longer periods.",
                    Brand = "Samsung",
                    ModelNumber = "RT30T3722S8",
                    ListPrice = 600.00,
                    DiscountPrice = 549.99,
                    WarrantyPeriod = "3 years",
                    CategoryId = 2,
                    Features = "Twin Cooling, MoistFresh Zone, Power Freeze",
                    PowerConsumption = "350W"
                },
                new Product
                {
                    Id = 4,
                    Name = "Whirlpool Multi-Door Refrigerator",
                    Description = "The Whirlpool Multi-Door Refrigerator offers advanced cooling technology, ensuring every corner of the fridge maintains a consistent temperature. Its Zeolite Technology absorbs excess moisture, preventing fruits and vegetables from getting soggy.",
                    Brand = "Whirlpool",
                    ModelNumber = "IF INV CNV 515",
                    ListPrice = 699.99,
                    DiscountPrice = 649.99,
                    WarrantyPeriod = "3 years",
                    CategoryId = 2,
                    Features = "Zeolite Technology, FreshFlow Air Tower, Adaptive Intelligence",
                    PowerConsumption = "400W"
                },

                // Category 3: Air Conditioners
                new Product
                {
                    Id = 5,
                    Name = "Daikin Split AC with Inverter Technology",
                    Description = "The Daikin Split Air Conditioner combines sleek design with high performance. Its PM2.5 air filter ensures cleaner air indoors, while its energy-efficient inverter technology reduces electricity bills. Perfect for all weather conditions.",
                    Brand = "Daikin",
                    ModelNumber = "FTKF50TV",
                    ListPrice = 700.00,
                    DiscountPrice = 649.99,
                    WarrantyPeriod = "5 years",
                    CategoryId = 3,
                    Features = "PM2.5 Filter, Quiet Operation, High-Efficiency Cooling",
                    PowerConsumption = "1500W"
                },
                new Product
                {
                    Id = 6,
                    Name = "LG DualCool Split AC",
                    Description = "With Dual Inverter Compressor technology, the LG DualCool Split AC ensures faster and quieter cooling. It features Monsoon Comfort for humid weather and an Anti-Bacterial Filter for improved air quality.",
                    Brand = "LG",
                    ModelNumber = "KS-Q18SNZD",
                    ListPrice = 750.00,
                    DiscountPrice = 699.99,
                    WarrantyPeriod = "10 years",
                    CategoryId = 3,
                    Features = "Anti-Bacterial Filter, Dual Inverter, Monsoon Comfort",
                    PowerConsumption = "1450W"
                },

                // Category 4: Microwave Ovens
                new Product
                {
                    Id = 7,
                    Name = "Panasonic Convection Microwave Oven",
                    Description = "The Panasonic Convection Microwave Oven offers versatile cooking options, from grilling to baking. Its sleek design fits perfectly into modern kitchens, while the Auto Cook menu simplifies the preparation of your favorite dishes.",
                    Brand = "Panasonic",
                    ModelNumber = "NN-CT645BFDG",
                    ListPrice = 200.00,
                    DiscountPrice = 179.99,
                    WarrantyPeriod = "2 years",
                    CategoryId = 4,
                    Features = "Convection Cooking, Auto Cook Menu, Touch Keypad",
                    PowerConsumption = "1200W"
                },
                new Product
                {
                    Id = 8,
                    Name = "Samsung Solo Microwave Oven",
                    Description = "Compact and easy to use, the Samsung Solo Microwave Oven is perfect for reheating, defrosting, and simple cooking. Its ceramic enamel cavity ensures easy cleaning and durability.",
                    Brand = "Samsung",
                    ModelNumber = "MS23J5133AG",
                    ListPrice = 150.00,
                    DiscountPrice = 129.99,
                    WarrantyPeriod = "1 year",
                    CategoryId = 4,
                    Features = "Ceramic Enamel Cavity, Quick Defrost, Reheat Function",
                    PowerConsumption = "1150W"
                },

                // Category 5: Vacuum Cleaners
                new Product
                {
                    Id = 9,
                    Name = "Dyson V11 Cordless Vacuum Cleaner",
                    Description = "Experience powerful suction and unmatched convenience with the Dyson V11 Cordless Vacuum Cleaner. It offers multiple cleaning modes and a long battery life, making it ideal for homes with pets and children.",
                    Brand = "Dyson",
                    ModelNumber = "V11 Absolute Pro",
                    ListPrice = 900.00,
                    DiscountPrice = 850.00,
                    WarrantyPeriod = "2 years",
                    CategoryId = 5,
                    Features = "HEPA Filtration, Cordless Design, Multiple Cleaning Modes",
                    PowerConsumption = "545W"
                },
                new Product
                {
                    Id = 10,
                    Name = "Philips Bagless Vacuum Cleaner",
                    Description = "Keep your home spotless with the Philips Bagless Vacuum Cleaner. Its PowerCyclone 5 technology delivers high suction efficiency, and the ergonomic design ensures easy handling.",
                    Brand = "Philips",
                    ModelNumber = "FC9352/01",
                    ListPrice = 200.00,
                    DiscountPrice = 180.00,
                    WarrantyPeriod = "2 years",
                    CategoryId = 5,
                    Features = "Bagless, PowerCyclone 5, Advanced Dust Bin Design",
                    PowerConsumption = "1900W"
                },
                // Category 6: Dishwashers
                new Product
                {
                    Id = 11,
                    Name = "Bosch Freestanding Dishwasher",
                    Description = "The Bosch Freestanding Dishwasher is a powerful kitchen appliance, designed to save time and water while providing superior cleaning. With multiple wash programs and a sleek design, it's perfect for modern homes.",
                    Brand = "Bosch",
                    ModelNumber = "SMS46KI03I",
                    ListPrice = 700.00,
                    DiscountPrice = 650.00,
                    WarrantyPeriod = "2 years",
                    CategoryId = 6,
                    Features = "EcoSilence Drive, AquaStop, Load Sensor",
                    PowerConsumption = "1200W"
                },
                new Product
                {
                    Id = 12,
                    Name = "IFB Neptune VX Dishwasher",
                    Description = "The IFB Neptune VX Dishwasher offers an energy-efficient and noise-free cleaning experience. Its Jet Wash program ensures quick cleaning in just 18 minutes, making it ideal for busy households.",
                    Brand = "IFB",
                    ModelNumber = "Neptune VX",
                    ListPrice = 600.00,
                    DiscountPrice = 580.00,
                    WarrantyPeriod = "2 years",
                    CategoryId = 6,
                    Features = "Jet Wash, Adjustable Racks, EcoWash Mode",
                    PowerConsumption = "1100W"
                },

                // Category 7: Coffee Makers
                new Product
                {
                    Id = 13,
                    Name = "De'Longhi Espresso Coffee Machine",
                    Description = "Enjoy barista-quality coffee at home with the De'Longhi Espresso Coffee Machine. It features a 15-bar pressure system and a manual frother for creamy cappuccinos and lattes.",
                    Brand = "De'Longhi",
                    ModelNumber = "EC685.R",
                    ListPrice = 400.00,
                    DiscountPrice = 350.00,
                    WarrantyPeriod = "1 year",
                    CategoryId = 7,
                    Features = "15-bar Pump, Manual Frother, Compact Design",
                    PowerConsumption = "1450W"
                },
                new Product
                {
                    Id = 14,
                    Name = "Nespresso Vertuo Plus Coffee Maker",
                    Description = "The Nespresso Vertuo Plus offers convenience and versatility, allowing you to brew a variety of coffee sizes. Its Centrifusion technology ensures optimal flavor extraction with every cup.",
                    Brand = "Nespresso",
                    ModelNumber = "GCA1-US-BM-NE",
                    ListPrice = 250.00,
                    DiscountPrice = 220.00,
                    WarrantyPeriod = "1 year",
                    CategoryId = 7,
                    Features = "Centrifusion Technology, One-Touch Brewing, Large Water Tank",
                    PowerConsumption = "1350W"
                },

                // Category 8: Electric Kettles
                new Product
                {
                    Id = 15,
                    Name = "Philips Electric Kettle",
                    Description = "Boil water in minutes with the Philips Electric Kettle. It features a stainless steel design, 1.5-liter capacity, and an automatic shut-off function for safety.",
                    Brand = "Philips",
                    ModelNumber = "HD9306/06",
                    ListPrice = 40.00,
                    DiscountPrice = 35.00,
                    WarrantyPeriod = "1 year",
                    CategoryId = 8,
                    Features = "Stainless Steel, 1.5L Capacity, Automatic Shut-Off",
                    PowerConsumption = "2000W"
                },
                new Product
                {
                    Id = 16,
                    Name = "Morphy Richards InstaCook Kettle",
                    Description = "The Morphy Richards InstaCook Electric Kettle is perfect for boiling water, making instant noodles, or preparing beverages. Its cool-touch handle ensures safe operation.",
                    Brand = "Morphy Richards",
                    ModelNumber = "Instacook",
                    ListPrice = 50.00,
                    DiscountPrice = 45.00,
                    WarrantyPeriod = "1 year",
                    CategoryId = 8,
                    Features = "Multi-Purpose, Cool-Touch Handle, 1L Capacity",
                    PowerConsumption = "1800W"
                },

                // Category 9: Food Processors
                new Product
                {
                    Id = 17,
                    Name = "Cuisinart 14-Cup Food Processor",
                    Description = "The Cuisinart Food Processor simplifies meal preparation with its large 14-cup capacity and multiple blade attachments. It's perfect for chopping, slicing, and kneading dough.",
                    Brand = "Cuisinart",
                    ModelNumber = "DFP-14BCNY",
                    ListPrice = 250.00,
                    DiscountPrice = 230.00,
                    WarrantyPeriod = "3 years",
                    CategoryId = 9,
                    Features = "14-Cup Capacity, Stainless Steel Blades, Easy-to-Clean Design",
                    PowerConsumption = "1000W"
                },
                new Product
                {
                    Id = 18,
                    Name = "Philips Daily Collection Food Processor",
                    Description = "Compact yet powerful, the Philips Daily Collection Food Processor offers efficient food preparation with multiple accessories for slicing, chopping, and blending.",
                    Brand = "Philips",
                    ModelNumber = "HR7627/00",
                    ListPrice = 100.00,
                    DiscountPrice = 90.00,
                    WarrantyPeriod = "2 years",
                    CategoryId = 9,
                    Features = "Compact Design, Multiple Accessories, Easy Storage",
                    PowerConsumption = "700W"
                },

                // Category 10: Blenders
                new Product
                {
                    Id = 19,
                    Name = "NutriBullet Pro Blender",
                    Description = "The NutriBullet Pro is a compact yet powerful blender designed to extract nutrients from fruits and vegetables. It's perfect for making smoothies, sauces, and more.",
                    Brand = "NutriBullet",
                    ModelNumber = "NB9-1301",
                    ListPrice = 150.00,
                    DiscountPrice = 130.00,
                    WarrantyPeriod = "1 year",
                    CategoryId = 10,
                    Features = "Powerful Motor, Easy to Clean, Compact Design",
                    PowerConsumption = "900W"
                },
                new Product
                {
                    Id = 20,
                    Name = "Vitamix Explorian Blender",
                    Description = "The Vitamix Explorian Blender offers exceptional versatility with 10-speed settings and a powerful motor. Its durable design ensures long-lasting performance for both hot and cold blends.",
                    Brand = "Vitamix",
                    ModelNumber = "E310",
                    ListPrice = 350.00,
                    DiscountPrice = 330.00,
                    WarrantyPeriod = "3 years",
                    CategoryId = 10,
                    Features = "10-Speed Settings, Self-Cleaning, Durable Construction",
                    PowerConsumption = "1400W"
                }

            );


            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "Home Essentials Ltd.",
                    Address = "123 Appliance Way",
                    City = "New York",
                    State = "NY",
                    PostalCode = "10001",
                    PhoneNumber = "212-555-7890"
                },
                new Company
                {
                    Id = 2,
                    Name = "KitchenPro Inc.",
                    Address = "456 Gourmet Ave",
                    City = "Los Angeles",
                    State = "CA",
                    PostalCode = "90001",
                    PhoneNumber = "310-555-1234"
                },
                new Company
                {
                    Id = 3,
                    Name = "Modern Living Solutions",
                    Address = "789 Lifestyle Blvd",
                    City = "Chicago",
                    State = "IL",
                    PostalCode = "60601",
                    PhoneNumber = "312-555-5678"
                },
                new Company
                {
                    Id = 4,
                    Name = "Tech & Comfort Co.",
                    Address = "321 Smart Home St",
                    City = "San Francisco",
                    State = "CA",
                    PostalCode = "94101",
                    PhoneNumber = "415-555-8765"
                }
            );
        }
    }
}
