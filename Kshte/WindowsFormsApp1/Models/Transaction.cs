using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Managers;

namespace WindowsFormsApp1.Models
{
    public class Transaction
    {
<<<<<<< HEAD
        #region DB Properties
        public int ID { get; internal set; }
        public string DateCreated { get; internal set; }
        public string DateCompleted { get; internal set; } = null;
        public int TableID { get; internal set; }
        #endregion
=======
        public int TableID { get; set; }
        public decimal CurrentValue
        {
            get
            {
                decimal sum = 0;
                foreach(Article article in Articles)
                {
                    sum += article.Price;
                }
                return sum;
            }
        }
>>>>>>> master

        private List<TransactionDetail> transactionDetails = null;
        public IReadOnlyCollection<TransactionDetail> TransactionDetails { get => transactionDetails.AsReadOnly(); }
        private decimal? currentPrice = null;
        public decimal CurrentPrice 
        { 
            get 
            {
                if (currentPrice.HasValue)
                {
                    currentPrice = CalculateCurrentPrice();
                }

                return currentPrice.Value;
            } 
        }

        internal Transaction()
        {
            transactionDetails = new List<TransactionDetail>();
        }
        public Transaction(DateTime dateCreated)
        {
            DateCreated = dateCreated.ToString();
        }
        internal void ForceSetTransactionDetails(IEnumerable<TransactionDetail> details)
        {
            if (transactionDetails != null)
                throw new InvalidOperationException("This transaction's transaction details are already set.");

            transactionDetails = details.ToList();
        }
        public bool AddTransactionDetail(TransactionDetail detail)
        {
            if (detail.Transaction == null)
            {
                detail.SetTransaction(this);
            }
            else
            {
                throw new InvalidOperationException("This transaction detail already belongs to a different transaction.");
            }

            if (!TransactionDetails.Contains(detail))
            {
                transactionDetails.Add(detail);
                return true;
            }
            return false;
        }
        public void RemoveTransactionDetail(TransactionDetail detail)
        {
            if (detail.Transaction.ID == ID && TransactionDetails.Contains(detail))
            {
                transactionDetails.Remove(detail);
            }
        }
        public void PayForTransactionDetail(TransactionDetail detail)
        {
            if (TransactionDetails.Contains(detail))
            {
                detail.PaidFor = true;
                currentPrice = null;
            }
        }
        private decimal CalculateCurrentPrice()
        {
            decimal price = 0;
            foreach (var detail in transactionDetails)
            {
                if (!detail.PaidFor)
                {
                    price += detail.EffectivePrice;
                }
            }
            return price;
        }

        public override string ToString()
        {
            return $"Table: {TableID}  $: {CurrentPrice}";
        }

        public IReadOnlyCollection<TransactionDetail> GetUnpaidDetails()
        {
            List<TransactionDetail> results = new List<TransactionDetail>();

            foreach (var detail in TransactionDetails)
            {
                if (!detail.PaidFor)
                {
                    results.Add(detail);
                }
            }

            return results.AsReadOnly();
        }
    }
}
