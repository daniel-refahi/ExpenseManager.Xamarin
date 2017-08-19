using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManager.ios.Utilities;
using ExpenseManager.Repository;
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
        int numberOfDataPoints;

        public ReportChartDataSource(List<Category> topCategories, double totalExpensesAverage)
		{
			_expenseAverage = new NSMutableArray();
			_category = new NSMutableArray();
            numberOfDataPoints = topCategories.Count;


            if (topCategories.Count > 0)
			    AddDataPointsForChart(topCategories[0].Name, totalExpensesAverage, topCategories[0].GetExpenses().Sum(e => e.Value));
            if (topCategories.Count > 1)
			    AddDataPointsForChart(topCategories[1].Name, totalExpensesAverage, topCategories[1].GetExpenses().Sum(e => e.Value));
            if (topCategories.Count > 2)
			    AddDataPointsForChart(topCategories[2].Name, totalExpensesAverage, topCategories[2].GetExpenses().Sum(e => e.Value));
            if (topCategories.Count > 3)
			    AddDataPointsForChart(topCategories[3].Name, totalExpensesAverage, topCategories[3].GetExpenses().Sum(e => e.Value));
            if (topCategories.Count > 4)
			    AddDataPointsForChart(topCategories[4].Name, totalExpensesAverage, topCategories[4].GetExpenses().Sum(e => e.Value));
            
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
			return _category.GetItem<SFChartDataPoint>((nuint)index);
		}

		[Export("chart:numberOfDataPointsForSeriesAtIndex:")]
		public override nint GetNumberOfDataPoints(SFChart chart, nint index)
		{
            return numberOfDataPoints;
		}
    }
}