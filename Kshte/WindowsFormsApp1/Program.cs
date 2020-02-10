using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DBTools;
using WindowsFormsApp1.Helpers;
using WindowsFormsApp1.Managers;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            DbCreator firstRunTest = new DbCreator();
            firstRunTest.HandleRun();

            if (!firstRunTest.DbReady)
            {
                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}
