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
using ExpenseManage.Common;

namespace ExpenseManager.ios
{
    public partial class ExpenseDetailController : UIViewController
    {
        public int ExpenseId { get; set; }
        Expense _expense { get; set; }
        List<Category> _categories;
        NSData _recieptImageData;
        bool _isFromCamara = false;

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

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

			if (_expense.ReceiptImage != null)
			{
                //ExpenseDetail_RecieptBtn.SetTitle("Show Reciept", UIControlState.Normal);
                try
                {
                    if (_isFromCamara)
                        ExpenseDetail_Receipt.Image = new UIImage(_recieptImageData);
                    else 
                        ExpenseDetail_Receipt.Image = new UIImage(_expense.ReceiptImage);
                }
                catch
                {
                    ExpenseDetail_Receipt.Image = new UIImage("Assets/receipt.png");
                }
			}
			else
				ExpenseDetail_Receipt.Image = new UIImage("Assets/receipt.png");
            _isFromCamara = false;
		}

        void loadExpense()
        {
            CoreUtilities.GetLogService().Log(nameof(ExpenseDetailController), "load expenses");
			try
			{
				_expense = new Expense(ExpenseId);
				ExpenseDetail_Value.Text = _expense.Value.ToString();
				ExpenseDetail_Description.Text = _expense.Description;
				ExpenseDetail_Date.Date = _expense.ExpenseDate.ToNSDate();
			}
            catch
			{
                CoreUtilities.GetLogService().Log(nameof(ExpenseDetailController), "load expenses as a new expense");
				_expense = new Expense();
			}
        }

        void loadCategorySelector()
        {
            CoreUtilities.GetLogService().Log(nameof(ExpenseDetailController), "load categoryies");
			_categories = (new RepositoryCore(CoreUtilities.GetLogService())).GetCategories();
			var categoryNames = _categories.Select(c => c.Name).ToList();
			var categorySelectorModel = new CategorySelectorModel(categoryNames);

            CoreUtilities.GetLogService().Log(nameof(ExpenseDetailController), "assiging model for the category selector");
			ExpenseDetail_Category.Model = categorySelectorModel;
			if (_expense.Value != 0)
				ExpenseDetail_Category.Select(categoryNames.IndexOf(_expense.GetCategory().Name), 0, true);
        }

        void ExpenseDetail_Delete_Clicked(object sender, EventArgs e)
        {
			CoreUtilities.GetLogService().Log(nameof(ExpenseDetailController), "try to delete expense");
			_expense.Delete();
			NavigationController.PopViewController(true);
        }

        void ExpenseDetail_Save_Clicked(object sender, EventArgs e)
        {
            try
            {
				CoreUtilities.GetLogService().Log(nameof(ExpenseDetailController), "try to save expense");
                var categoryName = ((CategorySelectorModel)ExpenseDetail_Category.Model).SelectedItem;
                _expense.CategoryId = _categories.FirstOrDefault(c => c.Name == categoryName).Id;
                _expense.Description = ExpenseDetail_Description.Text;
                _expense.Value = Convert.ToInt16(ExpenseDetail_Value.Text);
                _expense.ExpenseDate = ExpenseDetail_Date.Date.ToDateTime();
                _expense.ReceiptImage = saveReciept();

                _expense.Upsert();
                NavigationController.PopViewController(true);
            }
            catch(InvalidOperationException ex)
            {
                Toast.MakeText(ex.Message).SetDuration(StaticValues.ToastDuration).Show();
            }
            catch (Exception ex)
            {
                CoreUtilities.GetLogService().Log(nameof(ExpenseDetailController), ex.Message, LogType.Exception);
                Toast.MakeText("Please provide correct values.").SetDuration(StaticValues.ToastDuration).Show();
            }
        }

        string saveReciept()
        {
            NSError err = null;
            var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var jpgFilename = System.IO.Path.Combine(documentsDirectory, $"{Guid.NewGuid().ToString()}.jpg");
            if (_recieptImageData.Save(jpgFilename, false, out err))
			{
				CoreUtilities.GetLogService().Log(nameof(ExpenseDetailController), "image taken and everything is fine");
                return jpgFilename;
			}
			else
			{
				CoreUtilities.GetLogService().Log(nameof(ExpenseDetailController),
												  $"image not taken because: {err.LocalizedDescription}");
                return null;
			}
 		}

		void ExpenseDetail_Cancel_Clicked(object sender, EventArgs e)
        {
            CoreUtilities.GetLogService().Log(nameof(ExpenseDetailController), "caceling expense page");
            NavigationController.PopViewController(true);
        }

        async void ExpenseDetail_RecieptBtn_TouchUpInside(object sender, EventArgs e)
        {
            _isFromCamara = true;
            CoreUtilities.GetLogService().Log(nameof(ExpenseDetailController), "adding receipt");
			var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);

            CoreUtilities.GetLogService().Log(nameof(ExpenseDetailController), "checking for camera authorization");
			if (authorizationStatus != AVAuthorizationStatus.Authorized)
			{
                CoreUtilities.GetLogService().Log(nameof(ExpenseDetailController), "asking for camera access");
				await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
			}

            CoreUtilities.GetLogService().Log(nameof(ExpenseDetailController), "loading camera");
			Camera.TakePicture(this, (obj) =>
			{
				var photo = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
                _recieptImageData = photo.AsJPEG();
			});
        }
    }
}