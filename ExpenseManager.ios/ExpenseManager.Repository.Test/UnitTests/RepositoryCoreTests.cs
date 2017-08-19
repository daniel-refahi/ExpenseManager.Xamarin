using System;
using System.IO;
using System.Linq;
using ExpenseManager.Repository.Repository;
using ExpenseManager.Repository.Test.Services;
using NUnit.Framework;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;

namespace ExpenseManager.Repository.Test.UnitTests
{
    [TestFixture]
    public class RepositoryCoreTests
    {
        string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "expensemanager.db3");

		public void DbSetup()
		{
			if (File.Exists(_dbPath))
				File.Delete(_dbPath);

			var db = new SQLiteConnection(new SQLitePlatformIOS(), _dbPath);
			db.CreateTable<Category>();
			db.CreateTable<Expense>();
			db.CreateTable<Setting>();

			RepositoryCore.SetCurrentMonth(DateTime.Now.Year, DateTime.Now.Month);
		}

        [Test]
        public void CreateDataBase_IfDbDoesNotExist_CheckDb()
        {
            if (File.Exists(_dbPath))
                File.Delete(_dbPath);
            
            var core = new RepositoryCore(new LogService());
            core.CreateDataBase(_dbPath, new SQLitePlatformIOS(), true);

            Assert.IsTrue(File.Exists(_dbPath));
        }

		[Test]  
		public void CreateDataBase_IfDbDoesNotExist_CheckCategoryTable()
		{
			if (File.Exists(_dbPath))
				File.Delete(_dbPath);

			var core = new RepositoryCore(new LogService());
            core.CreateDataBase(_dbPath, new SQLitePlatformIOS(),true);

            var db = new SQLiteConnection(new SQLitePlatformIOS(), _dbPath);
            var categories = db.Table<Category>();

            Assert.IsTrue(categories.Count() > 0, "Category table hasn't been created.");
		}

        [Test]
        public void GetExpenses_WhenExpensesExist()
        {
            DbSetup();
            var db = new SQLiteConnection(new SQLitePlatformIOS(), _dbPath);
			var repositoryCore = new RepositoryCore(new LogService());
			var category = new Category();
			category.Name = "test";
			category.Plan = 45;
			category.Upsert();

            var beginingOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 1, 1, 1);
			db.Insert(new Expense() { CategoryId = category.Id, Value = 230, Description="for unit test"});
            db.Insert(new Expense() { CategoryId = category.Id, Value = 230, ExpenseDate = DateTime.Now.AddMonths(-1) });
            db.Insert(new Expense() { CategoryId = category.Id, Value = 230, ExpenseDate = beginingOfMonth });
            db.Insert(new Expense() { CategoryId = category.Id, Value = 230, ExpenseDate = beginingOfMonth.AddHours(-2) });

			var expenses = repositoryCore.GetExpenses();
            Assert.IsNotNull(expenses);
            Assert.AreEqual(2, expenses.Count);
            Assert.AreEqual(230, expenses.Where(e => e.Description == "for unit test").First().Value);
        }

		[Test]
		public void GetExpenses_WhenNoExpensesExist()
		{
			DbSetup();
			var repositoryCore = new RepositoryCore(new LogService());

            var expeneses = repositoryCore.GetExpenses();
			Assert.IsNotNull(expeneses);
            Assert.AreEqual(0, expeneses.Count);
		}

		[Test]
		public void GetCategories_WhenCategoriesExist()
		{
			DbSetup();
			var repositoryCore = new RepositoryCore(new LogService());
            var category = new Category();
            category.Name = "test";
            category.Plan = 45;
            category.Upsert();

			var category1 = new Category();
            category1.Name = "test1";
			category1.Plan = 45;
			category1.Upsert();

            var categories = repositoryCore.GetCategories();
			Assert.IsNotNull(categories);
            Assert.IsNotNull(categories.FirstOrDefault(c=> c.Name == category.Name));
            Assert.AreEqual(2, categories.Count);
		}

		[Test]
		public void GetCategories_WhenNoCategoryExist()
		{
			DbSetup();
			var repositoryCore = new RepositoryCore(new LogService());

            var categories = repositoryCore.GetCategories();
			Assert.IsNotNull(categories);
			Assert.AreEqual(0, categories.Count);
		}

		[Test]
		public void GetTopCategories_WhenNoCategoryExist()
		{
			DbSetup();
			var repositoryCore = new RepositoryCore(new LogService());

			var categories = repositoryCore.GetTopCategories();
			Assert.IsNotNull(categories);
			Assert.AreEqual(0, categories.Count);
		}

		[Test]
		public void GetTopCategories_WhenCategoriesExist()
		{
			DbSetup();
			var repositoryCore = new RepositoryCore(new LogService());

            for (int i = 0; i < 10; i++)
            {
                var c = new Category();
                c.Name = i.ToString();
                c.Plan = i;
                c.Upsert();

                // adding expenses
                for (int j = 0; j < i; j++)
                {
                    var e = new Expense();
                    e.CategoryId = c.Id;
                    e.Value = 10;
                    e.Upsert();
                }
            }

			var categories = repositoryCore.GetTopCategories();
			Assert.IsNotNull(categories);
            Assert.AreEqual(5, categories.Count);
            Assert.IsTrue(categories.Any(c => c.Name == "9"));
            Assert.IsTrue(categories.Any(c => c.Name == "8"));
            Assert.IsTrue(categories.Any(c => c.Name == "7"));
            Assert.IsTrue(categories.Any(c => c.Name == "6"));
            Assert.IsTrue(categories.Any(c => c.Name == "5"));
		}

        [Test]
        public void GetAppSettings_WhenSettingHasSetAlready()
        {
			DbSetup();
			var repositoryCore = new RepositoryCore(new LogService());
            var setting = new Setting();
            setting.CurrentYear = 2013;
            setting.CurrentMonth = 7;
            setting.Upsert();

            var currentSetting = repositoryCore.GetAppSettings();
            Assert.AreEqual(setting.CurrentMonth, currentSetting.CurrentMonth);
            Assert.AreEqual(setting.CurrentYear, currentSetting.CurrentYear);
        }

        [Test]
		public void GetAppSettings_WhenSettingHasNotSet()
		{
			DbSetup();
			var repositoryCore = new RepositoryCore(new LogService());

			var currentSetting = repositoryCore.GetAppSettings();
            Assert.IsNotNull(currentSetting);
            Assert.AreNotEqual(0, currentSetting.CurrentMonth);
            Assert.AreNotEqual(0,currentSetting.CurrentYear);
		}
    }
}





