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
    [Register ("SettingController")]
    partial class SettingController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView Settings_MonthPicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView Settings_YearPicker { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Settings_MonthPicker != null) {
                Settings_MonthPicker.Dispose ();
                Settings_MonthPicker = null;
            }

            if (Settings_YearPicker != null) {
                Settings_YearPicker.Dispose ();
                Settings_YearPicker = null;
            }
        }
    }
}