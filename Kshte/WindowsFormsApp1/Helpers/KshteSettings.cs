using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms.VisualStyles;

namespace Kshte.Helpers
{
    public class KshteSettings : ConfigurationSection
    {

        private static readonly Lazy<Configuration> Configuration = new Lazy<Configuration>(
            () =>
            {
                string exePath = Process.GetCurrentProcess().MainModule.FileName;
                //string exeName = Path.GetFileName(exePath);
                // string configName = exeName + ".config";
                return ConfigurationManager.OpenExeConfiguration(exePath);
            });

        private static readonly Lazy<KshteSettings> settings = new Lazy<KshteSettings>(
            () =>
            {
                return Configuration.Value.GetSection("KshteSettings") as KshteSettings;
            });

        public static KshteSettings Settings
        {
            get => settings.Value;
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        [ConfigurationProperty("LogFolderPath"
            , DefaultValue = ""
            , IsRequired = true)]
        public string LogFolderPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace((string)this["LogFolderPath"]))
                {
                    this["LogFolderPath"] =
                        Path.Combine(DocumentsFolderPath, "Logs");
                    Configuration.Value.Save();
                }
                return (string)this["LogFolderPath"];
            }
            set
            {
                this["LogFolderPath"] = value;
                Configuration.Value.Save();
            }
        }

        [ConfigurationProperty("TableMinID"
            , DefaultValue = 0
            , IsRequired = true)]
        [IntegerValidator(MinValue = 0
            , MaxValue = 100)]
        public int TableMinID
        {
            get => (int)this["TableMinID"];
            set
            {
                this["TableMinID"] = value;
                Configuration.Value.Save();
            }
        }

        [ConfigurationProperty("TableMaxID"
            , DefaultValue = 14
            , IsRequired = true)]
        [IntegerValidator(MinValue = 1
            , MaxValue = 100)]
        public int TableMaxID
        {
            get => (int)this["TableMaxID"];
            set
            {
                this["TableMaxID"] = value;
                Configuration.Value.Save();
            }
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
                    Configuration.Value.Save();
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
                        Configuration.Value.Save();
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
            set
            {
                this["DocumentsFolderName"] = value;
                Configuration.Value.Save();
            }
        }

        [ConfigurationProperty("DatabaseFolderName"
            , DefaultValue = "Database"
            , IsRequired = true)]
        public string DatabaseFolderName
        {
            get => (string)this["DatabaseFolderName"];
            set
            {
                this["DatabaseFolderName"] = value;
                Configuration.Value.Save();
            }
        }

        [ConfigurationProperty("DatabaseFileName"
            , DefaultValue = "Data.db"
            , IsRequired = true)]
        public string DatabaseFileName
        {
            get => (string)this["DatabaseFileName"];
            set
            {
                this["DatabaseFileName"] = value;
                Configuration.Value.Save();
            }
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
                Configuration.Value.Save();
            }
        }

        public string DatabaseFolderPath =>
            Path.Combine(DocumentsFolderPath, DatabaseFolderName);
        public string DatabaseFilePath => Path.Combine(DatabaseFolderPath, DatabaseFileName);

        [ConfigurationProperty("StartingCategories"
            , DefaultValue = "JUICE, BEER, WARM DRINKS, ALCOHOL, COMBINATIONS, FOOD"
            , IsRequired = true)]
        public string Categories
        {
            get => (string)this["StartingCategories"];
            set
            {
                this["StartingCategories"] = value;
                Configuration.Value.Save();
            }
        }

        public List<string> CategoryList => Categories.Split(',').Select(c => c.Trim(' ')).ToList();
    }
}