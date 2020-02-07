using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class CategoryView
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [Newtonsoft.Json.JsonConstructor]
        public CategoryView(int ID, string Name) 
        {
            this.ID = ID;
            this.Name = Name;
        }

        public CategoryView(Category category)
        {
            this.ID = category.ID;
            this.Name = category.Name;
        }

    }
}
