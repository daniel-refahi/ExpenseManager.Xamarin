using Foundation;
using System;
using UIKit;
using ExpenseManager.Repository.Repository;
using ExpenseManager.ios.ListSources;

namespace ExpenseManager.ios
{
    public partial class CategoriesController : UITableViewController
    {
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
			NavigationItem.Title = "Categories List";
			ParentViewController.Title = "Categories";
			var repository = new RepositoryCore();
			var categories = repository.GetCategories();
            var listSource = new CategoryListSource(categories, this);
			TableView.Source = listSource;
			TableView.ReloadData();
		}
    }
}

