using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class TransactionDetailView
    {
        public int ID { get; set; }
        public bool PaidFor { get; set; }
        public decimal EffectivePrice { get; set; }
       
        public ArticleView Article { get; set; }

        [Newtonsoft.Json.JsonConstructor]
        public TransactionDetailView(int ID, bool PaidFor, decimal EffectivePrice, ArticleView Article) 
        {
            this.ID = ID;
            this.PaidFor = PaidFor;
            this.EffectivePrice = EffectivePrice;
            this.Article = Article;
        }

        public TransactionDetailView(TransactionDetail transactionDetail) 
        {
            this.ID = transactionDetail.ID;
            this.PaidFor = transactionDetail.PaidFor;
            this.EffectivePrice = transactionDetail.EffectivePrice;
            this.Article = new ArticleView(transactionDetail.Article);
        }
    }
}
