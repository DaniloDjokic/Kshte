using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace WindowsFormsApp1.DBTools
{
    internal static class DBConnector
    {
        public static readonly string DBDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Kste\\Database\\");
        public static readonly string DBPath = Path.Combine(DBDirectory, "Data.db");

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

        private static void StartAndOpenDB()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }

            if (!File.Exists(DBPath))
            {
                Directory.CreateDirectory(DBDirectory);
                SQLiteConnection.CreateFile(DBPath);
            }

            connection = new SQLiteConnection($"DataSource={DBPath};Version=3;");
            connection.Open();
        }
    }
}
