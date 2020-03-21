using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kshte.DBTools;
using Kshte.Helpers;
using Kshte.Managers;
using Kshte.Models;

namespace Kshte
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //This is not proper logging. Change later.
            try
            {
                DbHandler firstRunTest = new DbHandler();
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
            catch (Exception e)
            {
                ErrorLogger errorLogger = new ErrorLogger();
                errorLogger.LogException(e);
            }
        }
    }
}
