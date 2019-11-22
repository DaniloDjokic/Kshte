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
