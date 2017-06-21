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
    [Register ("ExpenseDetailController")]
    partial class ExpenseDetailController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem ExpenseDetail_Cancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ExpenseDetail_CategoryName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem ExpenseDetail_Delete { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ExpenseDetail_Description { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem ExpenseDetail_Save { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ExpenseDetail_Value { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ExpenseDetail_Cancel != null) {
                ExpenseDetail_Cancel.Dispose ();
                ExpenseDetail_Cancel = null;
            }

            if (ExpenseDetail_CategoryName != null) {
                ExpenseDetail_CategoryName.Dispose ();
                ExpenseDetail_CategoryName = null;
            }

            if (ExpenseDetail_Delete != null) {
                ExpenseDetail_Delete.Dispose ();
                ExpenseDetail_Delete = null;
            }

            if (ExpenseDetail_Description != null) {
                ExpenseDetail_Description.Dispose ();
                ExpenseDetail_Description = null;
            }

            if (ExpenseDetail_Save != null) {
                ExpenseDetail_Save.Dispose ();
                ExpenseDetail_Save = null;
            }

            if (ExpenseDetail_Value != null) {
                ExpenseDetail_Value.Dispose ();
                ExpenseDetail_Value = null;
            }
        }
    }
}