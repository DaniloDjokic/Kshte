using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kshte.Models;
using Kshte.DBTools;

namespace Kshte.Managers
{
    public static class CategoryManager
    {
        private static List<Category> categories = null;
        public static IReadOnlyCollection<Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = DBContext.GetExistingCategories();
                }
                return categories.AsReadOnly();
            }
        }

        internal static Category GetById(int categoryID)
        {
            Category result = null;

            foreach (var category in Categories)
            {
                if (category.ID == categoryID)
                {
                    result = category;
                    break;
                }
            }

            return result;
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
        public static bool AddCategory(Category category)
        {
            if (!Categories.Contains(category))
            {
                var id = DBContext.AddNewCategory(category);

                category.ID = id;
                categories.Add(category);
                return true;
            }
            else
                return false;
        }
        public static bool UpdateCategory(Category category)
        {
            if (Categories.Contains(category))
            {
                DBContext.UpdateDB(category);
                return true;
            }
            else
                return false;
        }
    }
}
