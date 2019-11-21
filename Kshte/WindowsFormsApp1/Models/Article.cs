using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Managers;

namespace WindowsFormsApp1.Models
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

        public Article(string name, decimal price, Category category)
        {
            Name = name;
            Price = price;

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
