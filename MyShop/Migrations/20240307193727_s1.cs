using Microsoft.EntityFrameworkCore.Migrations;

namespace MyShop.Migrations
{
    public partial class s1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    QuantityInStock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryToProducts",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryToProducts", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_CategoryToProducts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryToProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Creator", "soheil0910" },
                    { 2, "G750", "mobile" },
                    { 3, "Honor 8", "mobile" },
                    { 4, "فقط کدنویسی", "لب تاب" },
                    { 5, "تعداد زیاد ", "شرکت ها" },
                    { 6, "بیشتر سنعت های بروز جهان", "سنعت هایی که کار میکند" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Price", "QuantityInStock" },
                values: new object[,]
                {
                    { 1, 214m, 8 },
                    { 2, 250m, 7 },
                    { 3, 2412m, 6 },
                    { 4, 2500.02m, 5 },
                    { 5, 3500m, 4 },
                    { 6, 9800m, 3 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ItemId", "Name" },
                values: new object[,]
                {
                    { 1, "نصب و راه اندازی BootStrap4 رو پروژه فروشگاه", 1, "28" },
                    { 2, "ساخت جداول فروشگاه با Code First - بخش دوم", 2, "32" },
                    { 3, "معرفی Razor Pages و ساخت ادمین فروشگاه با Razor Pages", 3, "43" },
                    { 4, "قوی ترین پردازنده جهان بسیار خنک با باتری اتمی و حافظه رم در حد کامپیوتر", 4, "گوشی" },
                    { 5, "گوشی با چند سیستم امل و قابلیت نصب هر گونه رام و دوربین فوقولاده ", 5, "گوشی" },
                    { 6, "نگران تمام شدن سوخت نباشید دارای برق شهری و ابشن های جلو تر از تکنولوژی", 6, "ماشین" }
                });

            migrationBuilder.InsertData(
                table: "CategoryToProducts",
                columns: new[] { "ProductId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 6, 3 },
                    { 6, 2 },
                    { 6, 1 },
                    { 5, 5 },
                    { 5, 4 },
                    { 5, 3 },
                    { 5, 2 },
                    { 5, 1 },
                    { 4, 5 },
                    { 4, 4 },
                    { 4, 3 },
                    { 4, 2 },
                    { 4, 1 },
                    { 3, 5 },
                    { 3, 4 },
                    { 3, 3 },
                    { 3, 2 },
                    { 3, 1 },
                    { 2, 5 },
                    { 2, 4 },
                    { 2, 3 },
                    { 2, 2 },
                    { 2, 1 },
                    { 1, 5 },
                    { 1, 4 },
                    { 1, 3 },
                    { 1, 2 },
                    { 6, 4 },
                    { 6, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryToProducts_CategoryId",
                table: "CategoryToProducts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ItemId",
                table: "Products",
                column: "ItemId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryToProducts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
