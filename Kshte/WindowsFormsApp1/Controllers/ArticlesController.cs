using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Controllers
{
    public class ArticlesController
    {
        public static List<Article> GetAllArticles(Category category)
        {
            return MockData.allArticles[category];
        }
    }
}
