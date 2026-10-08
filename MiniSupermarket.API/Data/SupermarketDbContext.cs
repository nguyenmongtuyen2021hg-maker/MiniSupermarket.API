using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    // DbContext đại diện cho phiên làm việc với cơ sở dữ liệu SQL Server
    // Đồ án: Nghiên cứu và Hiện thực hóa Phần mềm Quản lý Bán lẻ theo chuẩn Kiến trúc Doanh nghiệp -- Tiệm Tạp hóa Ngọc Mai
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options) : base(options) { }

        // Khai báo các bảng dữ liệu ánh xạ từ Model
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        // Cấu hình dữ liệu mồi ban đầu (Data Seeding)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Nạp sẵn 15 nhóm hàng (Categories) cho Tiệm Tạp hóa Ngọc Mai
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Bánh kẹo & Đồ ăn vặt", Description = "Snack, bánh quy, kẹo dẻo, socola" },
                new Category { CategoryId = 2, CategoryName = "Nước giải khát & Trà", Description = "Nước ngọt, nước khoáng, trà xanh, nước tăng lực" },
                new Category { CategoryId = 3, CategoryName = "Sữa & Sản phẩm từ sữa", Description = "Sữa tươi, sữa chua, sữa đặc, phô mai" },
                new Category { CategoryId = 4, CategoryName = "Mì gói & Thực phẩm ăn liền", Description = "Mì ăn liền, phở khô, cháo gói, miến ăn liền" },
                new Category { CategoryId = 5, CategoryName = "Gia vị & Nước chấm", Description = "Nước mắm, hạt nêm, đường, muối, bột ngọt, tiêu" },
                new Category { CategoryId = 6, CategoryName = "Dầu ăn & Bơ", Description = "Dầu thực vật, dầu mè, bơ thực vật, dầu hào" },
                new Category { CategoryId = 7, CategoryName = "Gạo, Bột & Nông sản khô", Description = "Gạo thơm, bột mì, bột chiên giòn, nếp, đậu xanh" },
                new Category { CategoryId = 8, CategoryName = "Đồ hộp & Thực phẩm chế biến", Description = "Cá hộp, pate, thịt hộp, xúc xích tiệt trùng" },
                new Category { CategoryId = 9, CategoryName = "Hóa phẩm giặt xả & Tẩy rửa", Description = "Nước giặt, nước xả vải, nước rửa chén, nước lau sàn" },
                new Category { CategoryId = 10, CategoryName = "Chăm sóc cá nhân & Vệ sinh", Description = "Dầu gội, sữa tắm, kem đánh răng, xà bông cục" },
                new Category { CategoryId = 11, CategoryName = "Giấy ăn & Khăn ướt", Description = "Giấy vệ sinh, khăn ăn, khăn giấy ướt, tã bỉm" },
                new Category { CategoryId = 12, CategoryName = "Đồ dùng gia đình & Nhà bếp", Description = "Màng bọc thực phẩm, túi rác, miếng cọ rửa, tăm bông" },
                new Category { CategoryId = 13, CategoryName = "Cà phê, Trà & Đồ uống hòa tan", Description = "Cà phê hòa tan, trà túi lọc, trà atiso, ngũ cốc dinh dưỡng" },
                new Category { CategoryId = 14, CategoryName = "Văn phòng phẩm & Tiện ích", Description = "Bút viết, tập vở, kéo, băng keo, phong bì, bấm móng tay" },
                new Category { CategoryId = 15, CategoryName = "Thực phẩm đông lạnh & Mát", Description = "Kem cây, xúc xích tươi, lạp xưởng, chả lụa" }
            );

            // Nạp sẵn 15 khách hàng mẫu ban đầu cho Tiệm Tạp hóa Ngọc Mai
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerName = "Nguyễn Văn A", PhoneNumber = "0901122334", MembershipRank = "Vàng", RewardPoints = 150 },
                new Customer { CustomerId = 2, CustomerName = "Trần Thị B", PhoneNumber = "0918877665", MembershipRank = "Bạc", RewardPoints = 50 },
                new Customer { CustomerId = 3, CustomerName = "Lê Văn C", PhoneNumber = "0983344556", MembershipRank = "Chuẩn", RewardPoints = 10 },
                new Customer { CustomerId = 4, CustomerName = "Phạm Thị Dung", PhoneNumber = "0978123456", MembershipRank = "Kim Cương", RewardPoints = 320 },
                new Customer { CustomerId = 5, CustomerName = "Hoàng Văn Em", PhoneNumber = "0912345678", MembershipRank = "Vàng", RewardPoints = 180 },
                new Customer { CustomerId = 6, CustomerName = "Vũ Thị Giang", PhoneNumber = "0966789012", MembershipRank = "Bạc", RewardPoints = 75 },
                new Customer { CustomerId = 7, CustomerName = "Đỗ Minh Hùng", PhoneNumber = "0933221144", MembershipRank = "Chuẩn", RewardPoints = 20 },
                new Customer { CustomerId = 8, CustomerName = "Đinh Thị Lan", PhoneNumber = "0944556677", MembershipRank = "Vàng", RewardPoints = 140 },
                new Customer { CustomerId = 9, CustomerName = "Bùi Quang Mai", PhoneNumber = "0988990011", MembershipRank = "Bạc", RewardPoints = 60 },
                new Customer { CustomerId = 10, CustomerName = "Trịnh Công Nam", PhoneNumber = "0909123890", MembershipRank = "Chuẩn", RewardPoints = 15 },
                new Customer { CustomerId = 11, CustomerName = "Ngô Thị Oanh", PhoneNumber = "0919283746", MembershipRank = "Kim Cương", RewardPoints = 450 },
                new Customer { CustomerId = 12, CustomerName = "Phan Thành Phúc", PhoneNumber = "0977345678", MembershipRank = "Vàng", RewardPoints = 190 },
                new Customer { CustomerId = 13, CustomerName = "Lý Thu Quỳnh", PhoneNumber = "0938475610", MembershipRank = "Bạc", RewardPoints = 85 },
                new Customer { CustomerId = 14, CustomerName = "Võ Văn Sơn", PhoneNumber = "0908765432", MembershipRank = "Chuẩn", RewardPoints = 30 },
                new Customer { CustomerId = 15, CustomerName = "Dương Thị Tuyết", PhoneNumber = "0987654321", MembershipRank = "Kim Cương", RewardPoints = 500 }
            );
        }
    }
}
