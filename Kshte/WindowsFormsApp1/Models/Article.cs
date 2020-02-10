using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kshte.Managers;

namespace Kshte.Models
{
    public class Article
    {
        #region DB Properties
        public int ID { get; internal set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; internal set; }
        #endregion

        private Category category = null;
        public Category Category
        {
            get
            {
                if (category == null)
                {
                    category = CategoryManager.GetById(CategoryID);
                }
                return category;
            }
        }

        public Article() { }

        public Article(string Name, decimal Price, Category category)
        {
            this.Name = Name;
            this.Price = Price;

            if (!CategoryManager.Categories.Contains(category))
                throw new InvalidOperationException("This category does not exist.");

            this.category = category;
        }

        public override string ToString()
        {
            return $"{Name}  ${Price}";
        }

    }
}
