using Equin.ApplicationFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WindowsFormsApp1.Helpers;
using WindowsFormsApp1.Managers;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Controllers
{
    public class HistoryController
    {
        private List<TransactionView> transactionViews;

        public DataGridView TransactionsGridView { get; private set; }
        public DateTimeSelector DateTimeSelector { get; private set; }

        public HistoryController(DataGridView historyGridView, DateTimeSelector dateTimeSelector = null)
        {
            if (historyGridView == null)
                throw new ArgumentNullException("Grid view can't be null.");

            this.TransactionsGridView = historyGridView;
            this.DateTimeSelector = dateTimeSelector;
        }

        public void InitDataGridView()
        {
            TransactionsGridView.AutoGenerateColumns = false;

            SetUpColumns();
        }
        public void RefreshDataGridView()
        {
            var allTransactions = TransactionManager.GetTransactionHistory();

            if (allTransactions != null)
            {
                var history = allTransactions.Select(t => new TransactionView(t.ID, t.DateCreated, t.DateCompleted, t.TableID, t.CurrentPrice, t.PaidPrice, t.TotalPrice, t.TransactionDetails));

                SetDataGridView(history);
            }
        }
        public void SetDataGridView(IEnumerable<TransactionView> transactionViews)
        {
            var ordered = transactionViews/*transactionViews.OrderByDescending(t => DateTime.Parse(t.DateCreated))*/;

            this.transactionViews = ordered.ToList();

            UpdateSelector();

            TransactionsGridView.DataSource = new BindingListView<TransactionView>(this.transactionViews);
        }
        public IEnumerable<TransactionView> GetSelected()
        {
            List<TransactionView> results = null;

            if (TransactionsGridView.SelectedRows.Count > 0)
            {
                results = new List<TransactionView>(TransactionsGridView.SelectedRows.Count);

                foreach (DataGridViewRow row in TransactionsGridView.SelectedRows)
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
        public int HandleSelect()
        {
            return SelectByChosenBounds();
        }

        private void DisplayTransaction(TransactionView transactionView)
        {
            if (transactionView == null)
            {
                throw new ArgumentNullException("Argument \"transactionView\" is null.");
            }

            var transactionDetails = transactionView.TransactionDetailViews;

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
            TransactionsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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

                    TransactionsGridView.Columns.Add(newTextColumn);
                }
            }
        }
        private void SortDataGridView(int columnIndex)
        {
            DataGridViewColumn newColumn = TransactionsGridView.Columns[columnIndex];
            DataGridViewColumn oldColumn = TransactionsGridView.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    TransactionsGridView.SortOrder == SortOrder.Ascending)
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
            TransactionsGridView.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                SortOrder.Ascending : SortOrder.Descending;
        }

        private void UpdateSelector()
        {
            if (DateTimeSelector != null)
            {
                var transactionViews = this.transactionViews.OrderBy(tv => DateTime.Parse(tv.DateCreated));

                if (transactionViews.Count() >= 1)
                {
                    TransactionView firstView = transactionViews.First();
                    TransactionView lastView = transactionViews.Last();

                    DateTimeSelector.RefreshBoundaries(DateTime.Parse(firstView.DateCreated), DateTime.Parse(lastView.DateCreated));
                }
            }           
        }
        private int SelectByChosenBounds()
        {
            int numOfSelected = 0;

            if (DateTimeSelector != null)
            {
                FromToDateTime selectedBounds = DateTimeSelector.GetSelectedBounds();

                foreach (DataGridViewRow row in TransactionsGridView.Rows)
                {
                    ObjectView<TransactionView> objectView = (ObjectView<TransactionView>)row.DataBoundItem;
                    TransactionView transactionView = objectView.Object;

                    DateTime dateCreated = DateTime.Parse(transactionView.DateCreated);

                    if (selectedBounds.IsWithinBounds(dateCreated))
                    {
                        row.Selected = true;
                        numOfSelected++;
                    }
                    else
                        row.Selected = false;
                }
            }

            return numOfSelected;
        }
    }
}
