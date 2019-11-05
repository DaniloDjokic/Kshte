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

namespace WindowsFormsApp1
{
    public partial class TableForm : Form
    {
        public TableForm(Transaction transaction)
        {
            InitializeComponent();
            InitForm(transaction);
        }

        private void InitForm(Transaction transaction)
        {
            activeArticlesListBox.View = View.Details;

            ColumnHeader columnHeader = new ColumnHeader();
            columnHeader.Width = activeArticlesListBox.Width;

            activeArticlesListBox.Columns.Add(columnHeader);

            foreach(Article a in transaction.Articles)
            {
                activeArticlesListBox.Items.Add(new ListViewItem(a.ToString()));
            }

            activeArticlesListBox.GridLines = true;
            activeArticlesListBox.HeaderStyle = ColumnHeaderStyle.None;
        }
    }
}
