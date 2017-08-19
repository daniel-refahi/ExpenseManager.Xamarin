using System;
using System.IO;
using ExpenseManage.Common;
using ExpenseManager.ios.Services;
using ExpenseManager.Repository.Repository;
using SQLite.Net.Platform.XamarinIOS;

namespace ExpenseManager.ios.Utilities
{
    public static class CoreUtilities
    {
        static IExpenseManagerLog logService;
        public static IExpenseManagerLog GetLogService()
        {
            if(logService == null)
            {
                logService = new LogService();
                return logService;
            }
            return logService;
        }

		public static void SetUpDatabase(bool deleteIfExist, bool withSeeding)
		{
			string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "expensemanager.db3");
            if (File.Exists(dbPath) && deleteIfExist)
                File.Delete(dbPath);

			var repositoryCore = new RepositoryCore(GetLogService());
            if (File.Exists(dbPath))
				repositoryCore.SetUpDataBaseConnection(dbPath, new SQLitePlatformIOS());
			else
				repositoryCore.CreateDataBase(dbPath, new SQLitePlatformIOS(), withSeeding);
		}
    }
}
