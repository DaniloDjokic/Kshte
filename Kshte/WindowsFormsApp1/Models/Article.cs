using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class Article
    {
        #region DB Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Category Category { get; set; }
        #endregion

        public override string ToString()
        {
            return $"{Name}  ${Price}";
        }

    }
}
