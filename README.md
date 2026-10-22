# 🛒 NGHIÊN CỨU VÀ HIỆN THỰC HÓA PHẦN MỀM QUẢN LÝ BÁN LẺ THEO CHUẨN KIẾN TRÚC DOANH NGHIỆP -- TIỆM TẠP HÓA NGỌC MAI
> **Môn học:** Lập trình Ứng dụng .NET Core (Mã môn: 229162)  
> **Buổi thực hành:** Buổi 4 - Kiến trúc Giao diện WinForms Shell (Sidebar & Main Workspace) và Điều hướng Phân quyền RBAC (Admin / Cashier / Warehouse)

---

## 🏗️ 1. Mô hình Kiến trúc Hệ thống (Client - Server)
Dự án được xây dựng theo mô hình phân tầng hiện đại, tách biệt hoàn toàn giữa Backend và Frontend:
* **`MiniSupermarket.API` (Backend):** Dự án ASP.NET Core Web API chịu trách nhiệm xử lý logic nghiệp vụ, quản lý dữ liệu và cung cấp các RESTful API chuẩn hóa.
* **`MiniSupermarket.WinForms` (Frontend Client):** Ứng dụng Windows Forms đóng vai trò là máy trạm POS tại quầy, sử dụng `HttpClient` để gọi dữ liệu từ API qua mạng và hiển thị trực quan lên `DataGridView`.

---

## 🛠️ 2. Công nghệ Sử dụng
* **Ngôn ngữ:** C# (.NET 8.0)
* **Backend:** ASP.NET Core Web API, Controllers, In-Memory Data, LINQ
* **Frontend:** Windows Forms (.NET 8.0), `System.Net.Http.Json`
* **Công cụ kiểm thử:** Swagger UI

---

