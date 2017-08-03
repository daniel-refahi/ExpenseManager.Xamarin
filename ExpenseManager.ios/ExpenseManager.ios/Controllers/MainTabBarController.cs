using Foundation;
using System;
using UIKit;
using ExpenseManager.ios.Utilities;
using ToastIOS;

namespace ExpenseManager.ios
{
    public partial class MainTabBarController : UITabBarController
    {
        int selectedTab;
        public MainTabBarController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            HandleAddBtn();

			ViewControllerSelected += (sender, e) =>
			{
                selectedTab = (int)SelectedIndex;
                if (selectedTab == 2 || selectedTab == 3)
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
                    if(selectedTab == 0)
                        AddExpense();
                    else if (selectedTab == 1)
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