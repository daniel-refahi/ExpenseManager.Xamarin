using System;
using SQLite.Net;
using SQLite.Net.Attributes;

namespace ExpenseManager.Repository.Repository
{
    [Table("Category")]
    public class Settings
    {
		[PrimaryKey][AutoIncrement]
		public int Id { get; private set; }
        public DateTime SelectedStartTime { get; set; }
        public DateTime SelectedEndTime { get; set; }

        public Settings()
        {
			using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
			{
                var settings = db.Table<Settings>().FirstOrDefault();

                if(settings == null)
                {
                    Id = -1;
                    SelectedStartTime = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
                    SelectedEndTime = SelectedStartTime.AddMonths(1);
                }
                else
                {
                    Id = settings.Id;
                    SelectedStartTime = settings.SelectedStartTime;
                    SelectedEndTime = settings.SelectedEndTime;
                }
			}
        }

        public void Upsert()
		{
			using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
			{
				if (Id == -1)
					db.Insert(this);
				else
					db.Update(this);
			}
		}
    }
}
