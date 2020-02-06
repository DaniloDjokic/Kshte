using Equin.ApplicationFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WindowsFormsApp1.Managers;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Controllers
{
    public class HistoryController
    {
        private DataGridView transactionsGridView;
        private List<TransactionView> transactionViews;

        public HistoryController(DataGridView historyGridView)
        {
            this.transactionsGridView = historyGridView;
        }

        public void InitDataGridView()
        {
            transactionsGridView.AutoGenerateColumns = false;

            SetUpColumns();
        }
        public void RefreshDataGridView()
        {
            var allTransactions = TransactionManager.GetTransactionHistory();

            var history = allTransactions.Select(t => new TransactionView(t.ID, t.DateCreated, t.DateCompleted, t.TableID, t.CurrentPrice, t.PaidPrice, t.TotalPrice, t.TransactionDetails));
            var historyOrdered = history.OrderByDescending(t => DateTime.Parse(t.DateCreated));

            transactionViews = historyOrdered.ToList();

            transactionsGridView.DataSource = new BindingListView<TransactionView>(transactionViews);
        }
        public IEnumerable<TransactionView> GetSelected()
        {
            List<TransactionView> results = null;

            if (transactionsGridView.SelectedRows.Count > 0)
            {
                results = new List<TransactionView>(transactionsGridView.SelectedRows.Count);

                foreach (DataGridViewRow row in transactionsGridView.SelectedRows)
                {
                    ObjectView<TransactionView> objectView = (ObjectView<TransactionView>)row.DataBoundItem;
                    results.Add(objectView.Object);
                }
            }

            return results;
        }

        public void HandleCellDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dataGrid = ((DataGridView)sender);

            if (e.RowIndex >= 0 && e.RowIndex < dataGrid.Rows.Count)
            {
                var objectView = ((ObjectView<TransactionView>)dataGrid.Rows[e.RowIndex].DataBoundItem);
                var transactionView = objectView.Object;

                DisplayTransaction(transactionView);
            }
        }
        public void HandleColumnHeaderClick(DataGridViewCellMouseEventArgs e)
        {
            SortDataGridView(e.ColumnIndex);
        }

        private void DisplayTransaction(TransactionView transactionView)
        {
            if (transactionView == null)
            {
                throw new ArgumentNullException("Argument \"transactionView\" is null.");
            }

            var transactionDetails = transactionView.GetTransactionDetails();

            if (transactionDetails != null)
            {
                using (TransactionDetailsForm detailsForm = new TransactionDetailsForm(transactionDetails))
                {
                    detailsForm.ShowDialog();
                }
            }
            else
            {
                throw new ArgumentNullException("TransactionDetails is null for this transaction.");
            }
        }
        private void SetUpColumns()
        {
            transactionsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            PropertyInfo[] propertyInfos = typeof(TransactionView).GetProperties();
            DataGridViewTextBoxColumn newTextColumn;
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.PropertyType.IsNumericType() || propertyInfo.PropertyType == typeof(string))
                {
                    newTextColumn = new DataGridViewTextBoxColumn();

                    Attribute displayNameAttribute = propertyInfo.GetCustomAttribute(typeof(DisplayNameAttribute));
                    string displayName;
                    if (displayNameAttribute != null)
                    {
                        displayName = ((DisplayNameAttribute)displayNameAttribute).DisplayName;
                    }
                    else
                    {
                        displayName = propertyInfo.Name;
                    }

                    newTextColumn.Name = displayName;
                    newTextColumn.DataPropertyName = propertyInfo.Name;
                    newTextColumn.SortMode = DataGridViewColumnSortMode.Programmatic;

                    transactionsGridView.Columns.Add(newTextColumn);
                }
                else
                {
                    MessageBox.Show("One of the properties cannot be displayed as a column as its type is not expected. \n" +
                        $" Property Name: {propertyInfo.Name} Property Type: {propertyInfo.PropertyType}");
                }
            }
        }
        private void SortDataGridView(int columnIndex)
        {
            DataGridViewColumn newColumn = transactionsGridView.Columns[columnIndex];
            DataGridViewColumn oldColumn = transactionsGridView.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    transactionsGridView.SortOrder == SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // Sort the selected column.
            transactionsGridView.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                SortOrder.Ascending : SortOrder.Descending;
        }
    }
}
