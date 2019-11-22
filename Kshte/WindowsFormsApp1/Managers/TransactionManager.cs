using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DBTools;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Managers
{
    public static class TransactionManager
    {
        private static List<Transaction> activeTransactions = null;
        public static IReadOnlyCollection<Transaction> ActiveTransactions
        {
            get
            {
                if (activeTransactions == null)
                {
                    activeTransactions = DBContext.GetActiveTransactions().ToList();
                    //TableManager.RefreshTransactionReferences();
                }

                return activeTransactions.AsReadOnly();
            }
        }

        public static IEnumerable<Transaction> GetTransactionHistory() => DBContext.GetAllTransactions();
        public static bool AddActiveTransaction(Table table, Transaction transaction)
        {
            if (!TableManager.Tables.Contains(table))
            {
                return false;
            }

            if (!ActiveTransactions.Contains(transaction))
            {
                transaction.TableID = table.ID;
                activeTransactions.Add(transaction);

                var id = DBContext.AddNewTransaction(transaction);
                transaction.ID = id;

                if (!TableManager.SetCurrentTransaction(table, transaction))
                {
                    activeTransactions.Remove(transaction);
                    DBContext.RemoveTransaction(transaction);
                    throw new InvalidOperationException("This table is occupied.");
                }

                return true;
            }
            else
                return false;
        }
        public static bool UpdateActiveTransaction(Transaction transaction)
        {
            if (ActiveTransactions.Contains(transaction))
            {
                DBContext.UpdateDB(transaction);
                return true;
            }
            else
                return false;
        }

        public static bool CompleteTransaction(Transaction transaction, DateTime dateCompleted)
        {
            if (!ActiveTransactions.Contains(transaction))
            {
                return false;
            }

            if (dateCompleted.CompareTo(DateTime.Parse(transaction.DateCreated)) < 0)
            {
                throw new ArgumentOutOfRangeException("The time of completition can't be earlier than the time of creation.");
            }

            if (transaction.GetUnpaidDetails().Count == 0)
            {
                transaction.DateCompleted = dateCompleted.ToString();
                DBContext.UpdateDB(transaction);
                activeTransactions.Remove(transaction);
                TableManager.GetById(transaction.TableID).SetCurrentTransaction(null);
                return true;
            }
            else
                return false;
        }

        internal static bool SwapTables(Transaction transaction1, Transaction transaction2)
        {
            if (!ActiveTransactions.Contains(transaction1))
            {
                return false;
            }

            if (!ActiveTransactions.Contains(transaction2))
            {
                return false;
            }

            int table1 = transaction1.TableID;
            int table2 = transaction2.TableID;

            transaction1.TableID = table2;
            DBContext.UpdateDB(transaction1);
            transaction2.TableID = table1;
            DBContext.UpdateDB(transaction2);

            return true;
        }
        internal static Transaction GetById(int transactionID)
        {
            Transaction result = null;

            foreach (var transaction in ActiveTransactions)
            {
                if (transaction.ID == transactionID)
                {
                    result = transaction;
                    break;
                }
            }

            return result;
        }
    }
}
