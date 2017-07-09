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
    public class SettingsTest
    {
        SQLiteConnection _db;
        RepositoryCore _repository;
		public void DbSetup()
		{
			string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "expensemanager.db3");
			if (File.Exists(dbPath))
				File.Delete(dbPath);

            _repository = new RepositoryCore(new LogService());
            _db = _repository.CreateDataBase(dbPath, new SQLitePlatformIOS());
		}

		[Test]
		public void GetWhenNoSettingExist()
		{
			DbSetup();
            var settings = _repository.GetAppSettings();

            Assert.AreEqual(DateTime.Now.Year, settings.CurrentYear);
            Assert.AreEqual(DateTime.Now.Month, settings.CurrentMonth);
            Assert.AreEqual(-1, settings.Id);
		}

		[Test]
		public void GetWhenSettingExist()
		{
			DbSetup();
            var settings = _repository.GetAppSettings();
            settings.CurrentMonth = 7;
            settings.CurrentYear = 8;
            settings.Upsert();

            var settings2 = _repository.GetAppSettings();
			Assert.AreEqual(8, settings2.CurrentYear);
			Assert.AreEqual(7, settings2.CurrentMonth);
			Assert.AreNotEqual(-1, settings2.Id);
		}

		[Test]
		public void UpsertWhenNoSettingExist()
		{
			DbSetup();
            var settings = _repository.GetAppSettings();
            settings.CurrentMonth = 8;
            settings.CurrentYear = 9;
            settings.Upsert();

            var newSettings = _db.Table<Setting>().FirstOrDefault();

            Assert.AreEqual(9, newSettings.CurrentYear);
            Assert.AreEqual(8, newSettings.CurrentMonth);
            Assert.AreNotEqual(-1, newSettings.Id);
		}

        [Test]
        public void UpsertWhenSettingExist()
		{
			DbSetup();
			var settings = _repository.GetAppSettings();
			settings.CurrentMonth = 8;
			settings.CurrentYear = 9;
			settings.Upsert();

			var settings1 = _repository.GetAppSettings();
			settings1.CurrentMonth = 1;
			settings1.CurrentYear = 2;  
			settings1.Upsert();

			var newSettings = _db.Table<Setting>().FirstOrDefault();

			Assert.AreEqual(1, newSettings.CurrentMonth);
            Assert.AreEqual(2, newSettings.CurrentYear);
			Assert.AreNotEqual(-1, newSettings.Id);
		}
    }
}