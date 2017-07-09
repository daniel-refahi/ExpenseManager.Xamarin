using Foundation;
using System;
using UIKit;
using ExpenseManager.Repository;
using ToastIOS;
using ExpenseManager.ios.Utilities;
using ExpenseManager.ios.ListSources;
using System.Collections.Generic;
using ExpenseManager.Repository.Repository;
using System.Linq;

namespace ExpenseManager.ios
{
    public partial class ExpenseDetailController : UIViewController
    {
        public int ExpenseId { get; set; }
        Expense expense { get; set; }
        List<Category> categories;

        public ExpenseDetailController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            try
            {
                expense = new Expense(ExpenseId);
                ExpenseDetail_Value.Text = expense.Value.ToString();
                ExpenseDetail_Description.Text = expense.Description;

            }
            catch
            {
                expense = new Expense();
            }

            categories = (new RepositoryCore(CoreUtilities.GetLogService())).GetCategories();
            var categoryNames = categories.Select(c => c.Name).ToList();
            var categorySelectorModel = new CategorySelectorModel(categoryNames);


            ExpenseDetail_Category.Model = categorySelectorModel;
            if(expense.Value != 0)
                ExpenseDetail_Category.Select(categoryNames.IndexOf(expense.GetCategory().Name),0,true);

			ExpenseDetail_Delete.Clicked += ExpenseDetail_Delete_Clicked;
			ExpenseDetail_Save.Clicked += ExpenseDetail_Save_Clicked;
			ExpenseDetail_Cancel.Clicked += ExpenseDetail_Cancel_Clicked;
        }

        void ExpenseDetail_Delete_Clicked(object sender, EventArgs e)
        {
            try
            {
                expense.Delete();
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
                var categoryName = ((CategorySelectorModel)ExpenseDetail_Category.Model).SelectedItem;
                expense.CategoryId = categories.FirstOrDefault(c => c.Name == categoryName).Id;
                expense.Description = ExpenseDetail_Description.Text;
                expense.Value = Convert.ToInt16(ExpenseDetail_Value.Text);
                expense.Upsert();
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