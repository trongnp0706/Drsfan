using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Drsfan.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    ModelNumber = table.Column<string>(type: "text", nullable: false),
                    ListPrice = table.Column<double>(type: "double precision", nullable: false),
                    DiscountPrice = table.Column<double>(type: "double precision", nullable: false),
                    WarrantyPeriod = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Features = table.Column<string>(type: "text", nullable: false),
                    PowerConsumption = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    StreetAddress = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    CompanyId = table.Column<int>(type: "integer", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShippingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrderTotal = table.Column<double>(type: "double precision", nullable: false),
                    OrderStatus = table.Column<string>(type: "text", nullable: true),
                    PaymentStatus = table.Column<string>(type: "text", nullable: true),
                    TrackingNumber = table.Column<string>(type: "text", nullable: true),
                    Carrier = table.Column<string>(type: "text", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentDueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SessionId = table.Column<string>(type: "text", nullable: true),
                    PaymentIntentId = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    StreetAddress = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHeaders_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderHeaderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderHeaders_OrderHeaderId",
                        column: x => x.OrderHeaderId,
                        principalTable: "OrderHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Washing Machines" },
                    { 2, 2, "Refrigerators" },
                    { 3, 3, "Air Conditioners" },
                    { 4, 4, "Microwave Ovens" },
                    { 5, 5, "Vacuum Cleaners" },
                    { 6, 6, "Water Purifiers" },
                    { 7, 7, "Dishwashers" },
                    { 8, 8, "Electric Kettles" },
                    { 9, 9, "Coffee Makers" },
                    { 10, 10, "Rice Cookers" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "City", "Name", "PhoneNumber", "PostalCode", "State" },
                values: new object[,]
                {
                    { 1, "123 Appliance Way", "New York", "Home Essentials Ltd.", "212-555-7890", "10001", "NY" },
                    { 2, "456 Gourmet Ave", "Los Angeles", "KitchenPro Inc.", "310-555-1234", "90001", "CA" },
                    { 3, "789 Lifestyle Blvd", "Chicago", "Modern Living Solutions", "312-555-5678", "60601", "IL" },
                    { 4, "321 Smart Home St", "San Francisco", "Tech & Comfort Co.", "415-555-8765", "94101", "CA" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Description", "DiscountPrice", "Features", "ListPrice", "ModelNumber", "Name", "PowerConsumption", "WarrantyPeriod" },
                values: new object[,]
                {
                    { 1, "Samsung", 1, "The Samsung EcoBubble Washer offers powerful cleaning with its unique EcoBubble technology, which transforms detergent into bubbles for faster penetration into fabrics. It ensures effective cleaning even at low temperatures, saving energy while protecting delicate clothing.", 399.99000000000001, "Hygiene Steam, Digital Inverter, EcoBubble Technology", 450.0, "WW70J3280KW", "Samsung EcoBubble Front Load Washer", "200W", "2 years" },
                    { 2, "LG", 1, "Designed for ultimate convenience, the LG Smart Inverter Washer delivers low noise and high energy efficiency. Its TurboDrum technology ensures powerful washing, while the Smart Diagnosis feature allows you to troubleshoot issues directly from your smartphone.", 449.99000000000001, "TurboDrum, Smart Diagnosis, Smart Inverter Control", 500.0, "T80SJMB1Z", "LG Smart Inverter Top Load Washer", "250W", "3 years" },
                    { 3, "Samsung", 2, "This spacious Samsung refrigerator features a frost-free design and a digital inverter compressor for long-lasting cooling. The Twin Cooling technology maintains optimal humidity levels, keeping fruits and vegetables fresh for longer periods.", 549.99000000000001, "Twin Cooling, MoistFresh Zone, Power Freeze", 600.0, "RT30T3722S8", "Samsung Double Door Refrigerator", "350W", "3 years" },
                    { 4, "Whirlpool", 2, "The Whirlpool Multi-Door Refrigerator offers advanced cooling technology, ensuring every corner of the fridge maintains a consistent temperature. Its Zeolite Technology absorbs excess moisture, preventing fruits and vegetables from getting soggy.", 649.99000000000001, "Zeolite Technology, FreshFlow Air Tower, Adaptive Intelligence", 699.99000000000001, "IF INV CNV 515", "Whirlpool Multi-Door Refrigerator", "400W", "3 years" },
                    { 5, "Daikin", 3, "The Daikin Split Air Conditioner combines sleek design with high performance. Its PM2.5 air filter ensures cleaner air indoors, while its energy-efficient inverter technology reduces electricity bills. Perfect for all weather conditions.", 649.99000000000001, "PM2.5 Filter, Quiet Operation, High-Efficiency Cooling", 700.0, "FTKF50TV", "Daikin Split AC with Inverter Technology", "1500W", "5 years" },
                    { 6, "LG", 3, "With Dual Inverter Compressor technology, the LG DualCool Split AC ensures faster and quieter cooling. It features Monsoon Comfort for humid weather and an Anti-Bacterial Filter for improved air quality.", 699.99000000000001, "Anti-Bacterial Filter, Dual Inverter, Monsoon Comfort", 750.0, "KS-Q18SNZD", "LG DualCool Split AC", "1450W", "10 years" },
                    { 7, "Panasonic", 4, "The Panasonic Convection Microwave Oven offers versatile cooking options, from grilling to baking. Its sleek design fits perfectly into modern kitchens, while the Auto Cook menu simplifies the preparation of your favorite dishes.", 179.99000000000001, "Convection Cooking, Auto Cook Menu, Touch Keypad", 200.0, "NN-CT645BFDG", "Panasonic Convection Microwave Oven", "1200W", "2 years" },
                    { 8, "Samsung", 4, "Compact and easy to use, the Samsung Solo Microwave Oven is perfect for reheating, defrosting, and simple cooking. Its ceramic enamel cavity ensures easy cleaning and durability.", 129.99000000000001, "Ceramic Enamel Cavity, Quick Defrost, Reheat Function", 150.0, "MS23J5133AG", "Samsung Solo Microwave Oven", "1150W", "1 year" },
                    { 9, "Dyson", 5, "Experience powerful suction and unmatched convenience with the Dyson V11 Cordless Vacuum Cleaner. It offers multiple cleaning modes and a long battery life, making it ideal for homes with pets and children.", 850.0, "HEPA Filtration, Cordless Design, Multiple Cleaning Modes", 900.0, "V11 Absolute Pro", "Dyson V11 Cordless Vacuum Cleaner", "545W", "2 years" },
                    { 10, "Philips", 5, "Keep your home spotless with the Philips Bagless Vacuum Cleaner. Its PowerCyclone 5 technology delivers high suction efficiency, and the ergonomic design ensures easy handling.", 180.0, "Bagless, PowerCyclone 5, Advanced Dust Bin Design", 200.0, "FC9352/01", "Philips Bagless Vacuum Cleaner", "1900W", "2 years" },
                    { 11, "Bosch", 6, "The Bosch Freestanding Dishwasher is a powerful kitchen appliance, designed to save time and water while providing superior cleaning. With multiple wash programs and a sleek design, it's perfect for modern homes.", 650.0, "EcoSilence Drive, AquaStop, Load Sensor", 700.0, "SMS46KI03I", "Bosch Freestanding Dishwasher", "1200W", "2 years" },
                    { 12, "IFB", 6, "The IFB Neptune VX Dishwasher offers an energy-efficient and noise-free cleaning experience. Its Jet Wash program ensures quick cleaning in just 18 minutes, making it ideal for busy households.", 580.0, "Jet Wash, Adjustable Racks, EcoWash Mode", 600.0, "Neptune VX", "IFB Neptune VX Dishwasher", "1100W", "2 years" },
                    { 13, "De'Longhi", 7, "Enjoy barista-quality coffee at home with the De'Longhi Espresso Coffee Machine. It features a 15-bar pressure system and a manual frother for creamy cappuccinos and lattes.", 350.0, "15-bar Pump, Manual Frother, Compact Design", 400.0, "EC685.R", "De'Longhi Espresso Coffee Machine", "1450W", "1 year" },
                    { 14, "Nespresso", 7, "The Nespresso Vertuo Plus offers convenience and versatility, allowing you to brew a variety of coffee sizes. Its Centrifusion technology ensures optimal flavor extraction with every cup.", 220.0, "Centrifusion Technology, One-Touch Brewing, Large Water Tank", 250.0, "GCA1-US-BM-NE", "Nespresso Vertuo Plus Coffee Maker", "1350W", "1 year" },
                    { 15, "Philips", 8, "Boil water in minutes with the Philips Electric Kettle. It features a stainless steel design, 1.5-liter capacity, and an automatic shut-off function for safety.", 35.0, "Stainless Steel, 1.5L Capacity, Automatic Shut-Off", 40.0, "HD9306/06", "Philips Electric Kettle", "2000W", "1 year" },
                    { 16, "Morphy Richards", 8, "The Morphy Richards InstaCook Electric Kettle is perfect for boiling water, making instant noodles, or preparing beverages. Its cool-touch handle ensures safe operation.", 45.0, "Multi-Purpose, Cool-Touch Handle, 1L Capacity", 50.0, "Instacook", "Morphy Richards InstaCook Kettle", "1800W", "1 year" },
                    { 17, "Cuisinart", 9, "The Cuisinart Food Processor simplifies meal preparation with its large 14-cup capacity and multiple blade attachments. It's perfect for chopping, slicing, and kneading dough.", 230.0, "14-Cup Capacity, Stainless Steel Blades, Easy-to-Clean Design", 250.0, "DFP-14BCNY", "Cuisinart 14-Cup Food Processor", "1000W", "3 years" },
                    { 18, "Philips", 9, "Compact yet powerful, the Philips Daily Collection Food Processor offers efficient food preparation with multiple accessories for slicing, chopping, and blending.", 90.0, "Compact Design, Multiple Accessories, Easy Storage", 100.0, "HR7627/00", "Philips Daily Collection Food Processor", "700W", "2 years" },
                    { 19, "NutriBullet", 10, "The NutriBullet Pro is a compact yet powerful blender designed to extract nutrients from fruits and vegetables. It's perfect for making smoothies, sauces, and more.", 130.0, "Powerful Motor, Easy to Clean, Compact Design", 150.0, "NB9-1301", "NutriBullet Pro Blender", "900W", "1 year" },
                    { 20, "Vitamix", 10, "The Vitamix Explorian Blender offers exceptional versatility with 10-speed settings and a powerful motor. Its durable design ensures long-lasting performance for both hot and cold blends.", 330.0, "10-Speed Settings, Self-Cleaning, Durable Construction", 350.0, "E310", "Vitamix Explorian Blender", "1400W", "3 years" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderHeaderId",
                table: "OrderDetails",
                column: "OrderHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_ApplicationUserId",
                table: "OrderHeaders",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ApplicationUserId",
                table: "ShoppingCarts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ProductId",
                table: "ShoppingCarts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "OrderHeaders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
