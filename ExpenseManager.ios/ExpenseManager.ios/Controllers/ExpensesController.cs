using Foundation;
using System;
using UIKit;
using ExpenseManager.Repository.Repository;
using ExpenseManager.ios.ListSources;
using CoreGraphics;

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
			var repository = new RepositoryCore();
			var expenses = repository.GetExpenses();
			var listSource = new ExpenseListSource(expenses, this);
			TableView.Source = listSource;
			NavigationItem.Title = "Expenses List";
            ParentViewController.Title = "Expenses";

		}

        public void EditExpenseClicked(int expenseId, UITableViewCell sender)
		{
			selectedExpenseId = expenseId;
			PerformSegue("ExpenseDetailSeque", sender);
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			if (segue.Identifier == "ExpenseDetailSeque")
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