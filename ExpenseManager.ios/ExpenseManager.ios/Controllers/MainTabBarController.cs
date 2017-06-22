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
                    AddBtn_Clicked();
				})
			, true);
            
			ViewControllerSelected += (sender, e) =>
			{
				switch (TabBar.SelectedItem.Title)
				{
					case "Expenses":
                        if (selectedTab == "Expenses")
                            break;
                        
						NavigationItem.SetRightBarButtonItem(
							new UIBarButtonItem(UIBarButtonSystemItem.Add, (btn, args) =>
							{
                                AddBtn_Clicked();
							}), true);
						break;
					case "Categories":
						NavigationItem.SetRightBarButtonItem(null, true);
						break;
				}
			};
        }

        private void AddBtn_Clicked()
        {
			ExpenseDetailController expenseDetailController =
								Storyboard.InstantiateViewController("ExpenseDetail") as ExpenseDetailController;
			if (expenseDetailController != null)
			{
				NavigationController.PushViewController(expenseDetailController, true);
			}
        }
    }
}