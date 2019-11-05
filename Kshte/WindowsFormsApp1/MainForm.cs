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
        private List<Button> tableBtns = new List<Button>();

        private Color activeTableColor = Color.Red;

        public MainForm()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            FillActiveTablesListBox();
            MarkActiveTableButtons();
            AttachEventsToButtons();
        }

        private void FillActiveTablesListBox()
        {
            activeTransactions = MockData.activeTransactions;
            this.ActiveTransactionsListBox.DataSource = activeTransactions;
        }

        private void MarkActiveTableButtons()
        {
            foreach(Control control in Helpers.GetControlsRecursive(this.TabControl))
            {
                if (control.Name.Contains("table")) 
                    tableBtns.Add(control as Button);
            }

            foreach(Transaction transaction in activeTransactions)
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
            Transaction transaction = activeTransactions.Where(t => t.TableID == Int32.Parse((sender as Button).Text)).FirstOrDefault();
            Form tableForm = new TableForm(transaction);
            tableForm.ShowDialog();
        }
    }
}
