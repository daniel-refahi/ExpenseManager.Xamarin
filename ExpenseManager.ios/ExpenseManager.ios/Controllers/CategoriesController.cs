using Foundation;
using System;
using UIKit;
using ExpenseManager.Repository.Repository;
using ExpenseManager.ios.ListSources;
using ExpenseManager.ios.Utilities;

namespace ExpenseManager.ios
{
    public partial class CategoriesController : UITableViewController
    {
        int selectedCategoryId;
        public CategoriesController (IntPtr handle) : base (handle)
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
            NavigationItem.Title = StaticValues.CategoryListNavigationItemTitle;
            ParentViewController.Title = StaticValues.CategoryListNavigationTitle;
            var repository = new RepositoryCore(CoreUtilities.GetLogService());
			var categories = repository.GetCategories();
            var listSource = new CategoryListSource(categories, this);
			TableView.Source = listSource;
			TableView.ReloadData();
		}

		public void EditCategoryClicked(int categoryId, UITableViewCell sender)
		{
            selectedCategoryId = categoryId;
            PerformSegue(StaticValues.CategoryDetailSegue, sender);
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

            if (segue.Identifier == StaticValues.CategoryDetailSegue)
			{
                var categoryDetailController = segue.DestinationViewController as CategoryDetailController;
				if (categoryDetailController != null)
				{
                    categoryDetailController.CategoryId = selectedCategoryId;
					// finding the selected item from table view
					//var source = TableView.Source as EmployeeListSrouce;
					//var rowPath = TableView.IndexPathForSelectedRow.Row;
					//var employee = source.GetItem(rowPath);
				}
			}
		}
    }
}

