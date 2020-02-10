using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kshte.Models;

namespace Kshte.Controllers
{
    public class CategoryController
    {
        public Category ActiveCategory { get; protected set; }
        public bool ArticleSelectMode { get; protected set; }

        /// <summary>
        /// Selects currently active article category
        /// </summary>
        /// <param name="category">The category to select</param>
        public void SelectCategory(Category category)
        {
            this.ActiveCategory = category;
            ArticleSelectMode = true;
        }

        /// <summary>
        /// Returns mode to category select
        /// </summary>
        public void UnselectCategory()
        {
            ArticleSelectMode = false;
        }
    }
}
