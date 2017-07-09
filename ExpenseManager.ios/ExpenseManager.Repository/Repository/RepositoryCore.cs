using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManage.Common;
using SQLite.Net;
using SQLite.Net.Interop;

namespace ExpenseManager.Repository.Repository
{
    public static class DBConnectionString
    {
        public static string DBPATH { get; set; }
        public static ISQLitePlatform PLATFORM { get; set; }
    }

    public class RepositoryCore
    {

        public static IExpenseManagerLog Logger;
        public RepositoryCore(IExpenseManagerLog logger)
        {
            Logger = logger;
        }

        public SQLiteConnection CreateDataBase(string dbPath, ISQLitePlatform sqlitePlatform)
        {
            DBConnectionString.DBPATH = dbPath;
            DBConnectionString.PLATFORM = sqlitePlatform;
            Logger.Log(nameof(RepositoryCore), "Before creating db.");
            var db = new SQLiteConnection(sqlitePlatform, dbPath);
            db.CreateTable<Category>();
            db.CreateTable<Expense>();
            Logger.Log(nameof(RepositoryCore), "Before seeding db.");
            Seed(db);
            Logger.Log(nameof(RepositoryCore), "Seeding db done.");
            return db;
        }

        public void SetUpDataBaseConnection(string dbPath, ISQLitePlatform sqlitePlatform)
        {
            Logger.Log(nameof(RepositoryCore), "SetUp db connection.");
			DBConnectionString.DBPATH = dbPath;
			DBConnectionString.PLATFORM = sqlitePlatform;
        }

        public List<Expense> GetExpenses()
        {
            Logger.Log(nameof(RepositoryCore), "Get Expenses.");
			using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
			{
                var expenses = db.Table<Expense>().ToList();

                Logger.Log(nameof(RepositoryCore), "Get Expenses done.");
                return expenses;
			}
        }

        public List<Category> GetCategories()
        {
            Logger.Log(nameof(RepositoryCore), "Get categories.");
			using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
			{
                var categories = db.Table<Category>().ToList();

                Logger.Log(nameof(RepositoryCore), "Get categories done.");
                return categories;
			}
        }

        public List<Category> GetTopCategories()
        {
            Logger.Log(nameof(RepositoryCore), "Get top categories.");
			using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
			{
                var categories = db.Table<Category>().ToList();
                var topCategories = categories.OrderByDescending(c => c.GetExpenses().Sum(e=> e.Value))
                                              .Take(5)
                                              .ToList();

                Logger.Log(nameof(RepositoryCore), "Get top categories.");
                return topCategories;
			}
        }

        void Seed(SQLiteConnection db)
        {
			var commute = new Category() { Name = "Commute", Plan = 200 };
			db.Insert(commute);
			var rent = new Category() { Name = "Rent", Plan = 1200 };
			db.Insert(rent);
			var insurance = new Category() { Name = "Insurance", Plan = 450 };
			db.Insert(insurance);
			var groceries = new Category() { Name = "Groceries", Plan = 650 };
			db.Insert(groceries);
			var games = new Category() { Name = "Games", Plan = 80 };
			db.Insert(games);
			var clothes = new Category() { Name = "Clothes", Plan = 120 };
			db.Insert(clothes);

			db.Insert(new Expense() { CategoryId = commute.Id, Value = 50, Description = "Some description goes here." });
			db.Insert(new Expense() { CategoryId = commute.Id, Value = 50, Description = "Some description goes here." });
			db.Insert(new Expense() { CategoryId = commute.Id, Value = 50, Description = "Some description goes here." });

			db.Insert(new Expense() { CategoryId = rent.Id, Value = 1600, Description = "Some description goes here." });

			db.Insert(new Expense() { CategoryId = insurance.Id, Value = 250, Description = "Some description goes here." });
			db.Insert(new Expense() { CategoryId = insurance.Id, Value = 300, Description = "Some description goes here." });

			db.Insert(new Expense() { CategoryId = groceries.Id, Value = 120, Description = "Some description goes here." });
			db.Insert(new Expense() { CategoryId = groceries.Id, Value = 60, Description = "Some description goes here." });
			db.Insert(new Expense() { CategoryId = groceries.Id, Value = 110, Description = "Some description goes here." });
			db.Insert(new Expense() { CategoryId = groceries.Id, Value = 22, Description = "Some description goes here." });
			db.Insert(new Expense() { CategoryId = groceries.Id, Value = 26, Description = "Some description goes here." });
			db.Insert(new Expense() { CategoryId = groceries.Id, Value = 90, Description = "Some description goes here." });

			db.Insert(new Expense() { CategoryId = games.Id, Value = 89, Description = "Some description goes here." });
			db.Insert(new Expense() { CategoryId = games.Id, Value = 19, Description = "Some description goes here." });

			db.Insert(new Expense() { CategoryId = clothes.Id, Value = 10, Description = "Some description goes here." });
			db.Insert(new Expense() { CategoryId = clothes.Id, Value = 25, Description = "Some description goes here." });
			db.Insert(new Expense() { CategoryId = clothes.Id, Value = 9, Description = "Some description goes here." });
			db.Insert(new Expense() { CategoryId = clothes.Id, Value = 120, Description = "Some description goes here." });
			db.Insert(new Expense() { CategoryId = clothes.Id, Value = 100, Description = "Some description goes here." });
        }
    }
}
