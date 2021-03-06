﻿using System;
using System.IO;
using ExpenseManager.Repository.Repository;
using ExpenseManager.Repository.Test.Services;
using NUnit.Framework;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;

namespace ExpenseManager.Repository.Test.UnitTests
{
    [TestFixture]
    public class CategoryTests
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

        [Test()][ExpectedException(typeof(InvalidOperationException))]
        public void Create_NegativePlan()
		{
            DbSetup();
            var category = new Category()
            {
                Name = "some name",
                Plan = -20
            };
            category.Upsert();
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
            category.Upsert();
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
            category.Upsert();
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
            category.Upsert();

            var invalidCategory = new Category() 
            {
                Name = "some name",
                Plan = 12
            };
            invalidCategory.Upsert();
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
            category.Upsert();

            var db = new SQLiteConnection(new SQLitePlatformIOS(), _dbPath);
            var c = db.Get<Category>(category.Id);
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
			category.Upsert();

            category.Plan = -10;
            category.Upsert();
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
			category.Upsert();

            category.Name = string.Empty;
            category.Upsert();
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
			category.Upsert();

			for (int i = 0; i < 101; i++)
			{
                category.Name += i;
			}
            category.Upsert();
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
			category.Upsert();

			var invalidCategory = new Category()
			{
				Name = "some name",
				Plan = 12
			};
            invalidCategory.Upsert();
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
			category.Upsert();

            category.Name = "new name";
            category.Plan = 23;
            category.Upsert();

            var db = new SQLiteConnection(new SQLitePlatformIOS(), _dbPath);
			var c = db.Get<Category>(category.Id);
			Assert.AreEqual(c.Name, category.Name);
			Assert.AreEqual(c.Plan, category.Plan);
		}

		[Test]
		public void UpdateOnlyPlan_Valid()
		{
			DbSetup();
			var category = new Category()
			{
				Name = "some name",
				Plan = 10
			};
			category.Upsert();

			category.Name = "some name";
			category.Plan = 23;
			category.Upsert();

			var db = new SQLiteConnection(new SQLitePlatformIOS(), _dbPath);
			var c = db.Get<Category>(category.Id);
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
			category.Upsert();

            for (int i = 0; i < 5; i++)
            {
				var e1 = new Expense()
				{
					CategoryId = category.Id,
					Value = 20
				};
                e1.Upsert();
            }

            category.Delete();

            var db = new SQLiteConnection(new SQLitePlatformIOS(), _dbPath);
            var deletedCategory = db.Table<Category>().Where(c => c.Id == category.Id).FirstOrDefault();
            Assert.IsNull(deletedCategory);

            var deletedExpense = db.Table<Expense>().Where(e => e.CategoryId == category.Id).FirstOrDefault();
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
			category.Upsert();

            var expenses = category.GetExpenses();
            Assert.AreEqual(0, expenses.Count);
		}

		[Test]
		public void GetExpenses_Valid()
		{
			DbSetup();
			var category = new Category(){Name = "some name",Plan = 10};
			category.Upsert();

			var e1 = new Expense(){CategoryId = category.Id,Value = 20};
			e1.Upsert();

			var e2 = new Expense(){CategoryId = category.Id,Value = 25};
			e2.Upsert();

            var e3 = new Expense(){CategoryId = category.Id,Value = 25, ExpenseDate = DateTime.Now.AddMonths(-2)};
			e3.Upsert();

            // to test the utc issue with sqlite
            var beginingOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 1, 1, 1);
            var e4 = new Expense() { CategoryId = category.Id, Value = 25, ExpenseDate =  beginingOfMonth};
			e4.Upsert();

            var e5 = new Expense() { CategoryId = category.Id, Value = 25, ExpenseDate = beginingOfMonth.AddHours(-2) };
			e5.Upsert();

			var expenses = category.GetExpenses();
			Assert.AreEqual(3, expenses.Count);
            Assert.AreEqual(20, expenses[0].Value);
			Assert.AreEqual(25, expenses[1].Value);
		}

		[Test]
		public void Ctor_Valid()
		{
			DbSetup();
			var category = new Category()
			{
				Name = "some name",
				Plan = 10
			};
			category.Upsert();

            var loadedCategory = new Category(category.Id);
            Assert.AreEqual(category.Name, loadedCategory.Name);
            Assert.AreEqual(category.Plan, loadedCategory.Plan);
		}
    }
}
