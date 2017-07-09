using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManage.Common;
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

        public Category()
        {
            RepositoryCore.Logger.Log("category ctor without id");
            Id = -1;
        }

        public Category(int id)
        {
			try
            {
                RepositoryCore.Logger.Log("category ctor with id");
                using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
                {
                    var category = db.Find<Category>(id);
                    Id = category.Id;
                    Name = category.Name;
                    Plan = category.Plan;
                }
			} 
            catch (Exception ex) 
            {
                RepositoryCore.Logger.Log("category ctor with id", ex.Message, LogType.Exception);
                throw ex;
			}
        }

        public List<Expense> GetExpenses ()
        {
            try
            {
                RepositoryCore.Logger.Log(nameof(Category.GetExpenses));
                using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
                {
                    var expenses = db.Table<Expense>().Where(e => e.CategoryId == Id);

                    return expenses == null ? new List<Expense>() : expenses.ToList();
                }
            }
            catch (Exception ex)
            {
                RepositoryCore.Logger.Log(nameof(Category.GetExpenses), ex.Message, LogType.Exception);
                throw ex;
            }
        }

        public void Upsert()
        {
            try
            {
                RepositoryCore.Logger.Log(nameof(Category.Upsert), ToString());
                using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
                {
                    Validate(db);
                    if (Id == -1)
                        db.Insert(this);
                    else
                        db.Update(this);
                }
            }
            catch (Exception ex) 
            {
                RepositoryCore.Logger.Log(nameof(Category.Upsert), ex.Message, LogType.Exception);
                throw ex;
            }
        }

        public void Delete()
		{
            try
            {
                RepositoryCore.Logger.Log(nameof(Category.Delete), ToString());
                using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
                {
                    foreach (var expense in GetExpenses())
                    {
                        expense.Delete();
                    }

                    db.Delete(this);
                }
            }
            catch (Exception ex)
            {
                RepositoryCore.Logger.Log(nameof(Category.Delete), ex.Message, LogType.Exception);
                throw ex;
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

        public override string ToString()
        {
            return string.Format("[Category: Id={0}, Name={1}, Plan={2}]", Id, Name, Plan);
        }
    }
}
