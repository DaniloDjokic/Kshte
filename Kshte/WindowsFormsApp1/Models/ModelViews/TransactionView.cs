using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kshte.Models
{
    public class TransactionView
    {
        public int ID { get; set; }
        public string DateCreated { get; set; }
        public string DateCompleted { get; set; }
        public int TableID { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal PaidPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<TransactionDetailView> TransactionDetailViews { get; set; }

        public TransactionView(int ID, string DateCreated, string DateCompleted, int TableID, decimal CurrentPrice, decimal PaidPrice, decimal TotalPrice, IEnumerable<TransactionDetail> TransactionDetails)
        {
            this.ID = ID;
            this.DateCreated = DateCreated;
            this.DateCompleted = DateCompleted;
            this.TableID = TableID;
            this.CurrentPrice = CurrentPrice;
            this.PaidPrice = PaidPrice;
            this.TotalPrice = TotalPrice;

            if (TransactionDetails == null)
                throw new ArgumentNullException();

            this.TransactionDetailViews = TransactionDetails.Select(td => new TransactionDetailView(td));
        }

        [Newtonsoft.Json.JsonConstructor]
        public TransactionView(int ID, string DateCreated, string DateCompleted, int TableID, decimal CurrentPrice, decimal PaidPrice, decimal TotalPrice, IEnumerable<TransactionDetailView> TransactionDetailViews)
        {
            this.ID = ID;
            this.DateCreated = DateCreated;
            this.DateCompleted = DateCompleted;
            this.TableID = TableID;
            this.CurrentPrice = CurrentPrice;
            this.PaidPrice = PaidPrice;
            this.TotalPrice = TotalPrice;

            if (TransactionDetailViews == null)
                throw new ArgumentNullException();

            this.TransactionDetailViews = TransactionDetailViews;
        }
    }
}
