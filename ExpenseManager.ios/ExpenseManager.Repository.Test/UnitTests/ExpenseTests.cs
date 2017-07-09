using System;
using System.IO;
using ExpenseManager.Repository.Repository;
using ExpenseManager.Repository.Test.Services;
using NUnit.Framework;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;

namespace ExpenseManager.Repository.Test.UnitTests
{
	[TestFixture]
	public class ExpenseTests
	{
		SQLiteConnection _db;
		int _categoryId;

		public void DbSetup()
		{
			string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "expensemanager.db3");
			if (File.Exists(dbPath))
				File.Delete(dbPath);

			var repositoryCore = new RepositoryCore(new LogService());
			_db = repositoryCore.CreateDataBase(dbPath, new SQLitePlatformIOS());

			var category = new Category()
			{
				Name = "category",
				Plan = 20
			};
            category.Upsert();
			_categoryId = category.Id;
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Create_NegativeValue()
		{
			DbSetup();
			var expense = new Expense()
			{
				CategoryId = _categoryId,
				Description = string.Empty,
				Value = -10
			};
            expense.Upsert();
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Create_LongDescription()
		{
			DbSetup();
			string description = string.Empty;
			for (int i = 0; i < 201; i++)
			{
				description += i;
			}
			var expense = new Expense()
			{
				CategoryId = _categoryId,
				Description = description,
				Value = 10
			};
            expense.Upsert();
		}

		[Test]
		public void Create_Valid()
		{
			DbSetup();
			var expense = new Expense()
			{
				CategoryId = _categoryId,
				Description = string.Empty,
				Value = 10
			};
            expense.Upsert();

			var e = _db.Get<Expense>(expense.Id);
			Assert.AreEqual(e.Description, expense.Description);
			Assert.AreEqual(e.Value, expense.Value);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Update_NegativeValue()
		{
			DbSetup();
			var expense = new Expense()
			{
				CategoryId = _categoryId,
				Description = string.Empty,
				Value = 10
			};
            expense.Upsert();

			expense.Value = -10;
            expense.Upsert();
		}

		[Test]
		public void Update_Valid()
		{
			DbSetup();
			var expense = new Expense()
			{
				CategoryId = _categoryId,
				Description = "old description",
				Value = 10
			};
            expense.Upsert();
			expense.Description = "new description";
			expense.Value = 100;
            expense.Upsert();

			var e = _db.Get<Expense>(expense.Id);
			Assert.AreEqual(e.Description, expense.Description);
			Assert.AreEqual(e.Value, expense.Value);
		}

		[Test]
		public void Ctor_Valid()
		{
			DbSetup();
			var expense = new Expense()
			{
				CategoryId = _categoryId,
				Description = "old description",
				Value = 10
			};
            expense.Upsert();

            var loadedExpense = new Expense(expense.Id);
            Assert.AreEqual(expense.CategoryId, loadedExpense.CategoryId);
            Assert.AreEqual(expense.Description, loadedExpense.Description);
            Assert.AreEqual(expense.Value, loadedExpense.Value);
		}

		[Test]
		public void Delete_Valid()
		{
			DbSetup();
			var expense = new Expense()
			{
				CategoryId = _categoryId,
				Description = string.Empty,
				Value = 10
			};
            expense.Upsert();
            var expenseId = expense.Id;
			expense.Delete();

            var deletedExpense = _db.Table<Expense>().Where(e => e.Id == expenseId).FirstOrDefault();
			Assert.IsNull(deletedExpense);
		}

        [Test]
		public void GetCategory_Valid()
		{
			DbSetup();
			var expense = new Expense()
			{
				CategoryId = _categoryId,
				Description = string.Empty,
				Value = 10
			};
            expense.Upsert();

            var category = expense.GetCategory();

            Assert.AreEqual("category", category.Name);
		}
	}
}
