using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Controllers;
using WindowsFormsApp1.Managers;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public class TransactionController : CategoryController
    {
        public Transaction Transaction { get; private set; }
        
        public TransactionController(Transaction transaction, int tableID)
        {
            this.Transaction = transaction;
            ArticleSelectMode = false;

            if (transaction == null)
               CreateNewTransaction(tableID);
        }

        private void CreateNewTransaction(int tableID)
        {
            Transaction = new Transaction(DateTime.Now);
        }

        /// <summary>
        /// Adds unpaid article to currently active transaction
        /// </summary>
        /// <param name="article">The article to add</param>
        public TransactionDetail AddArticle(Article article)
        {
            try
            {
                var detail = new TransactionDetail(article, false, article.Price);
                Transaction.AddTransactionDetail(detail);

                TransactionManager.UpdateActiveTransaction(Transaction);

                return detail;
            }
            catch (Exception e)
            {
                MessageBox.Show("Artikal ne moze biti dodat: " + e.GetFullMessage(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        
        /// <summary>
        /// Makes db call to pay for an article and removes it from currently active transaction
        /// </summary>
        /// <param name="article">The article to pay</param>
        public void PayArticle(TransactionDetail detail, bool pushUpdate = true)
        {
            //Call to db
            try
            {
                Transaction.PayForTransactionDetail(detail);

                if (pushUpdate)
                {
                    TransactionManager.UpdateActiveTransaction(Transaction);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Artikal ne moze biti obrisan: " + e.GetFullMessage(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }

        /// <summary>
        /// Removes an article from the currently active transaction
        /// </summary>
        /// <param name="article">The article to remove</param>
        public void RemoveArticle(TransactionDetail detail, bool pushUpdate = true)
        {
            try
            {
                Transaction.RemoveTransactionDetail(detail);

                if (pushUpdate)
                {
                    TransactionManager.UpdateActiveTransaction(Transaction);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Artikal ne moze biti obrisan: " + e.GetFullMessage(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Makes a call to PayArticle for every currently active article in the transaction
        /// </summary>
        public void PayAll()
        {
            var details = Transaction.GetUnpaidDetails().ToList();

            for (int i = 0; i < details.Count; i++)
            {
                PayArticle(details[i], false);
            }

            TransactionManager.UpdateActiveTransaction(Transaction);
        }

        /// <summary>
        /// Removes this transaction and all of its details from the database and references to it
        /// </summary>
        public void RemoveTransaction()
        {
            TransactionManager.RemoveTransaction(Transaction);
        }

        public void CompleteTransaction()
        {
            TransactionManager.CompleteTransaction(Transaction, DateTime.Now);
        }
    }
}
