using System;
using System.Linq;
using ExpenseManager.ios.Utilities;
using ExpenseManager.Repository.Repository;
using Foundation;
using Syncfusion.SfChart.iOS;
using UIKit;

namespace ExpenseManager.ios.ListSources
{
    public class ReportChartDataSource : SFChartDataSource
    {
        NSMutableArray _expenseAverage;
        NSMutableArray _category;
        SFNumericalAxis _axis;

		public ReportChartDataSource()
		{
			_expenseAverage = new NSMutableArray();
			_category = new NSMutableArray();


			var repository = new RepositoryCore(CoreUtilities.GetLogService());
			var topCategories = repository.GetTopCategories();
			var averageSpent = repository.GetExpenses().Average(e => e.Value);

			AddDataPointsForChart(topCategories[0].Name, averageSpent, topCategories[0].GetExpenses().Sum(e => e.Value));
			AddDataPointsForChart(topCategories[1].Name, averageSpent, topCategories[1].GetExpenses().Sum(e => e.Value));
			AddDataPointsForChart(topCategories[2].Name, averageSpent, topCategories[2].GetExpenses().Sum(e => e.Value));
			AddDataPointsForChart(topCategories[3].Name, averageSpent, topCategories[3].GetExpenses().Sum(e => e.Value));
			AddDataPointsForChart(topCategories[4].Name, averageSpent, topCategories[4].GetExpenses().Sum(e => e.Value));



			//AddDataPointsForChart("Jan", 45, 27);
			//AddDataPointsForChart("Feb", 45, 28);
			//AddDataPointsForChart("Mar", 45, 35);
			//AddDataPointsForChart("Apr", 45, 44);
			//AddDataPointsForChart("May", 45, 54);
		}

        void AddDataPointsForChart(String categoryName, Double expsenseAverage, Double categoryValue)
		{
			_expenseAverage.Add(new SFChartDataPoint(NSObject.FromObject(categoryName), NSObject.FromObject(expsenseAverage)));
			_category.Add(new SFChartDataPoint(NSObject.FromObject(categoryName), NSObject.FromObject(categoryValue)));
		}

		[Export("numberOfSeriesInChart:")]
		public override nint NumberOfSeriesInChart(SFChart chart)
		{
			return 2;
		}

		[Export("chart:seriesAtIndex:")]
		public override SFSeries GetSeries(SFChart chart, nint index)
		{
			if (index == 1)
			{
				SFSplineSeries series = new SFSplineSeries();
                series.Label = new NSString(StaticValues.ReportExpenseAverage);
                series.YAxis = getYAxis();
				return series;
			}
			else
			{
				SFColumnSeries series = new SFColumnSeries();
				series.Label = new NSString(StaticValues.ReportTopCategoriesLabel);
                series.YAxis = getYAxis();
				return series;
			}
		}

        SFRangeAxisBase getYAxis()
        {
            if (_axis == null)
            {
                _axis = new SFNumericalAxis();
                _axis.OpposedPosition = true;
                _axis.ShowMajorGridLines = false;
            }
            return _axis;
        }

        [Export("chart:dataPointAtIndex:forSeriesAtIndex:")]
		public override SFChartDataPoint GetDataPoint(SFChart chart, nint index, nint seriesIndex)
		{
			if (seriesIndex == 1)
			{
				return _expenseAverage.GetItem<SFChartDataPoint>((nuint)index);
			}
			else
			{
				return _category.GetItem<SFChartDataPoint>((nuint)index);
			}
		}

		[Export("chart:numberOfDataPointsForSeriesAtIndex:")]
		public override nint GetNumberOfDataPoints(SFChart chart, nint index)
		{
			return 5;
		}
    }
}