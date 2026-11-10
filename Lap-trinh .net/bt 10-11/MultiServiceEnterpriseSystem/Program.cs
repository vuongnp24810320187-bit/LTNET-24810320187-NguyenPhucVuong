using System;
using System.Windows.Forms;
using MultiServiceEnterpriseSystem.GUI;

namespace MultiServiceEnterpriseSystem
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new frmMain());
        }
    }
}

