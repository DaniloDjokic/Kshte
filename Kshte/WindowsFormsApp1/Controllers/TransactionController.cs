using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Controllers;
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
            Transaction = new Transaction { TableID = tableID, Articles = new List<Article>() };
            MockData.activeTransactions.Add(Transaction);
        }

        /// <summary>
        /// Adds unpaid article to currently active transaction
        /// </summary>
        /// <param name="article">The article to add</param>
        public void AddArticle(Article article)
        {
            this.Transaction.Articles.Add(article);
        }
        
        /// <summary>
        /// Makes db call to pay for an article and removes it from currently active transaction
        /// </summary>
        /// <param name="article">The article to pay</param>
        public void PayArticle(Article article)
        {
            //Call to db
            this.RemoveArticle(article);
        }

        /// <summary>
        /// Removes an article from the currently active transaction
        /// </summary>
        /// <param name="article">The article to remove</param>
        public void RemoveArticle(Article article)
        {
            this.Transaction.Articles.Remove(article);
        }

        /// <summary>
        /// Makes a call to PayArticle for every currently active article in the transaction
        /// </summary>
        public void PayAll()
        {
            for(int i = 0; i < Transaction.Articles.Count; i++)
            {
                PayArticle(Transaction.Articles[i]);
            }
        }

        /// <summary>
        /// Clears all active articles and removes this transaction from active transactions
        /// </summary>
        public void RemoveTransaction()
        {
            for(int i = 0; i < Transaction.Articles.Count; i++)
            {
                RemoveArticle(Transaction.Articles[i]);
            }

            MockData.activeTransactions.Remove(Transaction);
        }
    }
}
