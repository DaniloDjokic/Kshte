using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Controllers
{
    public class AdminController : CategoryController
    {
        public void UpdateArticle(Article newArticle, Article oldArticle)
        {
            Article ar = MockData.allArticles[oldArticle.Category].FirstOrDefault(a => a == oldArticle);

            ar.Name = newArticle.Name;
            ar.Price = newArticle.Price;
        }

        public bool AddNewArticle(Article article)
        {
            Article ar = MockData.allArticles[article.Category].FirstOrDefault(a => a.Name == article.Name);
            
            if (ar != null)
                return false;

            MockData.allArticles[article.Category].Add(article);
            return true;
        }

        public void RemoveArticle(Article article)
        {
            MockData.allArticles[article.Category].Remove(article);
        }
    }
}
