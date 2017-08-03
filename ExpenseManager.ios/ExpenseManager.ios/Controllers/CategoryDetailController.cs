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
            var circularGauge = new SFCircularGauge();
            var scale = new SFCircularScale()
            {
                StartValue = 0,
                EndValue = 100,
                StartAngle = 40,
                SweepAngle = 320,
                RimWidth = 6,
                Interval = 10,
                ShowTicks = true,
                RimColor = UIColor.FromRGB(0, 0, 77),
                MinorTicksPerInterval = 0,
                LabelOffset = 0.1f,
                MajorTickSettings = new SFTickSettings()
                {
                    Color = UIColor.FromRGB(267, 267, 267),
                    Size = 7,
                    Offset = 0.041f,
                    Width = 1.5f
                },
                MinorTickSettings = new SFTickSettings()
                {
                    Color = UIColor.FromRGB(267, 267, 267),
                    Size = 8,
                    Offset = 0.5f
                }
            };

            var needlePointer = new SFNeedlePointer()
            {
                Value = 70,
                KnobColor = UIColor.FromRGB(128, 179, 255),
                KnobRadius = 12,
                Width = 1,
                LengthFactor = 0.7f,
                Color = UIColor.Gray
            };
            scale.Pointers.AddObjects(new List<SFNeedlePointer>() { needlePointer }.ToArray());

            var rangePointer = new SFRangePointer() 
            {
                Value = 70,
                Width = 6,
                Color = UIColor.FromRGB(169,749,722)
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
			_category.Delete();
			NavigationController.PopViewController(true);
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
    }
}