using System;
using System.IO;

namespace ExpenseManager.ios.Utilities
{
    public static class StaticValues
    {
		public static string DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"expensemanager.db3");
    }
}
