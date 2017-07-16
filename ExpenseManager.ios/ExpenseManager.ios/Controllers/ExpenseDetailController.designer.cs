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
        UIKit.UIPickerView ExpenseDetail_Category { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIDatePicker ExpenseDetail_Date { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem ExpenseDetail_Delete { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ExpenseDetail_Description { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView ExpenseDetail_Map { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ExpenseDetail_MapBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ExpenseDetail_Receipt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ExpenseDetail_RecieptBtn { get; set; }

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

            if (ExpenseDetail_Category != null) {
                ExpenseDetail_Category.Dispose ();
                ExpenseDetail_Category = null;
            }

            if (ExpenseDetail_Date != null) {
                ExpenseDetail_Date.Dispose ();
                ExpenseDetail_Date = null;
            }

            if (ExpenseDetail_Delete != null) {
                ExpenseDetail_Delete.Dispose ();
                ExpenseDetail_Delete = null;
            }

            if (ExpenseDetail_Description != null) {
                ExpenseDetail_Description.Dispose ();
                ExpenseDetail_Description = null;
            }

            if (ExpenseDetail_Map != null) {
                ExpenseDetail_Map.Dispose ();
                ExpenseDetail_Map = null;
            }

            if (ExpenseDetail_MapBtn != null) {
                ExpenseDetail_MapBtn.Dispose ();
                ExpenseDetail_MapBtn = null;
            }

            if (ExpenseDetail_Receipt != null) {
                ExpenseDetail_Receipt.Dispose ();
                ExpenseDetail_Receipt = null;
            }

            if (ExpenseDetail_RecieptBtn != null) {
                ExpenseDetail_RecieptBtn.Dispose ();
                ExpenseDetail_RecieptBtn = null;
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