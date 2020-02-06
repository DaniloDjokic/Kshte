using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Controllers
{
    public class ExportController
    {
        private FolderBrowserDialog exportFolderBrowserDialog;

        public ExportController()
        {
            this.exportFolderBrowserDialog = new FolderBrowserDialog();
            exportFolderBrowserDialog.RootFolder = Environment.SpecialFolder.MyDocuments;
        }

        public void HandleExport(IEnumerable<TransactionView> transactionViews)
        {
            if (transactionViews.Count() > 0)
            {
                if (exportFolderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = exportFolderBrowserDialog.SelectedPath;

                    if (Directory.Exists(path))
                    {
                        string fileName = GenerateFileName(transactionViews);
                        string fullPath = Path.Combine(path, fileName);

                        ExportToFile(fullPath, transactionViews);
                    }
                }
            }

            throw new NotImplementedException();
        }

        private void ExportToFile(string fullPath, IEnumerable<TransactionView> transactionViews)
        {
            throw new NotImplementedException();
        }

        private void ImportFromFile(string fullPath)
        {
            throw new NotImplementedException();
        }

        private string GenerateFileName(IEnumerable<TransactionView> transactionViews)
        {
            if (transactionViews == null || transactionViews.Count() < 1)
            {
                throw new ArgumentException();
            }

            var orderedViews = transactionViews.OrderBy(tv => DateTime.Parse(tv.DateCreated));

            TransactionView firstView = orderedViews.First();
            TransactionView lastView = orderedViews.Last();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(firstView.DateCreated);
            stringBuilder.Append(" -> ");
            stringBuilder.Append(lastView.DateCreated);

            return stringBuilder.ToString();
        }
    }
}
