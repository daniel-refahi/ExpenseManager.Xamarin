using System;
using ExpenseManage.Common;

namespace ExpenseManager.Repository.Test.Services
{
	public class LogService : IExpenseManagerLog
	{
		public string GetLogs()
		{
            return string.Empty;
		}

		public void Log(string callerName, string log, LogType logType)
		{
			
		}
	}
}
