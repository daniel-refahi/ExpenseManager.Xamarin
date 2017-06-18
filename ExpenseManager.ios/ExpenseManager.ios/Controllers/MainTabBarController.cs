using Foundation;
using System;
using UIKit;

namespace ExpenseManager.ios
{
    public partial class MainTabBarController : UITabBarController
    {
        public MainTabBarController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }

        partial void Clicked(UIBarButtonItem sender)
        {
            throw new NotImplementedException();
        }
    }
}