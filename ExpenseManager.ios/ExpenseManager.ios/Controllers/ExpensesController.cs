using Foundation;
using System;
using UIKit;
using ExpenseManager.Repository.Repository;
using ExpenseManager.ios.ListSources;
using CoreGraphics;
using ToastIOS;
using ExpenseManager.ios.Utilities;
using Microsoft.Azure.Mobile.Crashes;

namespace ExpenseManager.ios
{
	public partial class ExpensesController : UITableViewController
	{
		int selectedExpenseId;
		public ExpensesController(IntPtr handle) : base(handle)
		{
            
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
            			
            EdgesForExtendedLayout = UIRectEdge.None;
			ExtendedLayoutIncludesOpaqueBars = false;
            AutomaticallyAdjustsScrollViewInsets = false;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            NavigationItem.Title = StaticValues.ExpenseListNavigationItemTitle;
            ParentViewController.Title = StaticValues.ExpenseListNavigationTitle;
            var repository = new RepositoryCore(CoreUtilities.GetLogService());
			var expenses = repository.GetExpenses();
			var listSource = new ExpenseListSource(expenses, this);
			TableView.Source = listSource;
            TableView.ReloadData();
        }

        public void EditExpenseClicked(int expenseId, UITableViewCell sender)
		{
			selectedExpenseId = expenseId;
            PerformSegue(StaticValues.ExpenseDetailSegue, sender);
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

            if (segue.Identifier == StaticValues.ExpenseDetailSegue)
			{
                var expenseDetailController = segue.DestinationViewController as ExpenseDetailController;
			    if (expenseDetailController != null)
                {
                    expenseDetailController.ExpenseId = selectedExpenseId;
                    // finding the selected item from table view
                    //var source = TableView.Source as EmployeeListSrouce;
                    //var rowPath = TableView.IndexPathForSelectedRow.Row;
                    //var employee = source.GetItem(rowPath);
                }
			}
		}
    }
}