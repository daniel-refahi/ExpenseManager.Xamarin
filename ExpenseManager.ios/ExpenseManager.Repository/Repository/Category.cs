using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Repository.Repository;
using SQLite.Net;
using SQLite.Net.Attributes;

namespace ExpenseManager.Repository
{
    [Table("Category")]
    public class Category
    {
        [PrimaryKey][AutoIncrement]
        public int Id { get; private set; }
        [MaxLength(100)][Unique]
        public string Name { get; set; }
        public double Plan { get; set; }

        public List<Expense> GetExpenses ()
        {
            using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
            {
                var expenses = db.Table<Expense>().Where(e => e.CategoryId == Id);

                return expenses == null ? new List<Expense>() : expenses.ToList();
            }
        }

        public void Create()
        {
            using(var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
            {
				Validate(db);
				db.Insert(this);    
            }
        }

        public void Update()
        {
            using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
            {
                Validate(db);
                db.Update(this);
            }
        }

        public void Delete()
		{
            using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
            {
                foreach (var expense in GetExpenses())
                {
                    expense.Delete();
                }

                db.Delete(this);
            }
        }

        void Validate(SQLiteConnection db)
        {
			if (Plan < 0)
				throw new InvalidOperationException("Plan can't be negative.");
			if (string.IsNullOrWhiteSpace(Name))
				throw new InvalidOperationException("Category name can't be empty.");
			if (Name.Length > 100)
				throw new InvalidOperationException("Category name can't be more than 100 character.");
			if (db.Table<Category>().Where(c => c.Name == Name).FirstOrDefault() != null)
				throw new InvalidOperationException("Category name already exists.");
        }
    }
}
