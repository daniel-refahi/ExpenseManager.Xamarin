using System;
namespace ExpenseManage.Common
{
    public interface IExpenseManagerLog
    {
        void Log(string callerName, string log = "", LogType logType = LogType.Info);
        string GetLogs();
    }
}
