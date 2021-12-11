using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Controllers;
using WindowsFormsApp1.Managers;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class ArticleDialog : Form
    {
        private Article articleToEdit;
        private AdminController adminController;
        private Category category;
        private MainForm mainForm;

        public ArticleDialog(Category category, MainForm mainForm, Article articleToEdit = null)
        {
            InitializeComponent();

            this.adminController = mainForm.AdminController;
            this.category = category;
            this.mainForm = mainForm;

            if(articleToEdit != null)
            {
                this.articleToEdit = articleToEdit;
                nameTxtBox.Text = articleToEdit.Name;
                priceTxtBox.Text = articleToEdit.Price.ToString();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            mainForm.DisplayArticles(ArticleManager.GetByCategory(adminController.ActiveCategory).ToList());
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                DisplayErrorMessage("Invalid input");
            }
            else
            {
                if (articleToEdit != null)
                {
                    articleToEdit.Name = nameTxtBox.Text;
                    articleToEdit.Price = int.Parse(priceTxtBox.Text);

                    adminController.UpdateArticle(articleToEdit);
                    this.Close();
                }
                else
                {
                    Article article = new Article(nameTxtBox.Text, int.Parse(priceTxtBox.Text), category);

                    if (!adminController.AddNewArticle(article))
                    {
                        DisplayErrorMessage("Article name exists");
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }

        private bool ValidateInput()
        {
            if(nameTxtBox.Text == "" || 
               priceTxtBox.Text == "" || 
               !Int32.TryParse(priceTxtBox.Text, out int parsedPrice) ||
               parsedPrice < 0)
            {
                return false;
            }
            return true;
        }

        private void DisplayErrorMessage(string error)
        {
            errorLbl.Text = error;
            errorLbl.Visible = true;
        }
    }
}
