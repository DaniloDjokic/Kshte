using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Helpers;
using Dapper;

namespace WindowsFormsApp1.DBTools
{
    internal static class DBSeeder
    {
        #region SQL Creation queries
        private static readonly string CreateArticle = @"CREATE TABLE IF NOT EXISTS Article
                                                        (
                                                            ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                                            Name NVARCHAR(50) NOT NULL,
                                                            Price DOUBLE NOT NULL,
                                                            CategoryID INTEGER NOT NULL,
                                                            FOREIGN KEY(CategoryID) REFERENCES Category(ID)
                                                                ON UPDATE CASCADE
                                                                ON DELETE RESTRICT
                                                        );";
        private static readonly string CreateTable = @"CREATE TABLE IF NOT EXISTS [Table]
                                                        (
                                                            ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                                            CurrentTransactionID INTEGER,
                                                            FOREIGN KEY(CurrentTransactionID) REFERENCES [Transaction](ID)
                                                                ON UPDATE CASCADE
                                                                ON DELETE SET NULL
                                                        );";
        private static readonly string CreateTransaction = @"CREATE TABLE IF NOT EXISTS [Transaction]
                                                        (
                                                            ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                                            DateCreated TEXT NOT NULL,
                                                            DateCompleted TEXT,
                                                            [TableID] INTEGER NOT NULL,
                                                            FOREIGN KEY(TableID) REFERENCES [Table](ID)
                                                                ON UPDATE CASCADE
                                                                ON DELETE NO ACTION
                                                        );";
        private static readonly string CreateTransactionDetails = @"CREATE TABLE IF NOT EXISTS [TransactionDetail]
                                                        (
                                                            ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                                            PaidFor TINYINT NOT NULL,
                                                            EffectivePrice DOUBLE NOT NULL,
                                                            TransactionID INTEGER NOT NULL,
                                                            ArticleID INTEGER NOT NULL,
                                                            FOREIGN KEY(TransactionID) REFERENCES [Transaction](ID)
                                                                ON UPDATE CASCADE
                                                                ON DELETE RESTRICT,
                                                            FOREIGN KEY(ArticleID) REFERENCES Article(ID)
                                                                ON UPDATE CASCADE
                                                                ON DELETE RESTRICT
                                                        );";
        private static readonly string CreateCategory = @"CREATE TABLE IF NOT EXISTS [Category]
                                                        (
                                                            ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                                            Name NVARCHAR(50) NOT NULL
                                                        );";
        #endregion

        #region SQL Data Insert Queries
        private static readonly string InsertNewTable = @"INSERT OR IGNORE INTO [Table](ID, CurrentTransactionID)
                                                        VALUES({0}, NULL)
                                                        ;";

        private static readonly string InsertNewCategory = @"INSERT OR IGNORE INTO [Category](ID, Name)
                                                        VALUES({0},""{1}"")
                                                        ;";
        #endregion



        public static void InitializeDatabase(SQLiteConnection conn)
        {
            SeedTables(conn);
            SeedData(conn);
        }

        private static void InsertTables(SQLiteConnection conn,int minId, int maxId)
        {
            for (int i = 0; i <= 14; i++)
            {
                conn.Execute(string.Format(InsertNewTable, i));
            }
        }

        private static void InsertCategories(SQLiteConnection conn, List<string> categories)
        {
            for (int i = 0; i < categories.Count; i++)
            {
                string query = string.Format(InsertNewCategory, i+1, categories[i]);
                conn.Execute(query);
            }
        }

        private static void SeedData(SQLiteConnection conn)
        {
            if (conn == null)
            {
                throw new ArgumentNullException();
            }

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    InsertTables(conn, KshteSettings.Settings.TableMinID, KshteSettings.Settings.TableMaxID);
                    InsertCategories(conn, KshteSettings.Settings.CategoryList);
                    tran.Commit();
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    throw new Exception("Error seeding data.", e);
                }
            }
        }

        private static void SeedTables(SQLiteConnection conn)
        {
            if (conn == null)
            {
                throw new ArgumentNullException();
            }

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            conn.Execute("PRAGMA foreign_keys=off;");
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    conn.Execute(CreateCategory);
                    conn.Execute(CreateArticle);
                    conn.Execute(CreateTable);
                    conn.Execute(CreateTransaction);
                    conn.Execute(CreateTransactionDetails);
                    tran.Commit();
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    throw new Exception("Error creating tables in database.", e);
                }
            }
            conn.Execute("PRAGMA foreign_keys=on;");
        }
    }
}
