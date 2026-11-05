using System;
using System.Windows.Forms;
using SalesSystem.GUI;

namespace SalesSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Khởi chạy màn hình Bán hàng chính (Form1)
            Application.Run(new Form1());
        }
    }
}