using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Drsfan.DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class seedMoreDataForCompanyCategoryProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Air Conditioners");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Microwave Ovens");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 5, 5, "Vacuum Cleaners" },
                    { 6, 6, "Water Purifiers" },
                    { 7, 7, "Dishwashers" },
                    { 8, 8, "Electric Kettles" },
                    { 9, 9, "Coffee Makers" },
                    { 10, 10, "Rice Cookers" }
                });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "City", "Name", "PhoneNumber", "PostalCode", "State" },
                values: new object[] { "123 Appliance Way", "New York", "Home Essentials Ltd.", "212-555-7890", "10001", "NY" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "City", "Name", "PhoneNumber", "PostalCode", "State" },
                values: new object[] { "456 Gourmet Ave", "Los Angeles", "KitchenPro Inc.", "310-555-1234", "90001", "CA" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Address", "City", "Name", "PhoneNumber", "PostalCode", "State" },
                values: new object[] { "789 Lifestyle Blvd", "Chicago", "Modern Living Solutions", "312-555-5678", "60601", "IL" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "City", "Name", "PhoneNumber", "PostalCode", "State" },
                values: new object[] { 4, "321 Smart Home St", "San Francisco", "Tech & Comfort Co.", "415-555-8765", "94101", "CA" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Brand", "Description", "DiscountPrice", "Features", "ImageUrl", "ListPrice", "ModelNumber", "Name", "PowerConsumption", "WarrantyPeriod" },
                values: new object[] { "Samsung", "The Samsung EcoBubble Washer offers powerful cleaning with its unique EcoBubble technology, which transforms detergent into bubbles for faster penetration into fabrics. It ensures effective cleaning even at low temperatures, saving energy while protecting delicate clothing.", 399.99000000000001, "Hygiene Steam, Digital Inverter, EcoBubble Technology", "/images/products/washer1.jpg", 450.0, "WW70J3280KW", "Samsung EcoBubble Front Load Washer", "200W", "2 years" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Brand", "CategoryId", "Description", "DiscountPrice", "Features", "ImageUrl", "ListPrice", "ModelNumber", "Name", "PowerConsumption", "WarrantyPeriod" },
                values: new object[] { "LG", 1, "Designed for ultimate convenience, the LG Smart Inverter Washer delivers low noise and high energy efficiency. Its TurboDrum technology ensures powerful washing, while the Smart Diagnosis feature allows you to troubleshoot issues directly from your smartphone.", 449.99000000000001, "TurboDrum, Smart Diagnosis, Smart Inverter Control", "/images/products/washer2.jpg", 500.0, "T80SJMB1Z", "LG Smart Inverter Top Load Washer", "250W", "3 years" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Brand", "CategoryId", "Description", "DiscountPrice", "Features", "ImageUrl", "ListPrice", "ModelNumber", "Name", "PowerConsumption", "WarrantyPeriod" },
                values: new object[] { "Samsung", 2, "This spacious Samsung refrigerator features a frost-free design and a digital inverter compressor for long-lasting cooling. The Twin Cooling technology maintains optimal humidity levels, keeping fruits and vegetables fresh for longer periods.", 549.99000000000001, "Twin Cooling, MoistFresh Zone, Power Freeze", "/images/products/fridge1.jpg", 600.0, "RT30T3722S8", "Samsung Double Door Refrigerator", "350W", "3 years" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Brand", "CategoryId", "Description", "DiscountPrice", "Features", "ImageUrl", "ListPrice", "ModelNumber", "Name", "PowerConsumption", "WarrantyPeriod" },
                values: new object[] { "Whirlpool", 2, "The Whirlpool Multi-Door Refrigerator offers advanced cooling technology, ensuring every corner of the fridge maintains a consistent temperature. Its Zeolite Technology absorbs excess moisture, preventing fruits and vegetables from getting soggy.", 649.99000000000001, "Zeolite Technology, FreshFlow Air Tower, Adaptive Intelligence", "/images/products/fridge2.jpg", 699.99000000000001, "IF INV CNV 515", "Whirlpool Multi-Door Refrigerator", "400W", "3 years" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Description", "DiscountPrice", "Features", "ImageUrl", "ListPrice", "ModelNumber", "Name", "PowerConsumption", "WarrantyPeriod" },
                values: new object[,]
                {
                    { 5, "Daikin", 3, "The Daikin Split Air Conditioner combines sleek design with high performance. Its PM2.5 air filter ensures cleaner air indoors, while its energy-efficient inverter technology reduces electricity bills. Perfect for all weather conditions.", 649.99000000000001, "PM2.5 Filter, Quiet Operation, High-Efficiency Cooling", "/images/products/ac1.jpg", 700.0, "FTKF50TV", "Daikin Split AC with Inverter Technology", "1500W", "5 years" },
                    { 6, "LG", 3, "With Dual Inverter Compressor technology, the LG DualCool Split AC ensures faster and quieter cooling. It features Monsoon Comfort for humid weather and an Anti-Bacterial Filter for improved air quality.", 699.99000000000001, "Anti-Bacterial Filter, Dual Inverter, Monsoon Comfort", "/images/products/ac2.jpg", 750.0, "KS-Q18SNZD", "LG DualCool Split AC", "1450W", "10 years" },
                    { 7, "Panasonic", 4, "The Panasonic Convection Microwave Oven offers versatile cooking options, from grilling to baking. Its sleek design fits perfectly into modern kitchens, while the Auto Cook menu simplifies the preparation of your favorite dishes.", 179.99000000000001, "Convection Cooking, Auto Cook Menu, Touch Keypad", "/images/products/microwave1.jpg", 200.0, "NN-CT645BFDG", "Panasonic Convection Microwave Oven", "1200W", "2 years" },
                    { 8, "Samsung", 4, "Compact and easy to use, the Samsung Solo Microwave Oven is perfect for reheating, defrosting, and simple cooking. Its ceramic enamel cavity ensures easy cleaning and durability.", 129.99000000000001, "Ceramic Enamel Cavity, Quick Defrost, Reheat Function", "/images/products/microwave2.jpg", 150.0, "MS23J5133AG", "Samsung Solo Microwave Oven", "1150W", "1 year" },
                    { 9, "Dyson", 5, "Experience powerful suction and unmatched convenience with the Dyson V11 Cordless Vacuum Cleaner. It offers multiple cleaning modes and a long battery life, making it ideal for homes with pets and children.", 850.0, "HEPA Filtration, Cordless Design, Multiple Cleaning Modes", "/images/products/vacuum1.jpg", 900.0, "V11 Absolute Pro", "Dyson V11 Cordless Vacuum Cleaner", "545W", "2 years" },
                    { 10, "Philips", 5, "Keep your home spotless with the Philips Bagless Vacuum Cleaner. Its PowerCyclone 5 technology delivers high suction efficiency, and the ergonomic design ensures easy handling.", 180.0, "Bagless, PowerCyclone 5, Advanced Dust Bin Design", "/images/products/vacuum2.jpg", 200.0, "FC9352/01", "Philips Bagless Vacuum Cleaner", "1900W", "2 years" },
                    { 11, "Bosch", 6, "The Bosch Freestanding Dishwasher is a powerful kitchen appliance, designed to save time and water while providing superior cleaning. With multiple wash programs and a sleek design, it's perfect for modern homes.", 650.0, "EcoSilence Drive, AquaStop, Load Sensor", "/images/products/dishwasher1.jpg", 700.0, "SMS46KI03I", "Bosch Freestanding Dishwasher", "1200W", "2 years" },
                    { 12, "IFB", 6, "The IFB Neptune VX Dishwasher offers an energy-efficient and noise-free cleaning experience. Its Jet Wash program ensures quick cleaning in just 18 minutes, making it ideal for busy households.", 580.0, "Jet Wash, Adjustable Racks, EcoWash Mode", "/images/products/dishwasher2.jpg", 600.0, "Neptune VX", "IFB Neptune VX Dishwasher", "1100W", "2 years" },
                    { 13, "De'Longhi", 7, "Enjoy barista-quality coffee at home with the De'Longhi Espresso Coffee Machine. It features a 15-bar pressure system and a manual frother for creamy cappuccinos and lattes.", 350.0, "15-bar Pump, Manual Frother, Compact Design", "/images/products/coffeemaker1.jpg", 400.0, "EC685.R", "De'Longhi Espresso Coffee Machine", "1450W", "1 year" },
                    { 14, "Nespresso", 7, "The Nespresso Vertuo Plus offers convenience and versatility, allowing you to brew a variety of coffee sizes. Its Centrifusion technology ensures optimal flavor extraction with every cup.", 220.0, "Centrifusion Technology, One-Touch Brewing, Large Water Tank", "/images/products/coffeemaker2.jpg", 250.0, "GCA1-US-BM-NE", "Nespresso Vertuo Plus Coffee Maker", "1350W", "1 year" },
                    { 15, "Philips", 8, "Boil water in minutes with the Philips Electric Kettle. It features a stainless steel design, 1.5-liter capacity, and an automatic shut-off function for safety.", 35.0, "Stainless Steel, 1.5L Capacity, Automatic Shut-Off", "/images/products/kettle1.jpg", 40.0, "HD9306/06", "Philips Electric Kettle", "2000W", "1 year" },
                    { 16, "Morphy Richards", 8, "The Morphy Richards InstaCook Electric Kettle is perfect for boiling water, making instant noodles, or preparing beverages. Its cool-touch handle ensures safe operation.", 45.0, "Multi-Purpose, Cool-Touch Handle, 1L Capacity", "/images/products/kettle2.jpg", 50.0, "Instacook", "Morphy Richards InstaCook Kettle", "1800W", "1 year" },
                    { 17, "Cuisinart", 9, "The Cuisinart Food Processor simplifies meal preparation with its large 14-cup capacity and multiple blade attachments. It's perfect for chopping, slicing, and kneading dough.", 230.0, "14-Cup Capacity, Stainless Steel Blades, Easy-to-Clean Design", "/images/products/foodprocessor1.jpg", 250.0, "DFP-14BCNY", "Cuisinart 14-Cup Food Processor", "1000W", "3 years" },
                    { 18, "Philips", 9, "Compact yet powerful, the Philips Daily Collection Food Processor offers efficient food preparation with multiple accessories for slicing, chopping, and blending.", 90.0, "Compact Design, Multiple Accessories, Easy Storage", "/images/products/foodprocessor2.jpg", 100.0, "HR7627/00", "Philips Daily Collection Food Processor", "700W", "2 years" },
                    { 19, "NutriBullet", 10, "The NutriBullet Pro is a compact yet powerful blender designed to extract nutrients from fruits and vegetables. It's perfect for making smoothies, sauces, and more.", 130.0, "Powerful Motor, Easy to Clean, Compact Design", "/images/products/blender1.jpg", 150.0, "NB9-1301", "NutriBullet Pro Blender", "900W", "1 year" },
                    { 20, "Vitamix", 10, "The Vitamix Explorian Blender offers exceptional versatility with 10-speed settings and a powerful motor. Its durable design ensures long-lasting performance for both hot and cold blends.", 330.0, "10-Speed Settings, Self-Cleaning, Durable Construction", "/images/products/blender2.jpg", 350.0, "E310", "Vitamix Explorian Blender", "1400W", "3 years" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Microwaves");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Fans");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "City", "Name", "PhoneNumber", "PostalCode", "State" },
                values: new object[] { "123 Tech Street", "San Francisco", "Tech Solutions Ltd.", "123-456-7890", "94103", "CA" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "City", "Name", "PhoneNumber", "PostalCode", "State" },
                values: new object[] { "456 Market Ave", "New York", "Global Enterprises", "987-654-3210", "10001", "NY" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Address", "City", "Name", "PhoneNumber", "PostalCode", "State" },
                values: new object[] { "789 Future Blvd", "Austin", "NextGen Innovations", "555-123-4567", "73301", "TX" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Brand", "Description", "DiscountPrice", "Features", "ImageUrl", "ListPrice", "ModelNumber", "Name", "PowerConsumption", "WarrantyPeriod" },
                values: new object[] { "LG", "Front load washing machine, inverter technology, energy-efficient.", 299.99000000000001, "Automatic wash mode, quick wash, energy-saving", "/images/products/lggiat.jpg", 349.99000000000001, "LG1234", "LG 8kg Washing Machine", "500W", "24 months" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Brand", "CategoryId", "Description", "DiscountPrice", "Features", "ImageUrl", "ListPrice", "ModelNumber", "Name", "PowerConsumption", "WarrantyPeriod" },
                values: new object[] { "Samsung", 2, "Double-door refrigerator, 250L capacity, energy-saving technology.", 459.99000000000001, "Quick cooling, energy-efficient", "/images/products/samsungtu.jpg", 499.99000000000001, "Samsung250L", "Samsung 250L Refrigerator", "150W", "12 months" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Brand", "CategoryId", "Description", "DiscountPrice", "Features", "ImageUrl", "ListPrice", "ModelNumber", "Name", "PowerConsumption", "WarrantyPeriod" },
                values: new object[] { "Sharp", 3, "Sharp 23L microwave with grilling function, time-saving cooking.", 99.989999999999995, "Grilling and microwave functions, easy to use", "/images/products/sharplosong.jpg", 119.98999999999999, "Sharp23L", "Sharp 23L Microwave", "800W", "12 months" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Brand", "CategoryId", "Description", "DiscountPrice", "Features", "ImageUrl", "ListPrice", "ModelNumber", "Name", "PowerConsumption", "WarrantyPeriod" },
                values: new object[] { "Panasonic", 4, "Panasonic stand fan with 3 wind modes, durable motor.", 29.989999999999998, "Strong wind modes, energy-saving", "/images/products/panasonicquat.jpg", 39.990000000000002, "Panasonic16", "Panasonic 16-inch Fan", "50W", "12 months" });
        }
    }
}
