using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsApp1.Helpers
{
    public class KshteSettings : ConfigurationSection
    {
        private static KshteSettings settings
            = ConfigurationManager.GetSection("KshteSettings") as KshteSettings;

        public static KshteSettings Settings
        {
            get
            {
                return settings;
            }
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        [ConfigurationProperty("TableMinID"
            , DefaultValue = 0
            , IsRequired = true)]
        [IntegerValidator(MinValue = 0
            , MaxValue = 100)]
        public int TableMinID
        {
            get => (int)this["TableMinID"];
            set => this["TableMinID"] = value;
        }

        [ConfigurationProperty("TableMaxID"
            , DefaultValue = 14
            , IsRequired = true)]
        [IntegerValidator(MinValue = 1
            , MaxValue = 100)]
        public int TableMaxID
        {
            get => (int)this["TableMaxID"];
            set => this["TableMaxID"] = value;
        }

        [ConfigurationProperty("DocumentsFolderPath"
            , DefaultValue = ""
            , IsRequired = true)]
        public string DocumentsFolderPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace((string) this["DocumentsFolderPath"]))
                {
                    this["DocumentsFolderPath"] =
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                            DocumentsFolderName);
                }
                return (string)this["DocumentsFolderPath"];
            }
            set
            {
                try
                {
                    var fullPath = Path.GetFullPath(value);
                    if (fullPath.EndsWith($"{DocumentsFolderName}{Path.DirectorySeparatorChar}"))
                    {
                        this["DocumentsFolderPath"] = fullPath;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error setting documents folder path.", e);
                }
            }
        }

        [ConfigurationProperty("DocumentsFolderName"
            , DefaultValue = "Kshte"
            , IsRequired = true)]
        public string DocumentsFolderName
        {
            get => (string)this["DocumentsFolderName"];
            set => this["DocumentsFolderName"] = value;
        }

        [ConfigurationProperty("DatabaseFolderName"
            , DefaultValue = "Database"
            , IsRequired = true)]
        public string DatabaseFolderName
        {
            get => (string)this["DatabaseFolderName"];
            set => this["DatabaseFolderName"] = value;
        }

        [ConfigurationProperty("DatabaseFileName"
            , DefaultValue = "Data.db"
            , IsRequired = true)]
        public string DatabaseFileName
        {
            get => (string)this["DatabaseFileName"];
            set => this["DatabaseFileName"] = value;
        }

        [ConfigurationProperty("SQLiteConnectionString"
            , DefaultValue = "DataSource={0};Version=3;"
            , IsRequired = true)]
        public string ConnectionString
        {
            get
            {
                string dbPath = DatabaseFilePath;
                return string.Format((string)this["SQLiteConnectionString"], dbPath);
            }
            set
            {
                //Add more checks, maybe password?
                if (!value.Contains("{0}"))
                    throw new Exception(
                        "Invalid connection string: String needs to contain {0} as placeholder for database file path.");
                this["SQLiteConnectionString"] = value;
            }
        }

        public string DatabaseFolderPath =>
            Path.Combine(DocumentsFolderPath, DatabaseFolderName);
        public string DatabaseFilePath => Path.Combine(DatabaseFolderPath, DatabaseFileName);

        [ConfigurationProperty("Categories"
            , DefaultValue = "JUICE, BEER, WARM DRINKS, ALCOHOL, COMBINATIONS, FOOD"
            , IsRequired = true)]
        public string Categories
        {
            get => (string)this["Categories"];
            set => this["Categories"] = value;
        }

        public List<string> CategoryList => Categories.Split(',').Select(c => c.Trim(' ')).ToList();
    }
}