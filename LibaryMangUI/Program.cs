
using System;
using System.Windows.Forms;

using LibaryMangUI.Forms;
namespace LibaryMangUI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ana menyu il? ba?la
            Application.Run(new Forms.MainMenu());
        }
    }
}