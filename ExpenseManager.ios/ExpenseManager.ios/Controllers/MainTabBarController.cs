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
            selectedTab = "Expenses";

            HandleAddBtn();
        }

        void HandleAddBtn()
        {
			NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, args) =>
				{
                    switch(selectedTab)
	                {
                        case "Expenses":
                            AddExpense();
                            break;
                        case "Categories":
                            AddCategory();
                            break;
	                }
				})
			, true);
            
			ViewControllerSelected += (sender, e) =>
			{
				switch (TabBar.SelectedItem.Title)
				{
					case "Expenses":
                        selectedTab = "Expenses";
						break;
					case "Categories":
                        selectedTab = "Categories";
                        break;
				}
			};
        }

        void AddExpense()
        {
			ExpenseDetailController expenseDetailController =
								Storyboard.InstantiateViewController("ExpenseDetail") as ExpenseDetailController;
			if (expenseDetailController != null)
			{
				NavigationController.PushViewController(expenseDetailController, true);
			}
        }

		void AddCategory()
		{
            CategoryDetailController categoryDetailController =
								Storyboard.InstantiateViewController("CategoryDetail") as CategoryDetailController;
			if (categoryDetailController != null)
			{
				NavigationController.PushViewController(categoryDetailController, true);
			}
		}
    }
}