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
    public partial class MainForm : Form
    {
        private List<Transaction> activeTransactions = new List<Transaction>();

        public MainForm()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            FillActiveTablesListBox();
            MarkActiveTableButtons();
        }

        private void FillActiveTablesListBox()
        {
            activeTransactions = MockData.activeTransactions;
            this.ActiveTransactionsListBox.DataSource = activeTransactions;
        }

        private void MarkActiveTableButtons()
        {
            List<Button> tableBtns = new List<Button>();

            foreach(Control control in this.TabControl.TabPages[0])
            {
                if (control.Name.Contains("table"))
                    tableBtns.Add(control as Button);
            }

            foreach(Transaction transaction in activeTransactions)
            {
                Button tableBtn = tableBtns.Where(t => t.Name.Contains(transaction.TableID.ToString())).FirstOrDefault();
                tableBtn.BackColor = Color.Red;
            }
        }
    }
}
