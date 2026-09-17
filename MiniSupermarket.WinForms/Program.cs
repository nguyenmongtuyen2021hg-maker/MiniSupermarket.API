namespace MiniSupermarket.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new FormCategoryManagement());
            FormCategoryManagement frmCategory = new FormCategoryManagement();
            FormRoleManagement frmRole= new FormRoleManagement();
            frmCategory.Show();
            frmRole.Show();
            Application.Run();
        }
    }
}