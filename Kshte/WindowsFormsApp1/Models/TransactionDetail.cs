using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class TransactionDetail
    {
        #region DB Properties
        public int ID { get; set; }
        public bool PaidFor { get; set; }
        public decimal EffectivePrice { get; set; }
        public Transaction Transaction { get; set; }
        public Article Article { get; set; }
        #endregion
    }
}
