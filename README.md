# 🛒 MiniSupermarket System – Tổng kết Buổi Thực hành 2

## 📌 Giới thiệu

**MiniSupermarket System** là hệ thống quản lý siêu thị mini được xây dựng theo mô hình **Client – Server**, trong đó Backend cung cấp các RESTful API và Frontend WinForms đóng vai trò ứng dụng khách.

Sau Buổi Thực hành 2, hệ thống đã được nâng cấp từ một ứng dụng CRUD cơ bản thành hệ thống có **xác thực người dùng và phân quyền truy cập bằng JWT (JSON Web Token)**.

---

## 🎯 Mục tiêu Buổi Thực hành 2

Buổi thực hành tập trung vào các nội dung:

- Xây dựng cơ chế đăng nhập và xác thực người dùng.
- Áp dụng **JWT Authentication** cho Backend.
- Thực hiện phân quyền người dùng theo vai trò.
- Bảo vệ các RESTful API bằng `[Authorize]`.
- Xây dựng màn hình đăng nhập trên WinForms.
- Quản lý JWT Token ở phía Client.
- Sử dụng `HttpClient` để giao tiếp an toàn với Backend.
- Gửi Bearer Token trong HTTP Request Header.

---

# 📚 5.1. Tổng kết kiến thức và kỹ năng đạt được

## 🔐 1. Tư duy bảo mật hệ thống

Sinh viên đã nắm được nguyên lý hoạt động của cơ chế:

> **JWT – JSON Web Token – Stateless Authentication**

JWT cho phép Backend xác thực người dùng thông qua token được gửi từ Client mà không cần duy trì session đăng nhập phức tạp trên RAM của Server.

Quy trình xác thực cơ bản:

```text
┌──────────────┐
│   WinForms   │
│    Client    │
└──────┬───────┘
       │
       │ 1. Username + Password
       ▼
┌──────────────┐
│ AuthController│
│   Backend    │
└──────┬───────┘
       │
       │ 2. Xác thực tài khoản
       │
       │ 3. Generate JWT
       ▼
┌──────────────┐
│  JWT Token   │
└──────┬───────┘
       │
       │ 4. Bearer Token
       ▼
┌──────────────┐
│ Protected API│
│ [Authorize]  │
└──────────────┘

👨‍💻 5. Tác giả
Họ tên sinh viên: [Nguyễn Thị Mộng Tuyền]


Mã sinh viên: [2124110290]


Lớp học phần: [CCQ2411D]
