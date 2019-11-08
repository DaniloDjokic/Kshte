using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.DBTools;

namespace WindowsFormsApp1.Managers
{
    public static class TableManager
    {
        private static IReadOnlyCollection<Table> tables = null;
        public static IReadOnlyCollection<Table> Tables
        {
            get
            {
                if (tables == null)
                {
                    tables = DBContext.GetExistingTables().AsReadOnly();
                }
                return tables;
            }
            private set
            {
                tables = value;
            }
        }

        public static void AddTable(Table table)
        {
            var id = DBContext.AddNewTable(table);

            table.ID = id;

            var temp = tables.ToList();
            temp.Add(table);
            tables = temp.AsReadOnly();
        }
    }
}
