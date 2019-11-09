using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class Transaction
    {
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

        public List<Article> Articles { get; set; }

        public override string ToString()
        {
            return $"Table: {TableID}  $: {CurrentValue}";
        }
    }
}
