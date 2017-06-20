using Foundation;
using System;
using UIKit;
using ExpenseManager.Repository;

namespace ExpenseManager.ios
{
    public partial class ExpenseDetailController : UIViewController
    {
        public int ExpenseId { get; set; }
        public ExpenseDetailController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var expense = new Expense(ExpenseId);
            ExpenseDetail_Value.Text = expense.Value.ToString("C0");
            ExpenseDetail_Description.Text = expense.Description;
            ExpenseDetail_CategoryName.Text = expense.GetCategory().Name;

        }
    }
}