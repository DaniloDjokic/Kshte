using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class TransactionView
    {
        private readonly int ID;

        public string DateCreated { get; private set; }
        public string DateCompleted { get; private set; }
        public int TableID { get; private set; }
        public decimal CurrentPrice { get; private set; }
        public decimal PaidPrice { get; private set; }
        public decimal TotalPrice { get; private set; }

        private IEnumerable<TransactionDetail> transactionDetails;
        public IEnumerable<TransactionDetail> GetTransactionDetails() {return transactionDetails;}

        public TransactionView(int ID, string DateCreated, string DateCompleted, int TableID, decimal CurrentPrice, decimal PaidPrice, decimal TotalPrice, IEnumerable<TransactionDetail> details)
        {
            this.ID = ID;
            this.DateCreated = DateCreated;
            this.DateCompleted = DateCompleted;
            this.TableID = TableID;
            this.CurrentPrice = CurrentPrice;
            this.PaidPrice = PaidPrice;
            this.TotalPrice = TotalPrice;

            if (details == null)
                throw new ArgumentNullException();

            this.transactionDetails = details;
        }

        public int GetID()
        {
            return ID;
        }
    }
}
