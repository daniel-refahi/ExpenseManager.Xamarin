using Foundation;
using System;
using UIKit;
using ExpenseManager.Repository;
using ExpenseManager.Repository.Repository;
using ToastIOS;
using ExpenseManager.ios.Utilities;

namespace ExpenseManager.ios
{
    public partial class CategoryDetailController : UIViewController
    {
        public int CategoryId { get; set; }
        Category category;
        public CategoryDetailController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			try
			{
                category = new Category(CategoryId);
                CategoryDetail_Name.Text = category.Name;
                CategoryDetail_Plan.Text = category.Plan.ToString();
			}
			catch
			{
                category = new Category();
			}

            CategoryDetail_Delete.Clicked += CategoryDetail_Delete_Clicked;
            CategoryDetail_Save.Clicked += CategoryDetail_Save_Clicked;
            CategoryDetail_Cancel.Clicked += CategoryDetail_Cancel_Clicked;
		}

        void CategoryDetail_Delete_Clicked(object sender, EventArgs e)
        {
			try
			{
                category.Delete();
				NavigationController.PopViewController(true);
			}
			catch
			{
				Toast.MakeText("Something went wrong!").SetDuration(StaticValues.ToastDuration).Show();
			}
        }

        void CategoryDetail_Save_Clicked(object sender, EventArgs e)
        {
			try
			{
                category.Name = CategoryDetail_Name.Text;
                category.Plan = Convert.ToInt16(CategoryDetail_Plan.Text);
                category.Upsert();
				NavigationController.PopViewController(true);
			}
			catch (InvalidOperationException ex)
			{
				Toast.MakeText(ex.Message).SetDuration(StaticValues.ToastDuration).Show();
			}
			catch
			{
				Toast.MakeText("Please provide correct values.").SetDuration(StaticValues.ToastDuration).Show();
			}
        }

        void CategoryDetail_Cancel_Clicked(object sender, EventArgs e)
        {
            NavigationController.PopViewController(true);
        }
    }
}