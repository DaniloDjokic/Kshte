using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kshte.Managers;

namespace Kshte.Models
{
    public class Table
    {
        #region DB Properties
        public int ID { get; internal set; }
        public int? CurrentTransactionID { get; set; }
        #endregion

        public Table() { }

        private Transaction currentTransaction = null;
        public Transaction CurrentTransaction 
        {
            get
            {
                if (CurrentTransactionID.HasValue && currentTransaction == null)
                {
                    currentTransaction = TransactionManager.GetById(CurrentTransactionID.Value);
                }

                return currentTransaction;
            }
        }

        internal Transaction SetCurrentTransaction(Transaction transaction)
        {
            Transaction oldTransaction = currentTransaction;

            currentTransaction = transaction;

            if (transaction == null)
                CurrentTransactionID = null;
            else
                CurrentTransactionID = transaction.ID;

            return oldTransaction;
        }
    }
}
