using System;
using System.IO;
using ExpenseManager.Repository.Repository;
using NUnit.Framework;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;

namespace ExpenseManager.Repository.Test.UnitTests
{
    [TestFixture]
    public class RepositoryCoreTests
    {
        string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "expensemanager.db3");

        [Test]
        public void CreateDataBase_IfDbDoesNotExist_CheckDb()
        {
            if (File.Exists(_dbPath))
                File.Delete(_dbPath);
            
            var core = new RepositoryCore();
            core.CreateDataBase(_dbPath, new SQLitePlatformIOS());

            Assert.IsTrue(File.Exists(_dbPath));
        }

		[Test]
		public void CreateDataBase_IfDbDoesNotExist_CheckCategoryTable()
		{
			if (File.Exists(_dbPath))
				File.Delete(_dbPath);

			var core = new RepositoryCore();
			core.CreateDataBase(_dbPath, new SQLitePlatformIOS());

            var db = new SQLiteConnection(new SQLitePlatformIOS(), _dbPath);
            var categories = db.Table<Category>();

            Assert.IsTrue(categories.Count() > 0, "Category table hasn't been created.");
		}
    }
}