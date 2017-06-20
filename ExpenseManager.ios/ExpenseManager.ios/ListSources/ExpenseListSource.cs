using System;
using System.Collections.Generic;
using ExpenseManager.ios.Controllers;
using ExpenseManager.ios.ListCells;
using ExpenseManager.Repository;
using Foundation;
using UIKit;

namespace ExpenseManager.ios.ListSources
{
	public class ExpenseListSource : UITableViewSource
	{
        List<Expense> Expenses;
		string CellIdentifier = "EmployeeCell";
        ExpensesController parentView;
        public ExpenseListSource(List<Expense> expenses, UITableViewController viewController)
		{
			Expenses = expenses;
            parentView = viewController as ExpensesController;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			tableView.RowHeight = 60;
			var cell = tableView.DequeueReusableCell(CellIdentifier) as ExpenseListCell;
			if (cell == null)
				cell = new ExpenseListCell(CellIdentifier, parentView);

            cell.UpdateCell(Expenses[indexPath.Row].Id, Expenses[indexPath.Row].Value, Expenses[indexPath.Row].Description);

			//var caption = $"{Expenses[indexPath.Row].FirstName} {Expenses[indexPath.Row].LastName}";
			//var subtitle = Expenses[indexPath.Row].Salary;
			//var avatarPath = $"Images/{Expenses[indexPath.Row].ImageId}.png";
			//var avatarImage = ImageUtilities.ResizeImage(UIImage.FromFile(avatarPath), 35, 40);
			//cell.UpdateCell(caption, subtitle, avatarImage, Expenses[indexPath.Row].Id);
			//cell.AccessibilityIdentifier = Guid.NewGuid().ToString();
			return cell;

			//tableView.RowHeight = 60;
			//var cell = tableView.DequeueReusableCell(CellIdentifier) as UITableViewCell;
			//if (cell == null)
			//    cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);

			//var employee = Employees[indexPath.Row];
			//cell.TextLabel.Text = $"{employee.FirstName} {employee.LastName}";
			//var image = UIImage.FromFile($"Images/{employee.ImageId}.png");
			//cell.ImageView.Image = image;
			//return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return Expenses.Count;
		}

        public Expense GetItem(int index)
		{
			return Expenses[index];
		}
	}
}
