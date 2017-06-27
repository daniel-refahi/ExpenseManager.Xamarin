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
    [Register ("CategoryDetailController")]
    partial class CategoryDetailController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem CategoryDetail_Cancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem CategoryDetail_Delete { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField CategoryDetail_Name { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField CategoryDetail_Plan { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem CategoryDetail_Save { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CategoryDetail_Cancel != null) {
                CategoryDetail_Cancel.Dispose ();
                CategoryDetail_Cancel = null;
            }

            if (CategoryDetail_Delete != null) {
                CategoryDetail_Delete.Dispose ();
                CategoryDetail_Delete = null;
            }

            if (CategoryDetail_Name != null) {
                CategoryDetail_Name.Dispose ();
                CategoryDetail_Name = null;
            }

            if (CategoryDetail_Plan != null) {
                CategoryDetail_Plan.Dispose ();
                CategoryDetail_Plan = null;
            }

            if (CategoryDetail_Save != null) {
                CategoryDetail_Save.Dispose ();
                CategoryDetail_Save = null;
            }
        }
    }
}