## 📂 3. Cấu trúc Solution
```text
MiniSupermarketSystem/
│
├── MiniSupermarket.API/          # Dự án Web API (Backend)
│   ├── Controllers/              # Chứa CategoriesController (CRUD & Search)
│   ├── Models/                   # Chứa lớp thực thể Category.cs,Roles.cs
│   └── Program.cs                # Cấu hình dịch vụ và Middleware
│
└── MiniSupermarket.WinForms/     # Dự án Windows Forms (Frontend Client)
    └── FormCategoryManagement.cs # Giao diện quản lý danh mục CRUD
    └── FormRoleManagement.cs     # Xây dựng giao diện WinForms Client 

🚀 4. Hướng dẫn Chạy và Kiểm thử Dự án
Bước 1: Chạy phía Backend (Web API)
Mở Solution bằng Visual Studio 2022.


Nhấp chuột phải vào project MiniSupermarket.API chọn Set as Startup Project.


Nhấn F5 để chạy. Trình duyệt sẽ tự động mở giao diện Swagger UI để kiểm tra các phương thức GET, POST, PUT, DELETE.


Bước 2: Chạy phía Frontend (WinForms Client)
Đảm bảo cổng (Port) trong ApiClientService hoặc HttpClient của WinForms khớp với cổng https://localhost:XXXXX của Web API đang chạy.

Nhấp chuột phải vào project MiniSupermarket.WinForms chọn Debug -> Start new instance.

Thử nghiệm các chức năng: Tải danh sách, Thêm mới, Sửa, Xóa và Tìm kiếm nhóm hàng.

👨‍💻 5. Tác giả
Họ tên sinh viên: [Nguyễn Thị Mộng Tuyền]


Mã sinh viên: [2124110290]


Lớp học phần: [CCQ2411D]

---

## Buổi 5 — ADO.NET, Stored Procedure và giao dịch POS

Chuỗi kết nối SQL Server và khóa session được cấu hình riêng qua User Secrets, không lưu giá trị thật trong Git. Xem hướng dẫn đầy đủ tại [README-BUOI5.md](README-BUOI5.md).

### Chuẩn bị database

Trong SSMS, mở và chạy `MiniSupermarket.API/SqlScripts/Buoi5.sql` trên database đã cấu hình bằng User Secrets. Script chỉ bổ sung cột `Orders.RewardPointsAwarded` nếu chưa có và tạo lại `dbo.sp_GetRevenueByCashier`; dữ liệu hóa đơn hiện có được giữ lại. Cột điểm của các hóa đơn cũ mặc định là 0 vì database trước đây chưa lưu điểm thưởng theo từng hóa đơn.

### Các phần thực hành

| Nội dung | Endpoint / thành phần | Kỹ thuật ADO.NET |
| --- | --- | --- |
| Tra cứu mã vạch | `GET /api/ado-products/barcode/{barcode}` | `SqlConnection`, tham số hóa, `SqlDataReader` |
| Tải danh sách ngoại tuyến | `GET /api/ado-products/disconnected-cache` | `SqlDataAdapter`, `DataTable` |
| Lọc tồn kho | `GET /api/ado-products/filter-stock?minStock=50` | `SqlDataReader`, `SqlParameter` |
| Đếm sản phẩm | `GET /api/ado-products/count` | `ExecuteScalar()` |
| Tổng hợp theo danh mục | `GET /api/ado-products/category-summary` | `DataTable` |
| Doanh thu theo cashier | `GET /api/reports/revenue-by-cashier` | `CommandType.StoredProcedure`, `SqlDataReader` |
| Checkout POS | `POST /api/checkout` | `SqlTransaction`, `Commit`, `Rollback` |
| Hủy hóa đơn | `DELETE /api/checkout/cancel/{orderId}` | Hoàn kho/điểm và xóa hóa đơn trong transaction; chỉ Admin |

Các API yêu cầu đăng nhập bằng bearer token. Báo cáo doanh thu và hủy hóa đơn chỉ dành cho Admin. Cashier chỉ được checkout với tài khoản đang đăng nhập; giá sản phẩm luôn được đọc từ database, không tin giá gửi từ WinForms.

### Kiểm tra trên Swagger

1. Chạy `MiniSupermarket.API`, đăng nhập qua `POST /api/users/login`, rồi dùng token bearer để gọi các endpoint.
2. Tra mã hiện có `893456789001` (200) và mã không tồn tại (404); tải cache và báo cáo theo cashier.
3. POS WinForms hiện gọi endpoint ADO.NET để tra barcode và `POST /api/checkout` khi thanh toán.
4. Thử giỏ có sản phẩm hợp lệ trước rồi thêm sản phẩm vượt tồn kho. Checkout trả 400 và rollback hóa đơn, chi tiết, tồn kho, điểm thưởng trong cùng giao dịch.

Không dùng thanh toán hợp lệ trên database thật chỉ để thử nghiệm vì nó sẽ ghi hóa đơn và thay đổi tồn kho. Hãy dùng database kiểm thử riêng nếu cần đối soát một giao dịch Commit đầu-cuối.

### Kết quả kiểm thử biệt lập

Đã chạy bản build trên database tạm rồi xóa database tạm sau khi kiểm tra:

- Checkout 2 món với số lượng lần lượt 2 và 3 trả 200, tổng tiền 89.000đ và cộng 8 điểm; giá gửi giả từ client không được dùng.
- Trước khi hủy: tạo 1 hóa đơn/2 dòng chi tiết, tồn kho giảm 2 và 3, điểm khách tăng 8.
- Hủy hóa đơn trả 200 và hoàn nguyên số hóa đơn, chi tiết, tồn kho, điểm về đúng trạng thái ban đầu.
- Giỏ hàng có món thứ hai vượt kho trả 400; dù hóa đơn và món thứ nhất đã được ghi trong transaction, rollback đưa mọi dữ liệu về trạng thái ban đầu.

---

## Buổi 6 — EF Core, phân trang LINQ và tích hợp WinForms

Buổi 6 bổ sung phân trang/tìm kiếm sản phẩm ở API, eager loading danh mục, bộ lọc nhóm hàng, thanh chuyển trang trên WinForms và API phân trang khách hàng theo điểm thưởng. Chi tiết yêu cầu, endpoint, luồng dữ liệu và cách chạy nằm trong [README-BUOI6.md](README-BUOI6.md).
