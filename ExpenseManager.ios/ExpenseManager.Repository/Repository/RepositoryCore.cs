using System;
using SQLite.Net;
using SQLite.Net.Interop;

namespace ExpenseManager.Repository.Repository
{
    public class RepositoryCore
    {
        public SQLiteConnection CreateDataBase(string dbPath, ISQLitePlatform sqlitePlatform)
        {
            var db = new SQLiteConnection(sqlitePlatform, dbPath);
            db.CreateTable<Category>();

            var insurances = new Category()
            {
                Name = "Insurances",
                Plan = 420
            };
            db.Insert(insurances);
            return db;
        }

        public SQLiteConnection GetDataBase(string dbPath, ISQLitePlatform sqlitePlatform)
        {
            var db = new SQLiteConnection(sqlitePlatform, dbPath);
            return db;
        }
    }
}
