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
            var commute = new Category() { Name = "Commute", Plan = 650 };
            db.Insert(commute);
            var rent = new Category() { Name = "Rent", Plan = 1600 };
            db.Insert(rent);
            var insurance = new Category() { Name = "Insurance", Plan = 250 };
            db.Insert(insurance);
            var groceries = new Category() { Name = "Groceries", Plan = 650 };
            db.Insert(groceries);
            var games = new Category() { Name = "Games", Plan = 120 };
            db.Insert(games);
            var clothes = new Category() { Name = "Clothes", Plan = 200 };
            db.Insert(clothes);
			var eatingOut = new Category() { Name = "Eating Out", Plan = 250 };
			db.Insert(eatingOut);
			var bills = new Category() { Name = "Bills", Plan = 500 };
            db.Insert(bills);
			var toys = new Category() { Name = "Toys For Kids", Plan = 70 };
            db.Insert(toys);
			var liquor = new Category() { Name = "Liquor", Plan = 90 };
			db.Insert(liquor);
			var saving = new Category() { Name = "Saving", Plan = 600 };
            db.Insert(saving);

            var random = new Random();
            var now = DateTime.Now;

            db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
            db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 34, Description = "Taking taxi to work" });
            db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" }); 
            db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 35, Description = "Taking taxi to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 40, Description = "Taking taxi to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });
			db.Insert(new Expense() { CategoryId = commute.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Taking bus to work" });


            db.Insert(new Expense() { CategoryId = rent.Id, ExpenseDate = GetRandomDate(), Value = 1600, Description = "Paying rent" });

            db.Insert(new Expense() { CategoryId = insurance.Id, ExpenseDate = GetRandomDate(), Value = 120, Description = "Medical insurance" });
            db.Insert(new Expense() { CategoryId = insurance.Id, ExpenseDate = GetRandomDate(), Value = 80, Description = "Car insurance" });

            db.Insert(new Expense() { CategoryId = groceries.Id, ExpenseDate = GetRandomDate(), Value = 32, Description = "Buying groceries" });
            db.Insert(new Expense() { CategoryId = groceries.Id, ExpenseDate = GetRandomDate(), Value = 80, Description = "Buying some snacks for the party" });
            db.Insert(new Expense() { CategoryId = groceries.Id, ExpenseDate = GetRandomDate(), Value = 75, Description = "Buying groceries" });
            db.Insert(new Expense() { CategoryId = groceries.Id, ExpenseDate = GetRandomDate(), Value = 22, Description = "Buying groceries" });
            db.Insert(new Expense() { CategoryId = groceries.Id, ExpenseDate = GetRandomDate(), Value = 26, Description = "Milk, Bread, Eggs" });
            db.Insert(new Expense() { CategoryId = groceries.Id, ExpenseDate = GetRandomDate(), Value = 90, Description = "Buying groceries" });

            db.Insert(new Expense() { CategoryId = games.Id, ExpenseDate = GetRandomDate(), Value = 69, Description = "Destiny 2" });
            db.Insert(new Expense() { CategoryId = games.Id, ExpenseDate = GetRandomDate(), Value = 39, Description = "Diablo 3" });

            db.Insert(new Expense() { CategoryId = clothes.Id, ExpenseDate = GetRandomDate(), Value = 10, Description = "New sweater" });
            db.Insert(new Expense() { CategoryId = clothes.Id, ExpenseDate = GetRandomDate(), Value = 25, Description = "New shoes" });
            db.Insert(new Expense() { CategoryId = clothes.Id, ExpenseDate = GetRandomDate(), Value = 90, Description = "New coat" });
            db.Insert(new Expense() { CategoryId = clothes.Id, ExpenseDate = GetRandomDate(), Value = 39, Description = "Some clothes" });

            db.Insert(new Expense() { CategoryId = eatingOut.Id, ExpenseDate = GetRandomDate(), Value = 19, Description = "Mcdonalds" });
            db.Insert(new Expense() { CategoryId = eatingOut.Id, ExpenseDate = GetRandomDate(), Value = 12, Description = "Friday night" });
            db.Insert(new Expense() { CategoryId = eatingOut.Id, ExpenseDate = GetRandomDate(), Value = 45, Description = "Going out with friends" });
            db.Insert(new Expense() { CategoryId = eatingOut.Id, ExpenseDate = GetRandomDate(), Value = 19, Description = "Lunch at Charlie's" });
            db.Insert(new Expense() { CategoryId = eatingOut.Id, ExpenseDate = GetRandomDate(), Value = 21, Description = "Eating out" });
            db.Insert(new Expense() { CategoryId = eatingOut.Id, ExpenseDate = GetRandomDate(), Value = 16, Description = "Mcdonalds" });
            db.Insert(new Expense() { CategoryId = eatingOut.Id, ExpenseDate = GetRandomDate(), Value = 32, Description = "Breakfast at airport" });

            db.Insert(new Expense() { CategoryId = bills.Id, ExpenseDate = GetRandomDate(), Value = 190, Description = "Electricity bill" });
            db.Insert(new Expense() { CategoryId = bills.Id, ExpenseDate = GetRandomDate(), Value = 153, Description = "Gas bill" });
            db.Insert(new Expense() { CategoryId = bills.Id, ExpenseDate = GetRandomDate(), Value = 65, Description = "Water bill" });

            db.Insert(new Expense() { CategoryId = toys.Id, ExpenseDate = GetRandomDate(), Value = 39, Description = "New lego set" });
            db.Insert(new Expense() { CategoryId = toys.Id, ExpenseDate = GetRandomDate(), Value = 110, Description = "New bike" });
            db.Insert(new Expense() { CategoryId = toys.Id, ExpenseDate = GetRandomDate(), Value = 12, Description = "New toy car" });

            db.Insert(new Expense() { CategoryId = liquor.Id, ExpenseDate = GetRandomDate(), Value = 12, Description = "Wine" });
            db.Insert(new Expense() { CategoryId = liquor.Id, ExpenseDate = GetRandomDate(), Value = 20, Description = "Wine" });
            db.Insert(new Expense() { CategoryId = liquor.Id, ExpenseDate = GetRandomDate(), Value = 25, Description = "Six pack beer" });
            db.Insert(new Expense() { CategoryId = liquor.Id, ExpenseDate = GetRandomDate(), Value = 11, Description = "Wine" });
            db.Insert(new Expense() { CategoryId = liquor.Id, ExpenseDate = GetRandomDate(), Value = 16, Description = "Wine" });

            db.Insert(new Expense() { CategoryId = saving.Id, ExpenseDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 7), Value = 120, Description = "Week one saving" });
            db.Insert(new Expense() { CategoryId = saving.Id, ExpenseDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 12), Value = 130, Description = "Week two saving" });
            db.Insert(new Expense() { CategoryId = saving.Id, ExpenseDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 20), Value = 120, Description = "Week three saving" });
            db.Insert(new Expense() { CategoryId = saving.Id, ExpenseDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 27), Value = 150, Description = "Week four saving" });
        }

        DateTime GetRandomDate()
        {
            var random = new Random();
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, random.Next(1,28));
        }
    }
}
