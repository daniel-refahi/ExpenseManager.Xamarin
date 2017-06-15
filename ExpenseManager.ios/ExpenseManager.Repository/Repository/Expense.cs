using System;
using ExpenseManager.Repository.Repository;
using SQLite.Net;
using SQLite.Net.Attributes;

namespace ExpenseManager.Repository
{
    public class Expense
    {
        [PrimaryKey][AutoIncrement]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public double Value { get; set; }
        public string ReceiptImage { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int CategoryId { get; set; }

        public Expense(){}

        public Expense(int id)
        {
			using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
			{
				var expense = db.Find<Expense>(id);
				Id = expense.Id;
				Description = expense.Description;
				Value = expense.Value;
				ReceiptImage = expense.ReceiptImage;
				Longitude = expense.Longitude;
				Latitude = expense.Latitude;
				CategoryId = expense.CategoryId;
			}
        }

        public Category GetCategory()
        {
            using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
            {
                return db.Get<Category>(CategoryId);
            }
        }

        public void Create()
        {
            using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
            {
                Validate();
                db.Insert(this);
            }
        }

        public void Update()
        {
            using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
            {
                Validate();
                db.Update(this);
            }
        }

        public void Delete()
        {
            using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
            {
                db.Delete(this);
            }
        }

        void Validate()
        {
            if (Description?.Length > 200)
                throw new InvalidOperationException("The description can't be more than 200 characters.");
            if (Value <= 0)
                throw new InvalidOperationException("The expense value can't be less than 0.");
        }
    }
}
