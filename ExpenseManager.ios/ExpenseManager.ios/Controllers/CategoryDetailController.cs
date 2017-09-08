using Foundation;
using System;
using UIKit;
using ExpenseManager.Repository;
using Syncfusion.SfChart.iOS;
using ToastIOS;
using ExpenseManager.ios.Utilities;
using ExpenseManager.ios.ListSources;
using Syncfusion.SfGauge.iOS;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ExpenseManager.ios
{
    public partial class CategoryDetailController : UIViewController
    {
        public int CategoryId { get; set; }
        Category _category;
        public CategoryDetailController (IntPtr handle) : base (handle){}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
            AddDoneButton();

			try
			{
                _category = new Category(CategoryId);
                CategoryDetail_Name.Text = _category.Name;
                CategoryDetail_Plan.Text = _category.Plan.ToString();

                var expenses = _category.GetExpenses();
                var tableView = new UITableViewController();
                var listSource = new ExpenseListSource(expenses, new UITableViewController());
                CategoryDetail_ExpenseList.Source = listSource;
                CategoryDetail_ExpenseList.ReloadData();
                SetupChart();
			}
			catch
			{
                _category = new Category();
			}

            MountEvents();
		}

        void SetupChart()
        {
            var totalExpenses = (nfloat)_category.GetExpenses().Sum(e => e.Value);
            var circularGauge = new SFCircularGauge();
            var maxChartNumber = _category.Plan > 10 ? _category.Plan : 10;
            var scale = new SFCircularScale()
            {
                StartValue = 0,
                EndValue = (nfloat)maxChartNumber,
                StartAngle = 40,
                SweepAngle = 320,
                RimWidth = 6,
                Interval = (int)maxChartNumber/6,
                ShowTicks = true,
                RimColor = StaticValues.LightBlue,
                MinorTicksPerInterval = 0,
                LabelOffset = 0.1f,
                MajorTickSettings = new SFTickSettings()
                {
                    Color = StaticValues.LightBlue,
                    Size = 2,
                    Offset = 0.041f,
                    Width = 1.5f
                },
                MinorTickSettings = new SFTickSettings()
                {
                    Color = StaticValues.LightBlue,
                    Size = 8,
                    Offset = 0.5f
                }
            };

            var needlePointer = new SFNeedlePointer()
            {
                Value = totalExpenses,
                KnobColor = StaticValues.DarkBlue,
                KnobRadius = 12,
                Width = 1,
                LengthFactor = 0.7f,
                Color = StaticValues.LightBlue
            };
            scale.Pointers.AddObjects(new List<SFNeedlePointer>() { needlePointer }.ToArray());

            var rangePointer = new SFRangePointer() 
            {
                Value = totalExpenses,
                Width = 6,
                Color = StaticValues.DarkBlue
            };
            scale.Pointers.AddObjects(new List<SFRangePointer>(){rangePointer}.ToArray());
            circularGauge.Scales.AddObjects(new List<SFCircularScale>() { scale }.ToArray());

            circularGauge.Frame = new RectangleF(10, 10, 
                                     (float)CategoryDetail_Chart.Bounds.Width - 10, 
                                                 (float)CategoryDetail_Chart.Bounds.Height + 10);
            CategoryDetail_Chart.AddSubview(circularGauge);
        }

        void MountEvents()
        {
			CategoryDetail_Delete.Clicked += CategoryDetail_Delete_Clicked;
			CategoryDetail_Save.Clicked += CategoryDetail_Save_Clicked;
			CategoryDetail_Cancel.Clicked += CategoryDetail_Cancel_Clicked;
        }

        void CategoryDetail_Delete_Clicked(object sender, EventArgs e)
        {
            
			UIAlertController actionSheetAlert = 
                UIAlertController.Create("Delete Category", "Please select an action", UIAlertControllerStyle.ActionSheet);
            
			actionSheetAlert
                .AddAction(UIAlertAction
                           .Create("Delete the category with all its expenses", UIAlertActionStyle.Default, (action) => 
                            {
								  _category.Delete();
								  NavigationController.PopViewController(true);
							}));
            actionSheetAlert
                .AddAction(UIAlertAction
                           .Create("Delete just the expenses in this category", UIAlertActionStyle.Default, (action) => 
                            {							
                                _category.DeleteAllExpenses();
								NavigationController.PopViewController(true);
							}));			
			actionSheetAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, (action) => Console.WriteLine("Cancel button pressed.")));

			// Required for iPad - You must specify a source for the Action Sheet since it is
			// displayed as a popover
			UIPopoverPresentationController presentationPopover = actionSheetAlert.PopoverPresentationController;
			if (presentationPopover != null)
			{
				presentationPopover.SourceView = View;
				presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
			}

			PresentViewController(actionSheetAlert, true, null);
        }

        void CategoryDetail_Save_Clicked(object sender, EventArgs e)
        {
			try
			{
                _category.Name = CategoryDetail_Name.Text;
                _category.Plan = Convert.ToInt16(CategoryDetail_Plan.Text);
                _category.Upsert();
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

		void AddDoneButton()
		{
			var keyboardToolbar = new UIToolbar();
			keyboardToolbar.SizeToFit();

			var flexBarButton = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null);

			var doneBtn = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done, (sender, e) =>
			 {
                CategoryDetail_Name.EndEditing(true);
				CategoryDetail_Plan.EndEditing(true);
			 });
			keyboardToolbar.Items = new UIBarButtonItem[] { flexBarButton, doneBtn };
			CategoryDetail_Name.InputAccessoryView = keyboardToolbar;
            CategoryDetail_Plan.InputAccessoryView = keyboardToolbar;
		}
    }
}