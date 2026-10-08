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

            FormCategoryManagement frmCategory = new FormCategoryManagement();
            FormRoleManagement frmRole = new FormRoleManagement();
            FormCustomerManagement frmCustomer = new FormCustomerManagement();

            frmCategory.Show();
            frmRole.Show();
            frmCustomer.Show();

            Application.Run();
        }
    }
}