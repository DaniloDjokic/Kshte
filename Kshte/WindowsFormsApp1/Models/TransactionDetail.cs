﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Managers;

namespace WindowsFormsApp1.Models
{
    public class TransactionDetail
    {
        #region DB Properties
        public int ID { get; internal set; }
        public bool PaidFor { get; internal set; }
        public decimal EffectivePrice { get; set; }
        public int TransactionID { get; internal set; }
        public int ArticleID { get; internal set; }
        #endregion

        private Transaction transaction = null;
        public Transaction Transaction
        {
            get
            {
                if (transaction == null)
                {
                    transaction = TransactionManager.GetById(TransactionID);
                }
                return transaction;
            }
        }
        private Article article = null;
        public Article Article
        {
            get
            {
                if (article == null)
                {
                    article = ArticleManager.GetById(ArticleID);
                }

                return article;
            }
        }

        public TransactionDetail(Article article, bool paidFor, decimal effectivePrice)
        {
            if (effectivePrice < 0)
                throw new ArgumentOutOfRangeException();

            if (article == null)
                throw new ArgumentNullException();

            if (ArticleManager.AllArticles.Contains(article))
            {
                this.article = article;
                ArticleID = article.ID;
            }
            else
                throw new InvalidOperationException("This article is not contained in the ArticleManager.");

            EffectivePrice = effectivePrice;
            PaidFor = paidFor;

            transaction = null;
            TransactionID = -1;
        }

        internal void SetTransaction(Transaction transaction)
        {
            if (TransactionManager.ActiveTransactions.Contains(transaction))
            {
                this.transaction = transaction;
                TransactionID = transaction.ID;
            }
            else
                throw new InvalidOperationException("This transaction is not contained in the TransactionManager.");
        }
    }
}
