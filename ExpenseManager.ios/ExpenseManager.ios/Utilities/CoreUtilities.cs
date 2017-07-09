using System;
using ExpenseManage.Common;
using ExpenseManager.ios.Services;

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
    }
}
