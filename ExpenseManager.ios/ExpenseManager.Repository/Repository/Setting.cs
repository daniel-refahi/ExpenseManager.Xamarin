using System;
using ExpenseManage.Common;
using ExpenseManager.Repository.Repository;
using SQLite.Net;
using SQLite.Net.Attributes;


namespace ExpenseManager.Repository
{
    [Table("Setting")]
    public class Setting
    {
        [PrimaryKey]
		public int Id { get; private set; }
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public int CurrentMonth { get; set; } = DateTime.Now.Month;

        public Setting()
        {
            RepositoryCore.Logger.Log("Settings ctor");
            Id = -1;
        }

        public void Upsert()
		{
			try
            {
                RepositoryCore.Logger.Log(nameof(Setting.Upsert), ToString());
                using (var db = new SQLiteConnection(DBConnectionString.PLATFORM, DBConnectionString.DBPATH))
                {
                    if (Id == -1)
                    {
                        Id = 1;
                        db.Insert(this);
                    }
                    else
                        db.Update(this);
                }
			} 
            catch (Exception ex) 
            {
                RepositoryCore.Logger.Log(nameof(Setting.Upsert), ex.Message, LogType.Exception);
                throw ex;
			}
		}

        public override string ToString()
        {
            return string.Format("[Settings: Id={0}, CurrentYear={1}, CurrentMonth={2}]", Id, CurrentYear, CurrentMonth);
        }
    }
}
