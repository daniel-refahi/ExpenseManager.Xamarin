﻿using System;
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
            category.Create();
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
            category.Create();
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
            category.Create();
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
            category.Create();

            var invalidCategory = new Category() 
            {
                Name = "some name",
                Plan = 12
            };
            category.Create();
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
            category.Create();

            var c = _db.Get<Category>(category.Id);
            Assert.AreEqual(c.Name, category.Name);
            Assert.AreEqual(c.Plan, category.Plan);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Update_NegativePlan()
		{
			DbSetup();
			var category = new Category()
			{
				Name = "some name",
				Plan = 20
			};
			category.Create();

            category.Plan = -10;
            category.Update();
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Update_EmptyName()
		{
			DbSetup();
			var category = new Category()
			{
				Name = "some name",
				Plan = 10
			};
			category.Create();

            category.Name = string.Empty;
            category.Update();
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Update_LongName()
		{
			DbSetup();

			var category = new Category()
			{
				Name = "some name",
				Plan = 10
			};
			category.Create();

			for (int i = 0; i < 101; i++)
			{
                category.Name += i;
			}
            category.Update();
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Update_ExistingCategoryName()
		{
			DbSetup();
			var category = new Category()
			{
				Name = "some name",
				Plan = 10
			};
			category.Create();

			var invalidCategory = new Category()
			{
				Name = "some name",
				Plan = 12
			};
            category.Update();
		}

		[Test]
		public void Update_Valid()
		{
			DbSetup();
			var category = new Category()
			{
				Name = "some name",
				Plan = 10
			};
			category.Create();

            category.Name = "new name";
            category.Plan = 23;
            category.Update();

			var c = _db.Get<Category>(category.Id);
			Assert.AreEqual(c.Name, category.Name);
			Assert.AreEqual(c.Plan, category.Plan);
		}

		[Test]
		public void Delete_Valid()
		{
			DbSetup();
			var category = new Category()
			{
				Name = "some name",
				Plan = 10
			};
			category.Create();

            for (int i = 0; i < 5; i++)
            {
				var e1 = new Expense()
				{
					CategoryId = category.Id,
					Value = 20
				};
				e1.Create();
            }

            category.Delete();

            var deletedCategory = _db.Table<Category>().Where(c => c.Id == category.Id).FirstOrDefault();
            Assert.IsNull(deletedCategory);

            var deletedExpense = _db.Table<Expense>().Where(e => e.CategoryId == category.Id).FirstOrDefault();
			Assert.IsNull(deletedExpense);
		}

		[Test]
		public void GetExpenses_WhenNoExpenseExist()
		{
			DbSetup();
			var category = new Category()
			{
				Name = "some name",
				Plan = 10
			};
			category.Create();

            var expenses = category.GetExpenses();
            Assert.AreEqual(0, expenses.Count);
		}

		[Test]
		public void GetExpenses_Valid()
		{
			DbSetup();
			var category = new Category()
			{
				Name = "some name",
				Plan = 10
			};
			category.Create();

            var e1 = new Expense() 
            {
                CategoryId = category.Id,
                Value = 20
            };
            e1.Create();

			var e2 = new Expense()
			{
				CategoryId = category.Id,
				Value = 25
			};
			e2.Create();

			var expenses = category.GetExpenses();
            Assert.AreEqual(2,expenses.Count);
            Assert.AreEqual(25,expenses[1].Value);
		}
    }
}
