using System;
using System.Collections.Generic;
using System.IO;
using ExpenseManage.Common;

namespace ExpenseManager.ios.Utilities
{
    public static class StaticValues
    {
		public static string DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"expensemanager.db3");
        public static int ToastDuration = 3000;
        public static Queue<LogEventArgument> LogQueue = new Queue<LogEventArgument>();


        public static readonly string CategoryListNavigationItemTitle = "Categories List";
        public static readonly string CategoryListNavigationTitle = "Categories";
        public static readonly string CategoryDetailSegue = "CategoryDetailSegue";
        public static readonly string CategoryDetailView = "CategoryDetail";
        public static readonly string CategoryCellIdentifier = "CategoryCell";

        public static readonly string ExpenseListNavigationItemTitle = "Expenses List";
        public static readonly string ExpenseListNavigationTitle = "Expenses";
        public static readonly string ExpenseDetailSegue = "ExpenseDetailSeque";
        public static readonly string ExpenseDetailView = "ExpenseDetail";
        public static readonly string ExpenseCellIdentifier = "ExpenseCell";


    }
}
