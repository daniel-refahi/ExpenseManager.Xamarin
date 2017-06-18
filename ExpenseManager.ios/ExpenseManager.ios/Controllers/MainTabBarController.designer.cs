// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ExpenseManager.ios
{
    [Register ("MainTabBarController")]
    partial class MainTabBarController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem TabBarAddBtn { get; set; }

        [Action ("Clicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Clicked (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
            if (TabBarAddBtn != null) {
                TabBarAddBtn.Dispose ();
                TabBarAddBtn = null;
            }
        }
    }
}