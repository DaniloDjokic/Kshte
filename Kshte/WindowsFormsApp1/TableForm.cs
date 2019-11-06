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

        public TableForm(Transaction transaction)
        {
            InitializeComponent();
            InitForm(transaction);
        }

        private void InitForm(Transaction transaction)
        {
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
                ListViewItem name = new ListViewItem(article.Name);
                name.SubItems.Add(new ListViewItem.ListViewSubItem(name, article.Price.ToString()));
                name.SubItems.Add(new ListViewItem.ListViewSubItem(name, "$"));
                name.SubItems.Add(new ListViewItem.ListViewSubItem(name, "/"));

                activeArticlesListView.Items.Add(name);
            }
        }

        private void DisplayArticles(Category category)
        {
            List<Article> articles = MockData.allArticles[category];

            allArticlesListView.Items.Clear();

            foreach (Article article in articles)
            {
                ListViewItem name = new ListViewItem(article.Name);
                name.SubItems.Add(new ListViewItem.ListViewSubItem(name, article.Price.ToString()));
                name.SubItems.Add(new ListViewItem.ListViewSubItem(name, "Add"));

                allArticlesListView.Items.Add(name);
            }

            allArticlesListView.Visible = true;
        }

        #region EventHandlers

        private void OnButtonAddClick(object sender, ListViewColumnMouseEventArgs e)
        {
            MessageBox.Show("SiSa", "Tit", MessageBoxButtons.OK);
        }

        private void OnButtonPayClick(object sender, ListViewColumnMouseEventArgs e)
        {
            MessageBox.Show("PENIS", "PEN", MessageBoxButtons.OK);
        }

        private void OnButtonDeleteClick(object sender, ListViewColumnMouseEventArgs e)
        {
            MessageBox.Show("BAGOMA", "BAG", MessageBoxButtons.OK);
        }
        

        private void sokoviBtn_Click(object sender, EventArgs e)
        {
            DisplayArticles(Category.JUICE);
        }

        private void pivoBtn_Click(object sender, EventArgs e)
        {
            DisplayArticles(Category.BEER);
        }

        private void topliNapiciBtn_Click(object sender, EventArgs e)
        {
            DisplayArticles(Category.WARM_DRINKS);
        }

        private void zestinaBtn_Click(object sender, EventArgs e)
        {
            DisplayArticles(Category.ALCOHOL);
        }

        private void miscBtn_Click(object sender, EventArgs e)
        {
            DisplayArticles(Category.MISC);
        }

        private void hranaBtn_Click(object sender, EventArgs e)
        {
            DisplayArticles(Category.FOOD);
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            this.allArticlesListView.Visible = false;
        }
        #endregion


    }
}
