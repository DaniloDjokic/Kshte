using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public static class MockData
    {
        public static List<Transaction> activeTransactions = new List<Transaction>
        {
            new Transaction
            {
                TableID = 1,
                CurrentValue = 340,
                Articles = new List<Article>
                {
                    new Article
                    {
                        Name = "Kafa",
                        Price = 100
                    },
                    new Article
                    {
                        Name ="Lasko 0.5",
                        Price = 160
                    }
                }
            },
            new Transaction 
            { 
                TableID = 11, 
                CurrentValue = 200,
                Articles = new List<Article>
                {
                    new Article
                    {
                        Name = "Koka kola",
                        Price = 120
                    }
                }
            }
        };
    }
}
