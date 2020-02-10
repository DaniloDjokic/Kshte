using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kshte.DBTools;
using Kshte.Models;

namespace Kshte.Managers
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
            if (transaction == null || !ActiveTransactions.Contains(transaction))
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

                Table currentTable = TableManager.GetById(transaction.TableID);

                if (currentTable != null)
                {
                    TableManager.ClearCurrentTransaction(currentTable);
                }

                return true;
            }
            else
                return false;
        }

        public static bool RemoveTransaction(Transaction transaction)
        {
            if (transaction == null || transaction.ID < 0)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(transaction.DateCompleted))
            {
                return false;
            }

            activeTransactions.Remove(transaction);
            Table currentTable = TableManager.GetById(transaction.TableID);
            if (currentTable != null)
            {
                TableManager.ClearCurrentTransaction(currentTable);
            }

            if (DBContext.RemoveTransaction(transaction) == 0)
            {
                throw new Exception("Couldn't remove the transaction.");
            }

            return true;
        }

        public static bool RemoveExportedTransactions(IEnumerable<TransactionView> transactionViews)
        {
            foreach (var transactionView in transactionViews)
            {
                if (transactionView.ID < 0 || 
                    string.IsNullOrWhiteSpace(transactionView.DateCompleted) || 
                    !DateTime.TryParse(transactionView.DateCompleted, out DateTime _))
                {
                    return false;
                }
            }

            int removedTransactions = DBContext.RemoveExportedTransactions(transactionViews);

            if (removedTransactions != transactionViews.Count())
            {
                return false;
            }

            return true;
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
