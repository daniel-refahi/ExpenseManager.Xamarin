using System;
using ExpenseManage.Common;
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
        public DateTime ExpenseDate { get; set; } = DateTime.Now;
        public double Value { get; set; }
        public string ReceiptImage { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int CategoryId { get; set; }

        public Expense()
        {
            RepositoryCore.Logger.Log(nameof(Expense), "Ctor with out Id.");
            Id = -1;
        }

        public Expense(int id)
        {
            try
            {
                RepositoryCore.Logger.Log(nameof(Expense), "Ctor with Id.");
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
			catch(Exception ex)
			{
                RepositoryCore.Logger.Log(nameof(Expense), ex.Message, LogType.Exception);
                throw ex;
			}
        }

        public Category GetCategory()
        {
            try
            {
                RepositoryCore.Logger.Log(nameof(Expense.GetCategory));
                using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
                {
                    return db.Get<Category>(CategoryId);
                }
            }
			catch (Exception ex)
			{
                RepositoryCore.Logger.Log(nameof(Expense.GetCategory), ex.Message, LogType.Exception);
				throw ex;
			}
        }

        public void Upsert()
        {
            try
            {
                RepositoryCore.Logger.Log(nameof(Expense.Upsert), $"Upsert: {ToString()}");
                using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
                {
                    Validate();
                    if (Id == -1)
                        db.Insert(this);
                    else
                        db.Update(this);
                }
            }
            catch(InvalidOperationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                RepositoryCore.Logger.Log(nameof(Expense.Upsert), ex.Message, LogType.Exception);
				throw ex;
            }
        }

        public void Delete()
        {
            try
            {
                RepositoryCore.Logger.Log(nameof(Expense.Delete), ToString());
                using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
                {
                    db.Delete(this);
                }
            }
			catch (Exception ex)
			{
                RepositoryCore.Logger.Log(nameof(Expense.Delete), ex.Message, LogType.Exception);
                throw ex;
			}
        }

        void Validate()
        {
            if (Description?.Length > 200)
                throw new InvalidOperationException("The description can't be more than 200 characters.");
            if (Value <= 0)
                throw new InvalidOperationException("The expense value can't be less than 0.");
        }

        public override string ToString()
        {
            return string.Format("[Expense: Id={0}, Description={1}, ExpenseDate={2}, Value={3}, ReceiptImage={4}, Longitude={5}, Latitude={6}, CategoryId={7}]", Id, Description, ExpenseDate, Value, ReceiptImage, Longitude, Latitude, CategoryId);
        }
    }
}
