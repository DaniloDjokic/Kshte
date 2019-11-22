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
        #region DB Properties
        public int ID { get; internal set; }
        public string DateCreated { get; internal set; }
        public string DateCompleted { get; internal set; } = null;
        public int TableID { get; internal set; }
        #endregion


        private List<TransactionDetail> transactionDetails = null;
        public IReadOnlyCollection<TransactionDetail> TransactionDetails { get => transactionDetails.AsReadOnly(); }
        public decimal CurrentPrice 
        { 
            get 
            {
                return CalculateCurrentPrice();
            } 
        }

        public decimal TotalPrice
        {
            get
            {
                return CalculateTotalPrice();
            }
        }

        public decimal PaidPrice
        {
            get
            {
                return CalculatePaidPrice();
            }
        }

        internal Transaction()
        {
            transactionDetails = new List<TransactionDetail>();
        }
        public Transaction(DateTime dateCreated)
        {
            DateCreated = dateCreated.ToString();
            transactionDetails = new List<TransactionDetail>();
        }
        internal void ForceSetTransactionDetails(IEnumerable<TransactionDetail> details)
        {
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

        private decimal CalculateTotalPrice()
        {
            decimal price = 0;
            foreach (var detail in transactionDetails)
            {
                price += detail.EffectivePrice;
            }
            return price;
        }

        private decimal CalculatePaidPrice()
        {
            decimal price = 0;
            foreach (var detail in transactionDetails)
            {
                if (detail.PaidFor)
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
