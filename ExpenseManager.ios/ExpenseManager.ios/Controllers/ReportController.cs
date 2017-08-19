using Foundation;
using System;
using System.Linq;
using UIKit;
using Syncfusion.SfChart.iOS;
using ExpenseManager.ios.ListSources;
using System.Drawing;
using ExpenseManager.ios.Utilities;
using ExpenseManager.Repository.Repository;

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

            ParentViewController.Title = StaticValues.ReportNavigationTitle;

            foreach (var view in View.Subviews)
            {
                view.RemoveFromSuperview();
            }

			SFChart chart = new SFChart();
			chart.Frame = this.View.Frame;

			chart.Legend.Visible = true;

			SFCategoryAxis primaryAxis = new SFCategoryAxis();
			chart.PrimaryAxis = primaryAxis;
            chart.ColorModel.Palette = SFChartColorPalette.Custom;
            chart.ColorModel.CustomColors = NSArray.FromObjects(StaticValues.DarkBlue,StaticValues.LightBlue);


			var repository = new RepositoryCore(CoreUtilities.GetLogService());
			var topCategories = repository.GetTopCategories();
			var totalExpense = repository.GetExpenses();
			var averageSpent = totalExpense.Count == 0 ? 0 : totalExpense.Average(e => e.Value);

            if (topCategories.Count > 0)
            {
                var dataModel = new ReportChartDataSource(topCategories,averageSpent);
                chart.DataSource = dataModel as SFChartDataSource;

                this.View.AddSubview(chart);
                chart.Frame = new RectangleF(10, 70, (float)View.Bounds.Width - 10, (float)View.Bounds.Height - 160);
            }
		}
    }
}