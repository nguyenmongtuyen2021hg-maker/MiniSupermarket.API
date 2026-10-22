namespace MiniSupermarket.WinForms
{
    // Quản lý phiên đăng nhập và phân quyền của người dùng hiện tại
    public static class SessionManager
    {
        public static string CurrentUsername { get; set; } = "admin01";
        public static string CurrentFullName { get; set; } = "Nguyễn Quản Trị";
        public static string CurrentRole { get; set; } = "Admin";
        public static string JwtToken { get; set; } = string.Empty;
    }
}
