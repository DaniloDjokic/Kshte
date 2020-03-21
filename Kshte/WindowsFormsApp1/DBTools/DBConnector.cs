using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using Kshte.Helpers;

namespace Kshte.DBTools
{
    internal static class DBConnector
    {
        public static readonly string DBDirectory = KshteSettings.Settings.DatabaseFolderPath;
        public static readonly string DBPath = KshteSettings.Settings.DatabaseFilePath;

        private static SQLiteConnection connection = null;

        public static SQLiteConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    StartAndOpenDB();
                }

                return connection;
            }
        }

        public static void StartAndOpenDB(bool forceCreateNew = false)
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }

            if (!CheckForDatabase() || forceCreateNew)
            {
                Directory.CreateDirectory(DBDirectory);

                if (File.Exists(DBPath))
                {
                    File.Delete(DBPath);
                }

                SQLiteConnection.CreateFile(DBPath);
            }

            connection = new SQLiteConnection(KshteSettings.Settings.ConnectionString);
            connection.Open();
        }

        public static bool CheckForDatabase()
        {
            try
            {
                if (!File.Exists(DBPath))
                {
                    return false;
                }
                else
                {
                    using (var conn = new SQLiteConnection(KshteSettings.Settings.ConnectionString))
                    {
                        conn.Open();

                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                //log
                return false;
            }
        }
    }
}
