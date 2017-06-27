using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManager.ios.ListCells;
using ExpenseManager.Repository;
using Foundation;
using UIKit;

namespace ExpenseManager.ios.ListSources
{
	public class CategoryListSource:UITableViewSource
	{
        List<Category> categories;
		string CellIdentifier = "CategoryCell";
        CategoriesController parentView;
        public CategoryListSource(List<Category> categories, UITableViewController viewController)
		{
            this.categories = categories;
            parentView = viewController as CategoriesController;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			tableView.RowHeight = 60;
            var cell = tableView.DequeueReusableCell(CellIdentifier) as CategoryListCell;
			if (cell == null)
				cell = new CategoryListCell(CellIdentifier, parentView);

            var totalExpenses = categories[indexPath.Row].GetExpenses().Sum(e=> e.Value);
            cell.UpdateCell(categories[indexPath.Row].Id, categories[indexPath.Row].Name, totalExpenses);

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
			return categories.Count;
		}

        public Category GetItem(int index)
		{
			return categories[index];
		}
	}
}
