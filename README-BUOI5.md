# BUỔI 5 — ADO.NET, STORED PROCEDURE VÀ GIAO DỊCH POS

**Dự án:** Tiệm Tạp hóa Ngọc Mai
**Môn học:** Lập trình Ứng dụng .NET Core (229162)
**Nền tảng:** ASP.NET Core Web API, WinForms, ADO.NET, SQL Server

## Thông tin sinh viên

- **Họ và tên:** Nguyễn Thị Mộng Tuyền
- **Mã số sinh viên:** 2124110290
- **Lớp học phần:** CCQ2411D

## 1. Mục tiêu

Buổi thực hành dùng ADO.NET để đọc và ghi dữ liệu SQL Server, gọi Stored Procedure và đảm bảo thanh toán POS có tính nguyên tử. Các lệnh SQL nhận dữ liệu từ người dùng đều dùng tham số `SqlParameter`; kết nối và command được giải phóng bằng `using`/`await using`.

| Thành phần | Vai trò trong bài |
| --- | --- |
| `SqlConnection` | Mở kết nối đến SQL Server và giải phóng kết nối sau khi dùng |
| `SqlCommand` | Thực thi câu SQL hoặc Stored Procedure |
| `SqlParameter` | Truyền dữ liệu riêng khỏi câu SQL, tránh SQL Injection |
| `SqlDataReader` | Đọc kết quả từng dòng theo chế độ connected/forward-only |
| `SqlDataAdapter` + `DataTable` | Nạp kết quả vào bộ nhớ để xử lý sau khi ngắt kết nối |
| `ExecuteScalar()` | Lấy một giá trị, ví dụ tổng số sản phẩm |
| `ExecuteNonQuery()` | Ghi/sửa/xóa dữ liệu, ví dụ cập nhật kho trong checkout |
| `ExecuteReader()` | Đọc một hoặc nhiều dòng kết quả |

## 2. Cấu hình local và Database

Thông tin kết nối SQL Server và khóa ký session không được lưu trong Git. Trước khi chạy API, tạo User Secrets cho project:

```powershell
dotnet user-secrets init --project MiniSupermarket.API
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=<SQL_SERVER>;Database=<DATABASE_NAME>;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;" --project MiniSupermarket.API
dotnet user-secrets set "JwtSettings:Secret" "<YOUR_RANDOM_SECRET_AT_LEAST_32_BYTES>" --project MiniSupermarket.API
```

Thay `<SQL_SERVER>`, `<DATABASE_NAME>` và khóa mẫu bằng cấu hình riêng trên máy của bạn. User Secrets nằm ngoài repo; không commit chuỗi kết nối hoặc khóa thật. Với SSMS:

1. Kết nối SQL Server chứa database đã cấu hình ở trên.
2. Mở `MiniSupermarket.API/SqlScripts/Buoi5.sql`.
3. Chạy toàn bộ script. Script dùng `CREATE OR ALTER PROCEDURE`, chỉ thêm `Orders.RewardPointsAwarded` nếu cột chưa có và không xóa hóa đơn hiện tại.

Cột `RewardPointsAwarded` lưu số điểm đã cấp cho mỗi đơn để báo cáo và hủy đơn có thể đối soát. Những hóa đơn cũ được gán 0 vì trước đây database không lưu điểm theo từng hóa đơn.

Stored Procedure `dbo.sp_GetRevenueByCashier` tổng hợp số đơn, doanh thu và điểm thưởng theo từng tài khoản có vai trò Cashier đang hoạt động. Database hiện có 6 cashier nên báo cáo trả 6 dòng.

## 3. API thực hành

Các API cần bearer token nhận từ `POST /api/users/login`.

| Method | Route | Quyền | Nội dung |
| --- | --- | --- | --- |
| GET | `/api/ado-products/barcode/{barcode}` | Người dùng đã đăng nhập | Tra một sản phẩm bằng `SqlDataReader` |
| GET | `/api/ado-products/disconnected-cache` | Người dùng đã đăng nhập | Nạp sản phẩm bằng `SqlDataAdapter`/`DataTable` |
| GET | `/api/ado-products/filter-stock?minStock=50` | Người dùng đã đăng nhập | Lọc sản phẩm sắp hết hàng bằng reader và tham số SQL |
| GET | `/api/ado-products/count` | Người dùng đã đăng nhập | Đếm sản phẩm bằng `ExecuteScalar()` |
| GET | `/api/ado-products/category-summary` | Người dùng đã đăng nhập | Tổng hợp sản phẩm theo danh mục vào `DataTable` |
| GET | `/api/reports/revenue-by-cashier` | Admin | Gọi Stored Procedure và đọc kết quả bằng reader |
| POST | `/api/checkout` | Admin, Cashier | Thanh toán trong một `SqlTransaction` |
| DELETE | `/api/checkout/cancel/{orderId}` | Admin | Hủy hóa đơn, hoàn kho và điều chỉnh điểm trong transaction |

