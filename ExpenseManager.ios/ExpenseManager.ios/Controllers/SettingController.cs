using Foundation;
using System;
using UIKit;
using ExpenseManager.ios.Utilities;

namespace ExpenseManager.ios
{
    public partial class SettingController : UIViewController
    {
        public SettingController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

            ParentViewController.Title = StaticValues.SettingsNavigationTitle;

		}
    }
}