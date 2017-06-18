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
		int selectedEmployeeId;
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
            TableView.SeparatorInset.InsetRect(new CGRect(0, 0, 150, 3));
			NavigationItem.Title = "Expenses List";
		}

		public void EditEmployeeClicked(int employeeId, UITableViewCell sender)
		{
			selectedEmployeeId = employeeId;
			PerformSegue("EmployeeListEmployeeDetailSegue", sender);
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			//if (segue.Identifier == "EmployeeListEmployeeDetailSegue")
			//{
			//  var employeeDetailViewController = segue.DestinationViewController as ViewController;
			//  if (employeeDetailViewController != null)
			//  {
			//      // finding the selected item from table view
			//      //var source = TableView.Source as EmployeeListSrouce;
			//      //var rowPath = TableView.IndexPathForSelectedRow.Row;
			//      //var employee = source.GetItem(rowPath);

			//      employeeDetailViewController.EmployeeId = selectedEmployeeId;
			//  }
			//}
		}
	}
}