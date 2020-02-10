using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kshte.Managers;
using Kshte.Models;

namespace Kshte.Controllers
{
    public class ArticlesController
    {
        public static List<Article> GetAllArticles(Category category)
        {
            return ArticleManager.GetByCategory(category).ToList();
        }
    }
}
