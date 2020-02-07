using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class ArticleView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public CategoryView Category { get; set; }

        [Newtonsoft.Json.JsonConstructor]
        public ArticleView(int ID, string Name, decimal Price, CategoryView Category) 
        {
            this.ID = ID;
            this.Name = Name;
            this.Price = Price;
            this.Category = Category;
        }

        public ArticleView(Article article)
        {
            this.ID = article.ID;
            this.Name = article.Name;
            this.Price = article.Price;
            this.Category = new CategoryView(article.Category);
        }

        public override string ToString()
        {
            return $"{Name}  ${Price}";
        }
    }
}
