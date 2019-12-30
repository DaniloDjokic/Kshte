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
using WindowsFormsApplication1;

namespace WindowsFormsApp1
{
    public partial class TableForm : Form
    {
        private int articleListViewDivider = 10;
        private MainForm mainForm;
        private TransactionController transactionController;

        public TableForm(Transaction transaction, MainForm mainForm, int tableID)
        {
            InitializeComponent();
            transactionController = new TransactionController(transaction, tableID);
            transaction = transactionController.Transaction;

            InitForm(transaction, mainForm);
        }

        private void InitForm(Transaction transaction, MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.tableIDLabel.Text = $"Table: {transaction.TableID.ToString()}"; 

            InitActiveArticlesListView(transaction);
            InitAllArticlesListView();

            allArticlesListView.Visible = false;
        }

        private void InitAllArticlesListView()
        {
            //Format listviews
            allArticlesListView.View = View.Details;
            allArticlesListView.FullRowSelect = true;
            allArticlesListView.GridLines = true;
            allArticlesListView.HeaderStyle = ColumnHeaderStyle.None;

            //Create columns
            ColumnHeader nameColumn = new ColumnHeader();
            nameColumn.Width = 6 * (allArticlesListView.Width / articleListViewDivider);

            ColumnHeader priceColumn = new ColumnHeader();
            priceColumn.Width = 2 * (allArticlesListView.Width / articleListViewDivider);

            ColumnHeader addButtonColumn = new ColumnHeader();
            addButtonColumn.Width = allArticlesListView.Width / articleListViewDivider;

            //Add columns to listview
            allArticlesListView.Columns.Add(nameColumn);
            allArticlesListView.Columns.Add(priceColumn);
            allArticlesListView.Columns.Add(addButtonColumn);

            //Create extender and button
            ListViewExtender extender = new ListViewExtender(allArticlesListView);

            ListViewButtonColumn buttonAdd = new ListViewButtonColumn(2);
            buttonAdd.Click += addBtn_Click;
            buttonAdd.FixedWidth = true;
            extender.AddColumn(buttonAdd);
        }

        private void InitActiveArticlesListView(Transaction transaction)
        {
            //Format list view
            activeArticlesListView.View = View.Details;
            activeArticlesListView.FullRowSelect = true;
            activeArticlesListView.GridLines = true;
            activeArticlesListView.HeaderStyle = ColumnHeaderStyle.None;

            //Create columns
            ColumnHeader nameColumn = new ColumnHeader();
            nameColumn.Width = 5 * (activeArticlesListView.Width / articleListViewDivider);

            ColumnHeader priceColumn = new ColumnHeader();
            priceColumn.Width = 2* (activeArticlesListView.Width / articleListViewDivider);

            ColumnHeader payButtonColumn = new ColumnHeader();
            payButtonColumn.Width = activeArticlesListView.Width / articleListViewDivider;

            ColumnHeader deleteButtonColumn = new ColumnHeader();
            deleteButtonColumn.Width = activeArticlesListView.Width / articleListViewDivider;

            //Add columns to listview
            activeArticlesListView.Columns.Add(nameColumn);
            activeArticlesListView.Columns.Add(priceColumn);
            activeArticlesListView.Columns.Add(payButtonColumn);
            activeArticlesListView.Columns.Add(deleteButtonColumn);

            //Create extender and listview buttons
            ListViewExtender extender = new ListViewExtender(activeArticlesListView);

            ListViewButtonColumn buttonPay = new ListViewButtonColumn(2);
            buttonPay.Click += payBtn_Click;
            buttonPay.FixedWidth = true;
            extender.AddColumn(buttonPay);

            ListViewButtonColumn buttonDelete = new ListViewButtonColumn(3);
            buttonDelete.Click += deleteBtn_Click;
            buttonDelete.FixedWidth = true;
            extender.AddColumn(buttonDelete);

            //Add column data and buttons
            foreach (TransactionDetail detail in transaction.TransactionDetails)
            {
                if (!detail.PaidFor)
                {
                    ListViewItem articleRow = new ListViewItem(detail.Article.Name);
                    articleRow.Name = detail.ID.ToString();
                    articleRow.SubItems.Add(new ListViewItem.ListViewSubItem(articleRow, detail.EffectivePrice.ToString()));
                    articleRow.SubItems.Add(new ListViewItem.ListViewSubItem(articleRow, "$"));
                    articleRow.SubItems.Add(new ListViewItem.ListViewSubItem(articleRow, "X"));

                    activeArticlesListView.Items.Add(articleRow);
                }
               
            }
        }

