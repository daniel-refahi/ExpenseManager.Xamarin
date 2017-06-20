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
        UIKit.UILabel ExpenseDetail_CategoryName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ExpenseDetail_Description { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ExpenseDetail_Value { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ExpenseDetail_CategoryName != null) {
                ExpenseDetail_CategoryName.Dispose ();
                ExpenseDetail_CategoryName = null;
            }

            if (ExpenseDetail_Description != null) {
                ExpenseDetail_Description.Dispose ();
                ExpenseDetail_Description = null;
            }

            if (ExpenseDetail_Value != null) {
                ExpenseDetail_Value.Dispose ();
                ExpenseDetail_Value = null;
            }
        }
    }
}