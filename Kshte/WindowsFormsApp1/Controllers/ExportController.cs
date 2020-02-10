using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kshte.Managers;
using Kshte.Models;

namespace Kshte.Controllers
{
    public class ExportController
    {
        public readonly string ExportCondition = "Samo kompletirane transakcije se mogu eksportovati.";

        public readonly string FileExtension = ".khsd";

        private FolderBrowserDialog exportFolderBrowserDialog;
        private OpenFileDialog importOpenFileDialog;

        public string LastFileName { get; private set;  } = string.Empty;

        public ExportController()
        {
            this.exportFolderBrowserDialog = new FolderBrowserDialog();
            this.exportFolderBrowserDialog.Description = "Izaberite folder eksportovanja";
            
            this.importOpenFileDialog = new OpenFileDialog();
            importOpenFileDialog.Multiselect = false;
            importOpenFileDialog.DefaultExt = FileExtension;
        }

        public void HandleExport(IEnumerable<TransactionView> transactionViews)
        {
            string fileName = string.Empty;

            if (transactionViews.Count() > 0)
            {
                if (exportFolderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = exportFolderBrowserDialog.SelectedPath;

                    if (Directory.Exists(path))
                    {
                        if (transactionViews.All(tv => CanBeExported(tv)))
                        {
                            fileName = GenerateFileName(transactionViews);
                            string fullPath = Path.Combine(path, fileName);

                            ExportToFile(fullPath, transactionViews);

                            RemovePrompt(transactionViews);
                        }
                        else
                        {
                            throw new ArgumentException($"Neke od izabranih transakcija ne mogu biti eksportovane. Uslov: {ExportCondition}");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Izabrani direktorijum ne postoji.");
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Nijedna transakcija nije izabrana.");
            }

            LastFileName = fileName;
        }
        public IEnumerable<TransactionView> HandleImport()
        {
            string fileName = string.Empty;

            IEnumerable<TransactionView> result = null;

            if (importOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = importOpenFileDialog.FileName;
                fileName = Path.GetFileName(filePath);

                if (File.Exists(filePath) && Path.GetExtension(filePath) == FileExtension)
                {
                    result = ImportFromFile(filePath);
                }
                else
                {
                    throw new InvalidOperationException($"Import file doesn't exist or its extension is not correct. Correct extension: {FileExtension}");
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
            DialogResult dialogResult = MessageBox.Show("Obrisati eksportovane transakcije iz baze?", "Upit brisanja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            stringBuilder.Append(FileExtension);

            return stringBuilder.ToString();
        }
        public static bool CanBeExported(TransactionView transactionView)
        {
            if (string.IsNullOrWhiteSpace(transactionView.DateCompleted))
                return false;
            if (!DateTime.TryParse(transactionView.DateCompleted, out DateTime _))
                return false;

            return true;
        }
    }
}
