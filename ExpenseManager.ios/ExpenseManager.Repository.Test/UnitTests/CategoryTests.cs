using System;
using System.IO;
using ExpenseManager.Repository.Repository;
using NUnit.Framework;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;

namespace ExpenseManager.Repository.Test.UnitTests
{
    [TestFixture]
    public class CategoryTests
    {
        SQLiteConnection _db;

        public void DbSetup()
        {
			string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "expensemanager.db3");
			if (File.Exists(dbPath))
				File.Delete(dbPath);
            
			var repositoryCore = new RepositoryCore();
			_db = repositoryCore.CreateDataBase(dbPath, new SQLitePlatformIOS());
        }

        [Test][ExpectedException(typeof(InvalidOperationException))]
        public void Create_NegativePlan()
		{
            DbSetup();
            var category = new Category()
            {
                Name = "some name",
                Plan = -20
            };
            category.Create(_db);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Create_EmptyName()
		{
            DbSetup();
			var category = new Category()
			{
				Name = "",
				Plan = 10
			};
            category.Create(_db);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Create_LongName()
		{
            DbSetup();
            string name = string.Empty;
            for (int i = 0; i < 101; i++)
            {
                name += i;
            }

            var category = new Category()
			{
				Name = name,
				Plan = 10
			};
            category.Create(_db);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Create_ExistingCategoryName()
		{
            DbSetup();
		    var category = new Category()
			{
				Name = "some name",
				Plan = 10
			};
            category.Create(_db);

            var invalidCategory = new Category() 
            {
                Name = "some name",
                Plan = 12
            };
            category.Create(_db);
		}

        [Test]
		public void Create_Valid()
		{
            DbSetup();
			var category = new Category()
			{
				Name = "some name",
				Plan = 10
			};
            category.Create(_db);

            var c = _db.Get<Category>(category.Id);
            Assert.AreEqual(c.Name, category.Name);
            Assert.AreEqual(c.Plan, category.Plan);
		}
    }
}
