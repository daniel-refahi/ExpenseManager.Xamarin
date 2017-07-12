using Foundation;
using System;
using UIKit;
using Syncfusion.SfChart.iOS;
using ExpenseManager.ios.ListSources;

namespace ExpenseManager.ios
{
    public partial class ReportController : UIViewController
    {
        public ReportController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

			SFChart chart = new SFChart();
			chart.Frame = this.View.Frame;

			//Define the title for the Chart.
			chart.Title.Text = new NSString("Weather Analysis");

			//Adding legend to the Chart.
			chart.Legend.Visible = true;

			//Adding Primary Axis for the Chart.
			SFCategoryAxis primaryAxis = new SFCategoryAxis();
			primaryAxis.Title.Text = new NSString("Month");
			chart.PrimaryAxis = primaryAxis;

			//Adding Secondary Axis for the Chart.
			SFNumericalAxis secondaryAxis = new SFNumericalAxis();
			secondaryAxis.Title.Text = new NSString("Temperature");
			chart.SecondaryAxis = secondaryAxis;

			//Defining the data source for the Chart.
			var dataModel = new ReportChartDataSource();
			chart.DataSource = dataModel as SFChartDataSource;

			//Adding our Chart as a sub view.
			this.View.AddSubview(chart);

			// Perform any additional setup after loading the view, typically from a nib.
		}
    }
}