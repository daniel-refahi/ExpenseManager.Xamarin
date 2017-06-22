using Foundation;
using System;
using UIKit;
using ExpenseManager.Repository;
using ToastIOS;
using ExpenseManager.ios.Utilities;

namespace ExpenseManager.ios
{
    public partial class ExpenseDetailController : UIViewController
    {
        public int ExpenseId { get; set; }
        Expense _expense { get; set; }
        public ExpenseDetailController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            try
            {
                _expense = new Expense(ExpenseId);
                ExpenseDetail_Value.Text = _expense.Value.ToString();
                ExpenseDetail_Description.Text = _expense.Description;
                ExpenseDetail_CategoryName.Text = _expense.GetCategory().Name;

            }
            catch
            {
                _expense = new Expense();
            }

			ExpenseDetail_Delete.Clicked += ExpenseDetail_Delete_Clicked;
			ExpenseDetail_Save.Clicked += ExpenseDetail_Save_Clicked;
			ExpenseDetail_Cancel.Clicked += ExpenseDetail_Cancel_Clicked;
        }

        void ExpenseDetail_Delete_Clicked(object sender, EventArgs e)
        {
            try
            {
                _expense.Delete();
                NavigationController.PopViewController(true);
            }
            catch
            {
                Toast.MakeText("Something went wrong!").SetDuration(StaticValues.ToastDuration).Show();
            }
        }

        void ExpenseDetail_Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                _expense.Description = ExpenseDetail_Description.Text;
                _expense.Value = Convert.ToInt16(ExpenseDetail_Value.Text);
                _expense.Update();
                NavigationController.PopViewController(true);
            }
            catch(InvalidOperationException ex)
            {
                Toast.MakeText(ex.Message).SetDuration(StaticValues.ToastDuration).Show();
            }
            catch
            {
                Toast.MakeText("Please provide correct values.").SetDuration(StaticValues.ToastDuration).Show();
            }
        }

        void ExpenseDetail_Cancel_Clicked(object sender, EventArgs e)
        {
            NavigationController.PopViewController(true);
        }
    }
}