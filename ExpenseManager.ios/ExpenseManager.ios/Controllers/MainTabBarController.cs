using Foundation;
using System;
using UIKit;
using ExpenseManager.ios.Utilities;
using ToastIOS;

namespace ExpenseManager.ios
{
    public partial class MainTabBarController : UITabBarController
    {
        string selectedTab;
        public MainTabBarController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            selectedTab = StaticValues.ExpenseListNavigationTitle;

            HandleAddBtn();

			ViewControllerSelected += (sender, e) =>
			{
				selectedTab = TabBar.SelectedItem.Title;
                if (selectedTab == StaticValues.ReportNavigationTitle || selectedTab == StaticValues.SettingsNavigationTitle)
                    NavigationItem.RightBarButtonItem.Enabled = false;
                else 
                    NavigationItem.RightBarButtonItem.Enabled = true;

			};
        }

        void HandleAddBtn()
        {
			NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, args) =>
				{
                    if(selectedTab == StaticValues.ExpenseListNavigationTitle)
                        AddExpense();
                    else if (selectedTab == StaticValues.CategoryListNavigationTitle)
                        AddCategory();
				})
			, true);
        }

        void AddExpense()
        {
			ExpenseDetailController expenseDetailController =
                Storyboard.InstantiateViewController(StaticValues.ExpenseDetailView) as ExpenseDetailController;
			if (expenseDetailController != null)
			{
				NavigationController.PushViewController(expenseDetailController, true);
			}
        }

		void AddCategory()
		{
            CategoryDetailController categoryDetailController =
                Storyboard.InstantiateViewController(StaticValues.CategoryDetailView) as CategoryDetailController;
			if (categoryDetailController != null)
			{
				NavigationController.PushViewController(categoryDetailController, true);
			}
		}
    }
}