POS WinForms dùng endpoint barcode ADO.NET khi quét hàng và dùng `POST /api/checkout` khi thanh toán.

### Đăng nhập mẫu

```http
POST https://localhost:7118/api/users/login
Content-Type: application/json

{
  "username": "cashier01",
  "password": "123456"
}
```

Gửi token API trả về trong header:

```http
Authorization: Bearer <token>
```

Tài khoản mẫu có thể thay đổi theo dữ liệu trong bảng `Users`. Không dùng tài khoản cashier để gọi báo cáo hoặc hủy hóa đơn.

## 4. Checkout và quy tắc dữ liệu

Ví dụ request:

```http
POST /api/checkout
Content-Type: application/json
Authorization: Bearer <cashier-token>

{
  "cashierUsername": "cashier01",
  "customerPhone": "0901122334",
  "items": [
    { "productId": 1, "quantity": 2, "unitPrice": 1 },
    { "productId": 5, "quantity": 3, "unitPrice": 1 }
  ]
}
```

`cashierUsername` phải khớp tài khoản trong token. Giá gửi từ client chỉ để tương thích payload WinForms; API lấy giá hiện hành từ bảng `Products`. Khi checkout:

1. Mở transaction và khóa các sản phẩm cần bán để tránh bán vượt tồn kho do hai lượt checkout đồng thời.
2. Tạo hóa đơn trong `Orders` và lấy `OrderId` bằng `OUTPUT INSERTED.OrderId`/`ExecuteScalar()`.
3. Ghi từng dòng `OrderDetails`, trừ tồn kho bằng `ExecuteNonQuery()`.
4. Nếu có khách thành viên, cộng điểm theo quy tắc mỗi 10.000đ được 1 điểm.
5. Gọi `Commit()` khi hoàn tất. Nếu một bước lỗi, gọi `Rollback()` để hoàn tác toàn bộ hóa đơn, chi tiết, tồn kho và điểm.

Thành công trả `orderId`, `orderCode`, `totalItems`, `finalAmount` và `rewardPointsAdded`. Giỏ hàng rỗng, khách hàng không tồn tại hoặc sản phẩm vượt tồn kho trả lỗi và transaction được rollback.

## 5. Hủy hóa đơn

Admin gọi:

```http
DELETE /api/checkout/cancel/1
Authorization: Bearer <admin-token>
```

API đọc hóa đơn và chi tiết, cộng lại kho, trừ lại điểm thưởng đã lưu trong `RewardPointsAwarded`, xóa chi tiết rồi xóa hóa đơn. Toàn bộ quy trình nằm trong một transaction; nếu có lỗi, dữ liệu được rollback.

## 6. Kiểm thử

### Tra barcode

- `GET /api/ado-products/barcode/893456789001` — mã có thật trả 200 và thông tin sản phẩm.
- `GET /api/ado-products/barcode/999999999999` — mã không có trả 404.
- `GET /api/ado-products/disconnected-cache` — trả danh sách đã nạp vào `DataTable`.
- `GET /api/ado-products/count` — trả số lượng sản phẩm.

### Báo cáo thu ngân

Admin gọi `GET /api/reports/revenue-by-cashier`. API gọi `dbo.sp_GetRevenueByCashier` với `CommandType.StoredProcedure`. Cashier gọi endpoint này phải nhận 403.

### Checkout Commit và hủy

Trên database kiểm thử riêng, checkout hai sản phẩm với số lượng 2 và 3 trả 200, tổng 89.000đ và cấp 8 điểm. Sau đó hủy hóa đơn trả 200; số dòng hóa đơn/chi tiết, tồn kho và điểm được hoàn nguyên về trạng thái ban đầu.

### Checkout Rollback

Gửi giỏ có một sản phẩm còn hàng trước, tiếp theo là sản phẩm có số lượng yêu cầu vượt xa tồn kho. API trả 400. Đối soát sau request: không còn hóa đơn/chi tiết mới; tồn kho và điểm không đổi. Bài kiểm thử này đã chạy trên database tạm biệt lập, không để lại thay đổi trong database bán hàng.

## 7. Kết quả triển khai

- Build `MiniSupermarket.API`: thành công, 0 lỗi và 0 cảnh báo.
- Build `MiniSupermarket.WinForms`: thành công, 0 lỗi và 0 cảnh báo.
- Đã kiểm tra API barcode, DataTable cache, lọc tồn kho, đếm sản phẩm, báo cáo Stored Procedure, quyền truy cập và các nhánh Commit/Rollback.
- Mã nguồn chính: `MiniSupermarket.API/Controllers/AdoProductController.cs`, `ReportsController.cs`, `CheckoutController.cs`.

Sau khi cập nhật code, dừng phiên API/WinForms cũ trong Visual Studio bằng nút **Stop**, rồi chạy lại project để nạp bản mới.
