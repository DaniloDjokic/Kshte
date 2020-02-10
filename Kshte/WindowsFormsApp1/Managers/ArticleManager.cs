using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kshte.DBTools;
using Kshte.Models;

namespace Kshte.Managers
{
    public static class ArticleManager
    {
        private static List<Article> articles = null;
        public static IReadOnlyCollection<Article> AllArticles
        {
            get
            {
                if (articles == null)
                {
                    articles = DBContext.GetExistingArticles();
                }
                return articles.AsReadOnly();
            }
        }

        public static bool AddArticle(Article article)
        {
            if (!AllArticles.Contains(article))
            {
                if (AllArticles.Where(a => a.Name == article.Name).Count() == 0)
                {
                    var id = DBContext.AddNewArticle(article);

                    article.ID = id;
                    articles.Add(article);

                    return true;
                }

                return false;
            }
            else
                return false;    
        }

        public static bool RemoveArticle(Article article)
        {
            if (AllArticles.Contains(article))
            {
                DBContext.RemoveArticle(article);
                articles.Remove(article);
                return true;
            }
            else
                return false;
        }

        public static bool UpdateArticle(Article article)
        {
            if (AllArticles.Contains(article))
            {
                DBContext.UpdateDB(article);
                return true;
            }
            else
                return false;
        }

        public static IEnumerable<Article> GetByCategory(Category category)
        {
            return AllArticles.Where(x => x.Category.Equals(category));
        }
        public static Article GetById(int articleID)
        {
            Article result = null;

            foreach (var article in AllArticles)
            {
                if (article.ID == articleID)
                {
                    result = article;
                    break;
                }
            }

            return result;
        }
    }
}
