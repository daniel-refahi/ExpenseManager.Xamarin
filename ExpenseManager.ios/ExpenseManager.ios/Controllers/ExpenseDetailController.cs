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
using AVFoundation;

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
            loadExpense();
            loadCategorySelector();

			ExpenseDetail_Delete.Clicked += ExpenseDetail_Delete_Clicked;
			ExpenseDetail_Save.Clicked += ExpenseDetail_Save_Clicked;
			ExpenseDetail_Cancel.Clicked += ExpenseDetail_Cancel_Clicked;
            ExpenseDetail_RecieptBtn.TouchUpInside += ExpenseDetail_RecieptBtn_TouchUpInside;
        }

        void loadExpense()
        {
			try
			{
				expense = new Expense(ExpenseId);
				ExpenseDetail_Value.Text = expense.Value.ToString();
				ExpenseDetail_Description.Text = expense.Description;
				ExpenseDetail_Date.Date = expense.ExpenseDate.ToNSDate();
			}
			catch
			{
				expense = new Expense();
			}
        }

        void loadCategorySelector()
        {
			categories = (new RepositoryCore(CoreUtilities.GetLogService())).GetCategories();
			var categoryNames = categories.Select(c => c.Name).ToList();
			var categorySelectorModel = new CategorySelectorModel(categoryNames);


			ExpenseDetail_Category.Model = categorySelectorModel;
			if (expense.Value != 0)
				ExpenseDetail_Category.Select(categoryNames.IndexOf(expense.GetCategory().Name), 0, true);
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
                expense.ExpenseDate = ExpenseDetail_Date.Date.ToDateTime();
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

        async void ExpenseDetail_RecieptBtn_TouchUpInside(object sender, EventArgs e)
        {
			var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);

			if (authorizationStatus != AVAuthorizationStatus.Authorized)
			{
				await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
			}

			Camera.TakePicture(this, (obj) =>
			{
				var photo = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
				var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				string jpgFilename = System.IO.Path.Combine(documentsDirectory, $"{Guid.NewGuid().ToString()}.jpg"); // hardcoded filename, overwritten each time
				NSData imgData = photo.AsJPEG();
				NSError err = null;
				if (imgData.Save(jpgFilename, false, out err))
				{
					Console.WriteLine("saved as " + jpgFilename);
				}
				else
				{
					Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
				}
			});
        }
    }
}