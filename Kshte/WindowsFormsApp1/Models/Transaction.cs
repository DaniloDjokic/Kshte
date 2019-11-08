using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class Transaction
    {
        #region DB Properties
        public int ID { get; set; }
        public string DateCreated { get; set; }
        public string DateCompleted { get; set; } = null;
        public int TableID { get; set; }
        #endregion

        public decimal CurrentValue { get; }

        public List<Article> Articles { get; set; }

        public override string ToString()
        {
            return $"Table: {TableID}  $: {CurrentValue}";
        }
    }
}
