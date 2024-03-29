﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;
using Dapper;

namespace WindowsFormsApp1.DBTools
{
    internal static class DBContext
    {
        internal static List<Category> GetExistingCategories()
        {
            string sqlQuery = "SELECT * FROM Category;";

            List<Category> result;
            try
            {
                result = DBConnector.Connection.Query<Category>(sqlQuery).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error fecthing existing categories.", e);
            }

            return result;
        }
        internal static int AddNewCategory(Category category)
        {
            string sqlQuery = "INSERT INTO Category(Name) VALUES (@Name); SELECT last_insert_rowid();";

            int newId;
            try
            {
                newId = int.Parse(DBConnector.Connection.ExecuteScalar(sqlQuery, new { category.Name }).ToString());
                category.ID = newId;
            }
            catch (Exception e)
            {
                throw new Exception("Error inserting a new category.", e);
            }
            return newId;
        }
        internal static List<Article> GetExistingArticles()
        {
            string sqlQuery = "SELECT * FROM [Article];";

            List<Article> result;
            try
            {
                result = DBConnector.Connection.Query<Article>(sqlQuery).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error fecthing existing categories.", e);
            }

            return result;
        }
        internal static List<Table> GetExistingTables()
        {
            string sqlQuery = "SELECT * FROM [Table];";

            List<Table> result;
            try
            {
                result = DBConnector.Connection.Query<Table>(sqlQuery).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error fecthing existing categories.", e);
            }

            return result;
        }
        internal static IEnumerable<Transaction> GetActiveTransactions()
        {
            string sqlQuery = "SELECT * FROM [Transaction] WHERE DateCompleted IS NULL;";
            string detailSqlQuery = "SELECT * FROM [TransactionDetail] WHERE TransactionID = @ID;";

            IEnumerable<Transaction> transactions = null;
            try
            {
                transactions = DBConnector.Connection.Query<Transaction>(sqlQuery);

                IEnumerable<TransactionDetail> details = null;
                foreach (var transaction in transactions)
                {
                    details = DBConnector.Connection.Query<TransactionDetail>(detailSqlQuery, new { transaction.ID });
                    transaction.ForceSetTransactionDetails(details);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error fetching active transactions.", e);
            }

            return transactions;
        }
        internal static IEnumerable<Transaction> GetAllTransactions()
        {
            string sqlQuery = "SELECT * FROM [Transaction];";
            string detailSqlQuery = "SELECT * FROM [TransactionDetail] WHERE TransactionID = @ID;";

            List<Transaction> transactions = null;
            try
            {
                transactions = DBConnector.Connection.Query<Transaction>(sqlQuery).ToList();

                IEnumerable<TransactionDetail> details = null;
                foreach (var transaction in transactions)
                {
                    details = DBConnector.Connection.Query<TransactionDetail>(detailSqlQuery, new { transaction.ID });
                    transaction.ForceSetTransactionDetails(details);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error fetching active transactions.", e);
            }

            return transactions;
        }
        internal static int AddNewArticle(Article article)
        {
            string sqlQuery = "INSERT INTO Article(Name,Price,CategoryID) VALUES (@name,@price,@catId); SELECT last_insert_rowid();";

            int newId;
            try
            {
                string name = article.Name;
                decimal price = article.Price;
                int catId = article.Category.ID;

                newId = int.Parse(DBConnector.Connection.ExecuteScalar(sqlQuery, new { name, price, catId }).ToString());
                article.ID = newId;
            }
            catch (Exception e)
            {
                throw new Exception("Error inserting a new article.", e);
            }
            return newId;
        }
        internal static int AddNewTable(Table table)
        {
            string sqlQuery = "INSERT INTO [Table](CurrentTransactionID) VALUES (@CurrentTransactionID); SELECT last_insert_rowid();";

            int newId;
            try
            {
                newId = int.Parse(DBConnector.Connection.ExecuteScalar(sqlQuery, new { table.CurrentTransactionID }).ToString());
                table.ID = newId;
            }
            catch (Exception e)
            {
                throw new Exception("Error inserting a new table.", e);
            }
            return newId;
        }
        internal static int AddNewTransaction(Transaction transaction)
        {
            string sqlQuery = "INSERT INTO [Transaction](DateCreated,DateCompleted,TableID) VALUES (@dateCreated,@dateCompleted,@TableID); SELECT last_insert_rowid();";
            string detailSqlQuery = "INSERT INTO [TransactionDetail](PaidFor, EffectivePrice, TransactionID, ArticleID) VALUES (@paidFor, @effectivePrice, @transactionId, @articleId); SELECT last_insert_rowid();";

            int newId;
            int detailId;
            using (var tran = DBConnector.Connection.BeginTransaction())
            {
                try
                {
                    string dateCreated = transaction.DateCreated.ToString();
                    string dateCompleted = transaction.DateCompleted != null ? transaction.DateCompleted.ToString() : null;
                    newId = int.Parse(DBConnector.Connection.ExecuteScalar(sqlQuery, new { dateCreated, dateCompleted, transaction.TableID }).ToString());
                    transaction.ID = newId;

                    try
                    {
                        foreach (var detail in transaction.TransactionDetails)
                        {
                            int paidFor = detail.PaidFor ? 1 : 0;
                            decimal effectivePrice = detail.EffectivePrice;
                            int transactionId = transaction.ID;
                            int articleId = detail.Article.ID;
                            detailId = int.Parse(DBConnector.Connection.ExecuteScalar(detailSqlQuery, new { paidFor, effectivePrice, transactionId, articleId }).ToString());
                            detail.ID = detailId;
                        }
                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Error inserting a transaction detail.", e);
                    }
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    throw new Exception("Error inserting a new transaction.", e);
                }
            }
            return newId;
        }
        internal static void UpdateDB(Article article)
        {
            string sqlQuery = "UPDATE Article SET Name=@Name, Price=@Price, CategoryID=@categoryID WHERE ID=@ID;";

            try
            {
                int categoryID = article.Category.ID;
                DBConnector.Connection.Execute(sqlQuery, new { article.Name, article.Price, categoryID, article.ID });
            }
            catch (Exception e)
            {
                throw new Exception("Error updating article.", e);
            }
        }
        internal static void UpdateDB(Table table)
        {
            string sqlQuery = "UPDATE [Table] SET CurrentTransactionID=@CurrentTransactionID WHERE ID=@ID;";

            try
            {
                DBConnector.Connection.Execute(sqlQuery, new { table.CurrentTransactionID, table.ID });
            }
            catch (Exception e)
            {
                throw new Exception("Error updating table.", e);
            }
        }
        internal static void UpdateDB(Category category)
        {
            string sqlQuery = "UPDATE [Category] SET Name=@Name WHERE ID=@ID;";

            try
            {
                DBConnector.Connection.Execute(sqlQuery, new { category.Name, category.ID });
            }
            catch (Exception e)
            {
                throw new Exception("Error updating category.", e);
            }
        }
        internal static void UpdateDB(Transaction transaction)
        {
            //update transaction table, ako prodje idemo dalje

            //idemo kroz svaki transactiondetail
            //ako postoji u bazi, updajtujemo ga
            //ako ne postoji u bazi, dodajemo ga
            //ako postoji u bazi a ne postoji lokalno, brisemo ga

            
            string sqlQuery = "UPDATE [Transaction] SET DateCreated=@DateCreated, DateCompleted=@DateCompleted, [TableID]=@TableID WHERE ID=@ID;";
            string getDBDetails = "SELECT * FROM [TransactionDetail] WHERE TransactionID=@ID;";
            string updateDetail = "UPDATE [TransactionDetail] SET PaidFor=@PaidFor, EffectivePrice=@EffectivePrice WHERE ID=@ID;";
            string insertDetail = "INSERT INTO [TransactionDetail](PaidFor,EffectivePrice,TransactionID,ArticleID) VALUES (@PaidFor,@EffectivePrice,@ID,@ArticleID); SELECT last_insert_rowid();";
            string removeDetail = "DELETE FROM [TransactionDetail] WHERE ID=@ID;";

            try
            {
                DBConnector.Connection.ExecuteScalar(sqlQuery, new { transaction.DateCreated, transaction.DateCompleted, transaction.TableID, transaction.ID });
            }
            catch (Exception e)
            {
                throw new Exception("Error updating transaction row.", e);
            }

            try
            {
                var dbDetails = DBConnector.Connection.Query<TransactionDetail>(getDBDetails, new { transaction.ID }).ToList();

                TransactionDetail foundDetail;
                foreach (var detail in transaction.TransactionDetails)
                {
                    foundDetail = null;

                    for (int i = dbDetails.Count - 1; i >= 0; i--)
                    {
                        if (dbDetails[i].ID == detail.ID)
                        {
                            foundDetail = dbDetails[i];
                            dbDetails.RemoveAt(i);
                            break;
                        }
                    }

                    if (foundDetail != null)
                    {
                        //postoji u bazi
                        DBConnector.Connection.Execute(updateDetail, new { detail.PaidFor, detail.EffectivePrice, detail.ID });
                    }
                    else
                    {
                        //ne postoji u bazi
                        int newId = int.Parse(DBConnector.Connection.ExecuteScalar(insertDetail, new { detail.PaidFor, detail.EffectivePrice, transaction.ID, detail.ArticleID}).ToString());
                        detail.ID = newId;
                    }
                }

                foreach (var detailToRemove in dbDetails)
                {
                    //ovi postoje u bazi ali ne postoje lokalno, u ovom slucaju ih brisemo sa lokala, sobzirom da nijedna druga lokacija ne moze dodavati detalje
                    DBConnector.Connection.Execute(removeDetail, new { detailToRemove.ID });
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error updating transaction details.", e);
            }
        }
        internal static void RemoveArticle(Article article)
        {
            string sqlQuery = "DELETE FROM [Article] WHERE ID=@ID;";

            try
            {
                DBConnector.Connection.Execute(sqlQuery, new { article.ID });
            }
            catch (Exception e)
            {
                throw new Exception("Error removing article.", e);
            }
        }

        internal static int RemoveTransaction(Transaction transaction)
        {
            string deleteDetails = "DELETE FROM [TransactionDetail] WHERE TransactionID=@ID;"; 
            string sqlQuery = "DELETE FROM [Transaction] WHERE ID=@ID AND DateCompleted IS NULL; SELECT changes();";

            int removedRowCount = 0;
            using (var tran = DBConnector.Connection.BeginTransaction())
            {
                try
                {
                    DBConnector.Connection.Execute(deleteDetails, new { transaction.ID });
                    removedRowCount = int.Parse(DBConnector.Connection.ExecuteScalar(sqlQuery, new { transaction.ID }).ToString());
                    tran.Commit();
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    throw new Exception("Error removing transaction. Changes rollbacked.", e);
                }
            }
            return removedRowCount;
        }
        internal static int RemoveExportedTransactions(IEnumerable<TransactionView> transactionViews)
        {
            int removedRowCount = 0;
            using (var tran = DBConnector.Connection.BeginTransaction())
            {
                try
                {
                    int[] ids = transactionViews.Select(tv => tv.ID).ToArray();
                    string idsString = string.Join(", ", ids);

                    string deleteDetails = $"DELETE FROM [TransactionDetail] WHERE TransactionID IN ({idsString});";
                    string deleteTransaction = $"DELETE FROM [Transaction] WHERE DateCompleted IS NOT NULL AND ID IN ({idsString}); SELECT changes();";

                    DBConnector.Connection.Execute(deleteDetails);
                    removedRowCount = int.Parse(DBConnector.Connection.ExecuteScalar(deleteTransaction).ToString());

                    if (removedRowCount == transactionViews.Count())
                    {
                        tran.Commit();
                    }
                    else
                    {
                        throw new Exception("Not all transactions have been removed.");
                    }
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    throw new Exception("Error removing transactions. Changes rollbacked.", e);
                }
            }

            return removedRowCount;
        }

    }
}
