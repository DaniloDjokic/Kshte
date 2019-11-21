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
    public partial class MainForm : Form
    {
        public AdminController AdminController { get; private set; }
        private ArticleDialog articleDialog;

        private List<Button> tableBtns = new List<Button>();

        private Color activeTableColor = Color.Red;
        private Color inactiveTableColor = Color.White;
        private int adminListViewDevider = 10;
        public MainForm()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            InitTableView();
            InitAdminView();
        }

 
        #region TableView

        private void InitTableView()
        {
            FillActiveTablesListBox();
            MarkActiveTableButtons();
            AttachEventsToButtons();
        }

        private void FillActiveTablesListBox()
        {
            this.ActiveTransactionsListBox.DataSource = TransactionManager.ActiveTransactions;
            foreach (Control control in Helpers.GetControlsRecursive(this.TabControl))
            {
                if (control.Name.Contains("table"))
                    tableBtns.Add(control as Button);
            }
        }

        public void RefreshTableView()
        {
            RefreshActiveTransactionsList();
            MarkActiveTableButtons();
        }

        private void RefreshActiveTransactionsList()
        {
            ActiveTransactionsListBox.DataSource = null;
            ActiveTransactionsListBox.DataSource = TransactionManager.ActiveTransactions;
        }

        private void MarkActiveTableButtons()
        {
            foreach (Button btn in tableBtns)
                btn.BackColor = inactiveTableColor;

            foreach(Transaction transaction in TransactionManager.ActiveTransactions)
            {
                Button tableBtn = tableBtns.Where(t => t.Name.Contains(transaction.TableID.ToString())).FirstOrDefault();
                tableBtn.BackColor = activeTableColor;
            }
        }

        private void AttachEventsToButtons()
        {
            foreach(Button button in tableBtns)
            {
                button.Click += new EventHandler(OpenTableForm);
            }
        }

        private void OpenTableForm(object sender, EventArgs args)
        {
            Transaction transaction = TableManager.GetById(Int32.Parse((sender as Button).Text)).CurrentTransaction;
            Form tableForm = new TableForm(transaction, this, Int32.Parse((sender as Button).Text));
            tableForm.ShowDialog();
        }
        #endregion

        #region AdminView

        private void SetTitleLabel(string text)
        {
            titleLabel.Text = text;
        }

        private void InitAdminView()
        {
            AdminController = new AdminController();

            SetTitleLabel("Categories");
            SetNavigationControls(false);
            InitAdminArticlesListView();
        }

        private void SetNavigationControls(bool status)
        {
            adminArticlesListView.Visible = status;
            backBtn.Visible = status;
            addProductBtn.Visible = status;
        }

        private void InitAdminArticlesListView()
        {
            //Format listviews
            adminArticlesListView.View = View.Details;
            adminArticlesListView.FullRowSelect = true;
            adminArticlesListView.GridLines = true;
            adminArticlesListView.HeaderStyle = ColumnHeaderStyle.None;

            //Create columns
            ColumnHeader nameColumn = new ColumnHeader();
            nameColumn.Width = 3 * (adminArticlesListView.Width / adminListViewDevider);

            ColumnHeader priceColumn = new ColumnHeader();
            priceColumn.Width = 2 * (adminArticlesListView.Width / adminListViewDevider);

            ColumnHeader modifyBtnColumn = new ColumnHeader();
            modifyBtnColumn.Width = adminArticlesListView.Width / adminListViewDevider;

            ColumnHeader removeBtnColumn = new ColumnHeader();
            removeBtnColumn.Width = adminArticlesListView.Width / adminListViewDevider;

            //Add columns to listview
            adminArticlesListView.Columns.Add(nameColumn);
            adminArticlesListView.Columns.Add(priceColumn);
            adminArticlesListView.Columns.Add(modifyBtnColumn);
            adminArticlesListView.Columns.Add(removeBtnColumn);

            //Create extender and button
            ListViewExtender extender = new ListViewExtender(adminArticlesListView);

            ListViewButtonColumn btnModify = new ListViewButtonColumn(2);
            btnModify.Click += modifyBtn_Click;
            btnModify.FixedWidth = true;
            extender.AddColumn(btnModify);

            ListViewButtonColumn btnRemove = new ListViewButtonColumn(3);
            btnRemove.Click += removeBtn_Click;
            btnRemove.FixedWidth = true;
            extender.AddColumn(btnRemove);
        }

        public void DisplayArticles(List<Article> articles)
        {
            adminArticlesListView.Items.Clear();

            foreach (Article article in articles)
            {
                ListViewItem articleRow = new ListViewItem(article.Name);
                articleRow.Name = article.Name;
                articleRow.SubItems.Add(new ListViewItem.ListViewSubItem(articleRow, article.Price.ToString()));
                articleRow.SubItems.Add(new ListViewItem.ListViewSubItem(articleRow, "Mod"));
                articleRow.SubItems.Add(new ListViewItem.ListViewSubItem(articleRow, "X"));

                adminArticlesListView.Items.Add(articleRow);
            }

            adminArticlesListView.Visible = true;
        }

        private void HandleCategoryClick(Category category)
        {
            DisplayArticles(ArticlesController.GetAllArticles(category));
            SetTitleLabel(category.Name);
            SetNavigationControls(true);
            AdminController.SelectCategory(category);
        }

        private void modifyBtn_Click(object sender, ListViewColumnMouseEventArgs e)
        {
            Article article = ArticleManager.GetByCategory(AdminController.ActiveCategory).FirstOrDefault(a => a.Name == e.Item.Name);
            if (article != null)
            {
                articleDialog = new ArticleDialog(AdminController.ActiveCategory, this, article);
                articleDialog.ShowDialog();
            }
        }

        private void removeBtn_Click(object sender, ListViewColumnMouseEventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to delete this item?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                AdminController.RemoveArticle(ArticleManager.GetByCategory(AdminController.ActiveCategory).FirstOrDefault(a => a.Name == e.Item.Name));
                DisplayArticles(ArticleManager.GetByCategory(AdminController.ActiveCategory).ToList());
            }
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
            if (AdminController.ArticleSelectMode)
            {
                SetNavigationControls(false);
                SetTitleLabel("Categories");
                AdminController.UnselectCategory();
            }
        }

        private void addProductBtn_Click(object sender, EventArgs e)
        {
            articleDialog = new ArticleDialog(AdminController.ActiveCategory, this);
            articleDialog.ShowDialog();
        }
        #endregion


    }
}
