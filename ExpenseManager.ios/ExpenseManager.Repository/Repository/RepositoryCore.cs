using System;
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
        public SQLiteConnection CreateDataBase(string dbPath, ISQLitePlatform sqlitePlatform)
        {
            DBConnectionString.DBPATH = dbPath;
            DBConnectionString.PLATFORM = sqlitePlatform;

            var db = new SQLiteConnection(sqlitePlatform, dbPath);
            db.CreateTable<Category>();
            db.CreateTable<Expense>();

            var insurances = new Category()
            {
                Name = "Insurances",
                Plan = 420
            };
            db.Insert(insurances);
            return db;
        }

        public void SetUpDataBaseConnection(string dbPath, ISQLitePlatform sqlitePlatform)
        {
			DBConnectionString.DBPATH = dbPath;
			DBConnectionString.PLATFORM = sqlitePlatform;
        }
    }
}
