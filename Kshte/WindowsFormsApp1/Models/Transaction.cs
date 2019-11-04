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
        public decimal CurrentValue { get; set; }

        public override string ToString()
        {
            return $"Table: {TableID}  $: {CurrentValue}";
        }
    }
}
