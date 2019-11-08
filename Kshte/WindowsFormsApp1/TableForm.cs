using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApplication1;

namespace WindowsFormsApp1
{
    public partial class TableForm : Form
    {
        private int articleListViewDivider = 10;
        private Category activeCategory;
        private Transaction transaction;
        private bool articleSelectMode = false;
        private MainForm mainForm;

        public TableForm(Transaction transaction, MainForm mainForm)
        {
            InitializeComponent();
            InitForm(transaction, mainForm);
        }

        private void InitForm(Transaction transaction, MainForm mainForm)
        {
            this.transaction = transaction;
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
            nameColumn.Width = 3 * (allArticlesListView.Width / articleListViewDivider);

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
            buttonAdd.Click += OnButtonAddClick;
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
            buttonPay.Click += OnButtonPayClick;
            buttonPay.FixedWidth = true;
            extender.AddColumn(buttonPay);

            ListViewButtonColumn buttonDelete = new ListViewButtonColumn(3);
            buttonDelete.Click += OnButtonDeleteClick;
            buttonDelete.FixedWidth = true;
            extender.AddColumn(buttonDelete);

            //Add column data and buttons
            foreach (Article article in transaction.Articles)
            {
                AddArticleToActiveItems(article);
            }
        }

        

        private void DisplayArticles(Category category)
        {
            List<Article> articles = MockData.allArticles[category];

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

        private void AddArticleToTransaction(Article article)
        {
            AddArticleToActiveItems(article);
            this.transaction.Articles.Add(article);
        }

        private void AddArticleToActiveItems(Article article)
        {
            ListViewItem articleRow = new ListViewItem(article.Name);
            articleRow.Name = article.Name;
            articleRow.SubItems.Add(new ListViewItem.ListViewSubItem(articleRow, article.Price.ToString()));
            articleRow.SubItems.Add(new ListViewItem.ListViewSubItem(articleRow, "$"));
            articleRow.SubItems.Add(new ListViewItem.ListViewSubItem(articleRow, "X"));

            activeArticlesListView.Items.Add(articleRow);
        }

        private void RemoveArticleFromTransaction(Article article, ListViewItem listItem)
        {
            this.transaction.Articles.Remove(article);
            activeArticlesListView.Items.Remove(listItem);
        }

        #region EventHandlers

        private void OnButtonAddClick(object sender, ListViewColumnMouseEventArgs e)
        {
            Article article = MockData.allArticles[activeCategory].FirstOrDefault(a => a.Name == e.Item.Name);
            AddArticleToTransaction(article);
        }

        private void OnButtonPayClick(object sender, ListViewColumnMouseEventArgs e)
        {
            //Add payment transaction
            activeArticlesListView.Items.Remove(e.Item);
        }

        private void OnButtonDeleteClick(object sender, ListViewColumnMouseEventArgs e)
        {
            RemoveArticleFromTransaction(MockData.allArticles[activeCategory].FirstOrDefault(a => a.Name == e.Item.Name), e.Item);
        }
        

        private void sokoviBtn_Click(object sender, EventArgs e)
        {
            DisplayArticles(Category.JUICE);
            activeCategory = Category.JUICE;
            articleSelectMode = true;
        }

        private void pivoBtn_Click(object sender, EventArgs e)
        {
            DisplayArticles(Category.BEER);
            activeCategory = Category.BEER;
            articleSelectMode = true;

        }

        private void topliNapiciBtn_Click(object sender, EventArgs e)
        {
            DisplayArticles(Category.WARM_DRINKS);
            activeCategory = Category.WARM_DRINKS;
            articleSelectMode = true;

        }

        private void zestinaBtn_Click(object sender, EventArgs e)
        {
            DisplayArticles(Category.ALCOHOL);
            activeCategory = Category.ALCOHOL;
            articleSelectMode = true;

        }

        private void miscBtn_Click(object sender, EventArgs e)
        {
            DisplayArticles(Category.MISC);
            activeCategory = Category.MISC;
            articleSelectMode = true;

        }

        private void hranaBtn_Click(object sender, EventArgs e)
        {
            DisplayArticles(Category.FOOD);
            activeCategory = Category.FOOD;
            articleSelectMode = true;

        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            if (articleSelectMode)
                this.allArticlesListView.Visible = false;
            else
                this.Close();
        }

        private void payAllBtn_Click(object sender, EventArgs e)
        {
            //Add transaction to db
            this.transaction.Articles.Clear();
            this.activeArticlesListView.Items.Clear();          
        }

        private void deleteOrderBtn_Click(object sender, EventArgs e)
        {
            this.transaction.Articles.Clear();
            this.activeArticlesListView.Items.Clear();
            MockData.activeTransactions.Remove(transaction);
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            mainForm.RefreshForm();
        }

        #endregion


    }
}
