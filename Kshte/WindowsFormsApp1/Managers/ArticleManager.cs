using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DBTools;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Managers
{
    public static class ArticleManager
    {
        private static IReadOnlyCollection<Article> articles = null;
        public static IReadOnlyCollection<Article> AllArticles
        {
            get
            {
                if (articles == null)
                {
                    articles = DBContext.GetExistingArticles().AsReadOnly();
                }
                return articles;
            }
            private set
            {
                articles = value;
            }
        }

        public static void AddArticle(Article article)
        {
            var id = DBContext.AddNewArticle(article);

            article.ID = id;

            var temp = articles.ToList();
            temp.Add(article);
            articles = temp.AsReadOnly();
        }

        public static IReadOnlyCollection<Article> GetByCategory(Category category)
        {
            List<Article> result = new List<Article>();

            foreach (var article in AllArticles)
            {              
                if (article.Category.Equals(category))
                {
                    result.Add(article);
                }
            }

            return result.AsReadOnly();
        }
    }
}
