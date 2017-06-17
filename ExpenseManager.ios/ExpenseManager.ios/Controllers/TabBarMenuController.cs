using System;
using UIKit;

namespace ExpenseManager.ios.Controllers
{
    public class TabBarMenuController : UITabBarController
    {
        UIViewController expensesTab, categoriesTab;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            expensesTab = new ExpensesController();
            expensesTab.Title = "Expenses";
            //expensesTab.View.BackgroundColor = UIColor.Green;
            categoriesTab = new CategoriesController();
            categoriesTab.Title = "Categories";
            //categoriesTab.View.BackgroundColor = UIColor.Orange;
            //tab3 = new UIViewController();
            //tab3.Title = "Red";
            //tab3.View.BackgroundColor = UIColor.Red;

            var tabs = new UIViewController[]{expensesTab, categoriesTab};
            ViewControllers = tabs;
            SelectedViewController = categoriesTab;
        }
    }
}
