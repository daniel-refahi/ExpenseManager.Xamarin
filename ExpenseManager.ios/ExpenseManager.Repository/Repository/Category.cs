using System;
using System.Collections.Generic;
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

        List<Expense> _Expenses;
        public List<Expense> GetExpenses (SQLiteConnection db)
        { 
            if(_Expenses == null)
            {
                _Expenses = new List<Expense>();
            }
            return _Expenses;
        }

        public void Create(SQLiteConnection db)
        {
            if (Plan < 0)
                throw new InvalidOperationException("Plan can't be negative.");
            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException("Category name can't be empty.");
            if (Name.Length > 100)
                throw new InvalidOperationException("Category name can't be more than 100 character.");
            if (db.Table<Category>().Where(c => c.Name == Name).FirstOrDefault() != null)
                throw new InvalidOperationException("Category name already exists.");
            db.Insert(this);
        }

        public void Update(SQLiteConnection db)
        {
            db.Update(this);
        }

        public void Delete(SQLiteConnection db)
        {
            foreach (var expense in GetExpenses(db))
            {
                expense.Delete();
            }

            db.Delete(this);
        }
    }
}
