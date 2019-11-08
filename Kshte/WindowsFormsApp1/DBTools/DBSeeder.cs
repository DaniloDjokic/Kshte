using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static void Seed(SQLiteConnection conn)
        {
            if (conn == null)
            {
                throw new ArgumentNullException();
            }

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            conn.Execute(CreateCategory);
            conn.Execute(CreateArticle);
            conn.Execute("PRAGMA foreign_keys=off;");
            conn.Execute("BEGIN TRANSACTION;");
            conn.Execute(CreateTable);
            conn.Execute(CreateTransaction);
            conn.Execute("COMMIT;");
            conn.Execute("PRAGMA foreign_keys=on;");
            conn.Execute(CreateTransactionDetails);
        }
    }
}
