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
        private static List<Table> tables = null;
        public static IReadOnlyCollection<Table> Tables
        {
            get
            {
                if (tables == null)
                {
                    tables = DBContext.GetExistingTables();
                }
                return tables.AsReadOnly();
            }
        }

        public static bool AddTable(Table table)
        {
            if (!Tables.Contains(table))
            {
                var id = DBContext.AddNewTable(table);

                table.ID = id;
                tables.Add(table);

                return true;
            }
            else
                return false;
        }

        internal static Table GetById(int tableID)
        {
            Table result = null;

            foreach (var table in Tables)
            {
                if (table.ID == tableID)
                {
                    result = table;
                    break;
                }
            }

            return result;
        }

        internal static bool SetCurrentTransaction(Table table, Transaction transaction)
        {
            if (!Tables.Contains(table))
                return false;

            if (!TransactionManager.ActiveTransactions.Contains(transaction))
                return false;

            if (table.CurrentTransaction != null)
                throw new InvalidOperationException("This table already has an ongoing transaction.");

            table.SetCurrentTransaction(transaction);
            DBContext.UpdateDB(table);

            return true;
        }

        internal static bool ClearCurrentTransaction(Table table)
        {
            if (!Tables.Contains(table))
                return false;

            if (table.CurrentTransaction == null)
                return true;

            table.SetCurrentTransaction(null);
            DBContext.UpdateDB(table);

            return true;
        }

        public static bool SwapTableTransactions(Table table1, Table table2)
        {
            if (!Tables.Contains(table1))
                return false;

            if (!Tables.Contains(table2))
                return false;

            Transaction transaction1 = table1.CurrentTransaction;
            Transaction transaction2 = table2.CurrentTransaction;

            table1.SetCurrentTransaction(transaction2);
            DBContext.UpdateDB(table1);
            table2.SetCurrentTransaction(transaction1);
            DBContext.UpdateDB(table2);

            if (transaction1 == null && transaction2 != null)
            {
                transaction2.TableID = table1.ID;
                TransactionManager.UpdateActiveTransaction(transaction2);
                return true;
            }

            if (transaction2 == null && transaction1 != null)
            {
                transaction1.TableID = table2.ID;
                TransactionManager.UpdateActiveTransaction(transaction1);
                return true;
            }

            if (transaction2 == null && transaction1 == null)
            {
                return false;
            }

            TransactionManager.SwapTables(transaction1, transaction2);
            return true;
        }

        //Legacy
        //internal static void RefreshTransactionReferences()
        //{
        //    try
        //    {
        //        var currentTables = Tables;
        //        foreach (var transaction in TransactionManager.ActiveTransactions)
        //        {
        //            currentTables.Where(table => table.ID == transaction.TableID).First().CurrentTransaction = transaction;
        //        }
        //    }
        //    catch (ArgumentNullException e)
        //    {
        //        throw new Exception("Error refreshing table->transaction references.", e);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Unknown error refreshing table->transaction references.", e);
        //    }
        //}
    }
}
