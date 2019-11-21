using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DBTools;
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
            DBSeeder.Seed(DBConnector.Connection);

            //Category category = new Category();
            //category.Name = "NovaKat";
            //CategoryManager.AddCategory(category);

            var categories = DBTools.DBContext.GetExistingCategories();
            //ArticleManager.GetByCategory(CategoryManager.GetByName("Alkohol"));
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
        }
    }
}
