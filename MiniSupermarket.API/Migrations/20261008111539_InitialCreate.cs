using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RewardPoints = table.Column<int>(type: "int", nullable: false),
                    MembershipRank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Bánh kẹo & Đồ ăn vặt", "Snack, bánh quy, kẹo dẻo, socola" },
                    { 2, "Nước giải khát & Trà", "Nước ngọt, nước khoáng, trà xanh, nước tăng lực" },
                    { 3, "Sữa & Sản phẩm từ sữa", "Sữa tươi, sữa chua, sữa đặc, phô mai" },
                    { 4, "Mì gói & Thực phẩm ăn liền", "Mì ăn liền, phở khô, cháo gói, miến ăn liền" },
                    { 5, "Gia vị & Nước chấm", "Nước mắm, hạt nêm, đường, muối, bột ngọt, tiêu" },
                    { 6, "Dầu ăn & Bơ", "Dầu thực vật, dầu mè, bơ thực vật, dầu hào" },
                    { 7, "Gạo, Bột & Nông sản khô", "Gạo thơm, bột mì, bột chiên giòn, nếp, đậu xanh" },
                    { 8, "Đồ hộp & Thực phẩm chế biến", "Cá hộp, pate, thịt hộp, xúc xích tiệt trùng" },
                    { 9, "Hóa phẩm giặt xả & Tẩy rửa", "Nước giặt, nước xả vải, nước rửa chén, nước lau sàn" },
                    { 10, "Chăm sóc cá nhân & Vệ sinh", "Dầu gội, sữa tắm, kem đánh răng, xà bông cục" },
                    { 11, "Giấy ăn & Khăn ướt", "Giấy vệ sinh, khăn ăn, khăn giấy ướt, tã bỉm" },
                    { 12, "Đồ dùng gia đình & Nhà bếp", "Màng bọc thực phẩm, túi rác, miếng cọ rửa, tăm bông" },
                    { 13, "Cà phê, Trà & Đồ uống hòa tan", "Cà phê hòa tan, trà túi lọc, trà atiso, ngũ cốc dinh dưỡng" },
                    { 14, "Văn phòng phẩm & Tiện ích", "Bút viết, tập vở, kéo, băng keo, phong bì, bấm móng tay" },
                    { 15, "Thực phẩm đông lạnh & Mát", "Kem cây, xúc xích tươi, lạp xưởng, chả lụa" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 1, null, "Nguyễn Văn A", "Vàng", "0901122334", 150 },
                    { 2, null, "Trần Thị B", "Bạc", "0918877665", 50 },
                    { 3, null, "Lê Văn C", "Chuẩn", "0983344556", 10 },
                    { 4, null, "Phạm Thị Dung", "Kim Cương", "0978123456", 320 },
                    { 5, null, "Hoàng Văn Em", "Vàng", "0912345678", 180 },
                    { 6, null, "Vũ Thị Giang", "Bạc", "0966789012", 75 },
                    { 7, null, "Đỗ Minh Hùng", "Chuẩn", "0933221144", 20 },
                    { 8, null, "Đinh Thị Lan", "Vàng", "0944556677", 140 },
                    { 9, null, "Bùi Quang Mai", "Bạc", "0988990011", 60 },
                    { 10, null, "Trịnh Công Nam", "Chuẩn", "0909123890", 15 },
                    { 11, null, "Ngô Thị Oanh", "Kim Cương", "0919283746", 450 },
                    { 12, null, "Phan Thành Phúc", "Vàng", "0977345678", 190 },
                    { 13, null, "Lý Thu Quỳnh", "Bạc", "0938475610", 85 },
                    { 14, null, "Võ Văn Sơn", "Chuẩn", "0908765432", 30 },
                    { 15, null, "Dương Thị Tuyết", "Kim Cương", "0987654321", 500 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "893456789001", 1, 7000m, "Snack Oishi Cay 42g", 100 },
                    { 2, "893456789002", 2, 10000m, "Nước ngọt Coca-Cola 320ml", 120 },
                    { 3, "893456789003", 3, 9000m, "Sữa tươi tiệt trùng Vinamilk 180ml", 80 },
                    { 4, "893456789004", 4, 4500m, "Mì Hảo Hảo Tôm Chua Cay 75g", 250 },
                    { 5, "893456789005", 5, 25000m, "Nước mắm Nam Ngư Đệ Nhị 900ml", 50 },
                    { 6, "893456789006", 6, 48000m, "Dầu ăn Tường An Cooking Oil 1L", 60 },
                    { 7, "893456789007", 7, 160000m, "Gạo thơm ST25 túi 5kg", 30 },
                    { 8, "893456789008", 8, 18000m, "Cá hộp Ba Cô Gái sốt cà 155g", 90 },
                    { 9, "893456789009", 9, 28000m, "Nước rửa chén Sunlight Chanh 750g", 70 },
                    { 10, "893456789010", 10, 145000m, "Dầu gội Clear Bạc Hà mát lạnh 630g", 40 },
                    { 11, "893456789011", 11, 75000m, "Giấy vệ sinh Pulppy 10 cuộn 3 lớp", 45 },
                    { 12, "893456789012", 12, 32000m, "Màng bọc thực phẩm Ringo 30cm", 50 },
                    { 13, "893456789013", 13, 58000m, "Cà phê G7 3in1 hộp 18 gói", 85 },
                    { 14, "893456789014", 14, 8000m, "Tập vở học sinh 96 trang Thiên Long", 200 },
                    { 15, "893456789015", 15, 22000m, "Xúc xích tiệt trùng CP Red 175g", 110 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
