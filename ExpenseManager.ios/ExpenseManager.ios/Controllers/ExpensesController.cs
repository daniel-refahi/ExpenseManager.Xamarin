using Foundation;
using System;
using UIKit;
using ExpenseManager.Repository.Repository;
using ExpenseManager.ios.ListSources;
using CoreGraphics;
using ToastIOS;
using ExpenseManager.ios.Utilities;

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

			NavigationItem.Title = "Expenses List";
            ParentViewController.Title = "Expenses";
   //         this.EdgesForExtendedLayout = UIRectEdge.None;
			//this.ExtendedLayoutIncludesOpaqueBars = false;
            //this.AutomaticallyAdjustsScrollViewInsets = false;


		}

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
			var repository = new RepositoryCore();
			var expenses = repository.GetExpenses();
			var listSource = new ExpenseListSource(expenses, this);
			TableView.Source = listSource;
            TableView.ReloadData();

			

            //ExpensesList_AddBtn.TouchUpInside += ExpensesList_AddBtn_Clicked;
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