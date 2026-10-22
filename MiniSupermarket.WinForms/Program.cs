namespace MiniSupermarket.WinForms
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Khởi chạy từ màn hình đăng nhập phân quyền FormLogin
            Application.Run(new FormLogin());
        }
    }
}