using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Managers;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Controllers
{
    public class AdminController : CategoryController
    {
        public void UpdateArticle(Article newArticle, Article oldArticle)
        {
            Article ar = ArticleManager.GetByCategory(oldArticle.Category).FirstOrDefault(a => a == oldArticle);

            ar.Name = newArticle.Name;
            ar.Price = newArticle.Price;
        }

        public bool AddNewArticle(Article article)
        {
            //Article ar = MockData.allArticles[article.Category].FirstOrDefault(a => a.Name == article.Name);

            //if (ar != null)
            //    return false;

            //MockData.allArticles[article.Category].Add(article);

            try
            {
                return ArticleManager.AddArticle(article);
            }
            catch (Exception e)
            {
                MessageBox.Show("Artikal ne moze biti dodat: " + e.GetFullMessage() , "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void RemoveArticle(Article article)
        {
            try
            {
                ArticleManager.RemoveArticle(article);
            }
            catch (Exception e)
            {
                MessageBox.Show("Artikal ne moze biti obrisan: " + e.GetFullMessage(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
