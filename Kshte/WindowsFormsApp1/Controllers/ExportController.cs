using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Managers;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Controllers
{
    public class ExportController
    {
        public readonly string fileExtension = ".khsd";

        private FolderBrowserDialog exportFolderBrowserDialog;
        private OpenFileDialog importOpenFileDialog;

        public string LastFileName { get; private set;  } = string.Empty;

        public ExportController()
        {
            this.exportFolderBrowserDialog = new FolderBrowserDialog();
            this.importOpenFileDialog = new OpenFileDialog();
            importOpenFileDialog.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
            importOpenFileDialog.DefaultExt = fileExtension;
        }

        public bool HandleExport(IEnumerable<TransactionView> transactionViews)
        {
            bool result = false;
            string fileName = string.Empty;

            if (transactionViews.Count() > 0)
            {
                if (exportFolderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = exportFolderBrowserDialog.SelectedPath;

                    if (Directory.Exists(path))
                    {
                        fileName = GenerateFileName(transactionViews);
                        string fullPath = Path.Combine(path, fileName);

                        ExportToFile(fullPath, transactionViews);

                        RemovePrompt(transactionViews);

                        result = true;
                    }
                    else
                    {
                        throw new InvalidOperationException("Entered directory does not exist.");
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("No transactions selected.");
            }

            LastFileName = fileName;

            return result;
        }
        public IEnumerable<TransactionView> HandleImport()
        {
            string fileName = string.Empty;

            IEnumerable<TransactionView> result = null;

            if (importOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = importOpenFileDialog.FileName;
                fileName = Path.GetFileName(filePath);

                if (File.Exists(filePath) && Path.GetExtension(filePath) == fileExtension)
                {
                    result = ImportFromFile(filePath);
                }
                else
                {
                    throw new InvalidOperationException($"Import file doesn't exist or its extension is not correct. Correct extension: {fileExtension}");
                }
            }

            LastFileName = fileName;

            return result;
        }

        private void ExportToFile(string fullPath, IEnumerable<TransactionView> transactionViews)
        {
            var serialized = JsonConvert.SerializeObject(transactionViews);
            File.WriteAllText(fullPath, serialized);
        }
        private IEnumerable<TransactionView> ImportFromFile(string fullPath)
        {
            if (!File.Exists(fullPath))
                throw new ArgumentException("File doesn't exist.");

            string serialized = File.ReadAllText(fullPath);
            IEnumerable<TransactionView> transactionViews = JsonConvert.DeserializeObject<IEnumerable<TransactionView>>(serialized);

            return transactionViews;
        }
        private void RemovePrompt(IEnumerable<TransactionView> transactionViews)
        {
            DialogResult dialogResult = MessageBox.Show("Remove exported transactions from the database?", "Remove prompt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                bool success = TransactionManager.RemoveExportedTransactions(transactionViews);

                if (!success)
                {
                    throw new Exception("Error removing exported transactions from the database.");
                }
            }
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
            stringBuilder.Append(DateTime.Parse(firstView.DateCreated).ToString("dd.MM.yyyy H-mm-ss"));
            stringBuilder.Append(" - ");
            stringBuilder.Append(DateTime.Parse(lastView.DateCreated).ToString("dd.MM.yyyy H-mm-ss"));
            stringBuilder.Append(fileExtension);

            return stringBuilder.ToString();
        }
    }
}
