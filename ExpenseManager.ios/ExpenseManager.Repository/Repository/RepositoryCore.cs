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

        public static int CurrentYear { get; internal set; } = DateTime.Now.Year;
        public static int CurrentMonth { get; internal set; } = DateTime.Now.Month;
        public static void SetCurrentMonth(int year, int month)
        {
            CurrentMonth = month;
            CurrentYear = year;
        }

        public SQLiteConnection CreateDataBase(string dbPath, ISQLitePlatform sqlitePlatform)
        {
            try
            {
                DBConnectionString.DBPATH = dbPath;
                DBConnectionString.PLATFORM = sqlitePlatform;
                Logger.Log(nameof(RepositoryCore), "Before creating db.");
                var db = new SQLiteConnection(sqlitePlatform, dbPath);
                db.CreateTable<Category>();
                db.CreateTable<Expense>();
                db.CreateTable<Setting>();
                Logger.Log(nameof(RepositoryCore), "Before seeding db.");
                Seed(db);
                Logger.Log(nameof(RepositoryCore), "Seeding db done.");
                return db;
            }
            catch (Exception ex)
            {
                Logger.Log(nameof(RepositoryCore.CreateDataBase), ex.Message, LogType.Exception);
                throw ex;
            }
        }

        public void SetUpDataBaseConnection(string dbPath, ISQLitePlatform sqlitePlatform)
        {
            try
            {
                Logger.Log(nameof(RepositoryCore), "SetUp db connection.");
                DBConnectionString.DBPATH = dbPath;
                DBConnectionString.PLATFORM = sqlitePlatform;
                var settings = GetAppSettings();
                SetCurrentMonth(settings.CurrentYear, settings.CurrentMonth);
            }
            catch (Exception ex)
            {
                Logger.Log(nameof(RepositoryCore.SetUpDataBaseConnection), ex.Message, LogType.Exception);
                throw ex;
            }
        }

        public List<Expense> GetExpenses()
        {
            try
            {
                Logger.Log(nameof(RepositoryCore), "Get Expenses.");
                using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
                {
                    var startDate = new DateTime(CurrentYear, CurrentMonth, 1).ToUniversalTime();
					var endDate = startDate.AddMonths(1).ToUniversalTime();

                    var expenses = db.Table<Expense>()
									 .Where(e => e.ExpenseDate >= startDate &&
											     e.ExpenseDate < endDate)
                                     .ToList();

                    Logger.Log(nameof(RepositoryCore), "Get Expenses done.");
                    return expenses;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(nameof(RepositoryCore.GetExpenses), ex.Message, LogType.Exception);
                throw ex;
            }
        }

        public List<Category> GetCategories()
        {
            try
            {
                Logger.Log(nameof(RepositoryCore), "Get categories.");
                using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
                {
                    var categories = db.Table<Category>().ToList();

                    Logger.Log(nameof(RepositoryCore), "Get categories done.");
                    return categories;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(nameof(RepositoryCore.GetCategories), ex.Message, LogType.Exception);
                throw ex;
            }
        }

        public List<Category> GetTopCategories()
        {
            try
            {
                Logger.Log(nameof(RepositoryCore), "Get top categories.");
                using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
                {
                    var categories = db.Table<Category>().ToList();
                    var topCategories = categories.OrderByDescending(c => c.GetExpenses().Sum(e => e.Value))
                                                  .Take(5)
                                                  .ToList();

                    Logger.Log(nameof(RepositoryCore), "Get top categories.");
                    return topCategories;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(nameof(RepositoryCore.GetTopCategories), ex.Message, LogType.Exception);
                throw ex;
            }
        }

        public Setting GetAppSettings()
        {
            try
            {
                Logger.Log(nameof(RepositoryCore.GetAppSettings));
                using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
                {
                    var settings = db.Table<Setting>().FirstOrDefault();
                    if (settings == null)
                        return new Setting();

                    Logger.Log(nameof(RepositoryCore.GetAppSettings), settings.ToString());
                    return settings;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(nameof(RepositoryCore.GetAppSettings), ex.Message, LogType.Exception);
                throw ex;
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

            var random = new Random();

            db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 50, Description = "Some description goes here." });
            db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 50, Description = "Some description goes here." });
            db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 50, Description = "Some description goes here." });

            db.Insert(new Expense() { CategoryId = rent.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 1600, Description = "Some description goes here." });

            db.Insert(new Expense() { CategoryId = insurance.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 250, Description = "Some description goes here." });
            db.Insert(new Expense() { CategoryId = insurance.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 300, Description = "Some description goes here." });

            db.Insert(new Expense() { CategoryId = groceries.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 120, Description = "Some description goes here." });
            db.Insert(new Expense() { CategoryId = groceries.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 60, Description = "Some description goes here." });
            db.Insert(new Expense() { CategoryId = groceries.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 110, Description = "Some description goes here." });
            db.Insert(new Expense() { CategoryId = groceries.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 22, Description = "Some description goes here." });
            db.Insert(new Expense() { CategoryId = groceries.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 26, Description = "Some description goes here." });
            db.Insert(new Expense() { CategoryId = groceries.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 90, Description = "Some description goes here." });

            db.Insert(new Expense() { CategoryId = games.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 89, Description = "Some description goes here." });
            db.Insert(new Expense() { CategoryId = games.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 19, Description = "Some description goes here." });

            db.Insert(new Expense() { CategoryId = clothes.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 10, Description = "Some description goes here." });
            db.Insert(new Expense() { CategoryId = clothes.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 25, Description = "Some description goes here." });
            db.Insert(new Expense() { CategoryId = clothes.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 9, Description = "Some description goes here." });
            db.Insert(new Expense() { CategoryId = clothes.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 120, Description = "Some description goes here." });
            db.Insert(new Expense() { CategoryId = clothes.Id, ExpenseDate = DateTime.Now.AddDays(random.Next(-45, 0)), Value = 100, Description = "Some description goes here." });
        }
    }
}
