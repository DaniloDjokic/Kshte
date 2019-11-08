using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.DBTools;

namespace WindowsFormsApp1.Managers
{
    public static class CategoryManager
    {
        private static IReadOnlyCollection<Category> categories = null;
        public static IReadOnlyCollection<Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = DBContext.GetExistingCategories().AsReadOnly();
                }
                return categories;
            }
            private set
            {
                categories = value;
            }
        }

        public static Category GetByName(string name, int ID = 0)
        {
            Category result = null;

            name = name.Normalize();

            foreach (var category in Categories)
            {
                if (category.Name == name)
                {
                    if (ID == 0 || category.ID == ID)
                    {
                        result = category;
                        break;
                    }
                }
            }

            return result;
        }

        public static void AddCategory(Category category)
        {
            var id = DBContext.AddNewCategory(category);

            category.ID = id;

            var temp = categories.ToList();
            temp.Add(category);
            categories = temp.AsReadOnly();
        }
    }
}
