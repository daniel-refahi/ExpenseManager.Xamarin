using System;
using ExpenseManage.Common;
using System.Linq;
using ExpenseManager.ios.Utilities;

namespace ExpenseManager.ios.Services
{
    public class LogService : IExpenseManagerLog
    {
        public string GetLogs()
        {
            return StaticValues.LogQueue.Aggregate(string.Empty, (total, next) =>
                                                   $"{next.LogTime} - {next.CallerName} - {next.LogType.ToString()}: {next.LogContent}\r\n");
        }

        public void Log(string callerName, string log, LogType logType)
        {
            if (StaticValues.LogQueue.Count > 50)
                StaticValues.LogQueue.Dequeue();
            StaticValues.LogQueue.Enqueue(new LogEventArgument()
            {
                LogContent = log,
                LogType = logType,
                CallerName = callerName
            });
        }
    }
}
