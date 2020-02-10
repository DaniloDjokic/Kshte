using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kshte.Managers;
using Kshte.Models;

namespace Kshte.Controllers
{
    public class AdminController : CategoryController
    {
        public void UpdateArticle(Article articleToUpdate)
        {
            try
            {
                if (!ArticleManager.UpdateArticle(articleToUpdate))
                    throw new Exception("Couldn't update article.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Artikal ne moze biti promenjen: " + e.GetFullMessage(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public bool AddNewArticle(Article article)
        {
            try
            {
                return ArticleManager.AddArticle(article);
            }
            catch (Exception e)
            {
                MessageBox.Show("Artikal ne moze biti dodat: " + e.GetFullMessage() , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Artikal ne moze biti obrisan: " + e.GetFullMessage(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
