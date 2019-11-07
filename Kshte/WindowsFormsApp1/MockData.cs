using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    //public enum Category { JUICE, BEER, WARM_DRINKS, FOOD, ALCOHOL, MISC }

    public static class MockData
    {
        public static List<Transaction> activeTransactions = new List<Transaction>
        {
            new Transaction
            {
                TableID = 1,
                //CurrentValue = 340,
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
                //CurrentValue = 200,
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

        public static Dictionary<Category, List<Article>> allArticles;// = new Dictionary<Category, List<Article>>
//        {
//            {
//                Category.JUICE, new List<Article>
//                {
//                    new Article { Name = "Next", Price = 120, Category = Category.JUICE
//    },
//                    new Article {Name = "Koka kola", Price = 100, Category = Category.JUICE
//}
//                }
//            },
//            {
//                Category.BEER, new List<Article>
//                {
//                    new Article { Name = "Lasko 0.5l", Price = 160, Category = Category.BEER },
//                    new Article {Name = "Nisko 0.3l", Price = 100, Category = Category.BEER}
//                }
//            },
//            {
//                Category.WARM_DRINKS, new List<Article>
//                {
//                    new Article {Name = "Turska kafa", Price = 80, Category = Category.WARM_DRINKS},
//                    new Article { Name = "Espreso", Price = 100, Category = Category.WARM_DRINKS}
//                }
//            }
//        };
    }
}