        private void DisplayArticles(List<Article> articles)
        {
            allArticlesListView.Items.Clear();

            foreach (Article article in articles)
            {
                ListViewItem articleRow = new ListViewItem(article.Name);
                articleRow.Name = article.Name;
                articleRow.SubItems.Add(new ListViewItem.ListViewSubItem(articleRow, article.Price.ToString()));
                articleRow.SubItems.Add(new ListViewItem.ListViewSubItem(articleRow, "Add"));

                allArticlesListView.Items.Add(articleRow);
            }

            allArticlesListView.Visible = true;
        }

        private void AddArticleToTransaction(TransactionDetail detail)
        {
            AddArticleToActiveItems(detail);
        }

        private void AddArticleToActiveItems(TransactionDetail detail)
        {
            ListViewItem articleRow = new ListViewItem(detail.Article.Name);

            articleRow.Name = detail.ID.ToString();
            articleRow.SubItems.Add(new ListViewItem.ListViewSubItem(articleRow, detail.EffectivePrice.ToString()));
            articleRow.SubItems.Add(new ListViewItem.ListViewSubItem(articleRow, "$"));
            articleRow.SubItems.Add(new ListViewItem.ListViewSubItem(articleRow, "X"));

            activeArticlesListView.Items.Add(articleRow);
        }

        private void RemoveArticleFromTransaction(TransactionDetail detail, ListViewItem listItem)
        {
            transactionController.RemoveArticle(detail);
            activeArticlesListView.Items.Remove(listItem);
        }

        private void HandleCategoryClick(Category category)
        {
            DisplayArticles(ArticlesController.GetAllArticles(category));
            transactionController.SelectCategory(category);
        }

        #region EventHandlers

        private void addBtn_Click(object sender, ListViewColumnMouseEventArgs e)
        {
            Article article = ArticleManager.GetByCategory(transactionController.ActiveCategory).FirstOrDefault(a => a.Name == e.Item.Name); 
            AddArticleToTransaction(transactionController.AddArticle(article));
        }

        private void payBtn_Click(object sender, ListViewColumnMouseEventArgs e)
        {
            transactionController.PayArticle(transactionController.Transaction.TransactionDetails.FirstOrDefault(detail => detail.ID == Int32.Parse(e.Item.Name)));
            activeArticlesListView.Items.Remove(e.Item);
        }

        private void deleteBtn_Click(object sender, ListViewColumnMouseEventArgs e)
        {
            RemoveArticleFromTransaction(transactionController.Transaction.TransactionDetails.FirstOrDefault(a => a.ID == Int32.Parse(e.Item.Name)), e.Item);
        }
        
        private void sokoviBtn_Click(object sender, EventArgs e)
        {
            HandleCategoryClick(CategoryManager.GetByName("JUICE"));
        }

        private void pivoBtn_Click(object sender, EventArgs e)
        {
            HandleCategoryClick(CategoryManager.GetByName("BEER"));
        }

        private void topliNapiciBtn_Click(object sender, EventArgs e)
        {
            HandleCategoryClick(CategoryManager.GetByName("WARM DRINKS"));
        }

        private void zestinaBtn_Click(object sender, EventArgs e)
        {
            HandleCategoryClick(CategoryManager.GetByName("ALCOHOL"));
        }

        private void miscBtn_Click(object sender, EventArgs e)
        {
            HandleCategoryClick(CategoryManager.GetByName("COMBINATIONS"));
        }

        private void hranaBtn_Click(object sender, EventArgs e)
        {
            HandleCategoryClick(CategoryManager.GetByName("FOOD"));
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            if (transactionController.ArticleSelectMode)
            {
                this.allArticlesListView.Visible = false;
                transactionController.UnselectCategory();
            }
            else
                this.Close();
        }

        private void payAllBtn_Click(object sender, EventArgs e)
        {
            transactionController.PayAll();
            this.activeArticlesListView.Items.Clear();          
        }

        private void deleteOrderBtn_Click(object sender, EventArgs e)
        {
            transactionController.RemoveTransaction();
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            mainForm.RefreshTableView();
        }

        #endregion


    }
}
