using System;
using Foundation;
using Syncfusion.SfChart.iOS;

namespace ExpenseManager.ios.ListSources
{
    public class ReportChartDataSource : SFChartDataSource
    {
		NSMutableArray highTemperature;
		NSMutableArray lowTemperature;
		NSMutableArray precipitation;

		public ReportChartDataSource()
		{
			highTemperature = new NSMutableArray();
			lowTemperature = new NSMutableArray();
			precipitation = new NSMutableArray();

			AddDataPointsForChart("Jan", 42, 27, 3.03);
			AddDataPointsForChart("Feb", 44, 28, 2.48);
			AddDataPointsForChart("Mar", 53, 35, 3.23);
			AddDataPointsForChart("Apr", 64, 44, 3.15);
			AddDataPointsForChart("May", 75, 54, 4.13);
			AddDataPointsForChart("Jun", 83, 63, 3.23);
			AddDataPointsForChart("Jul", 87, 68, 4.13);
			AddDataPointsForChart("Aug", 84, 66, 4.88);
			AddDataPointsForChart("Sep", 78, 59, 3.82);
			AddDataPointsForChart("Oct", 67, 48, 3.07);
			AddDataPointsForChart("Nov", 55, 38, 2.83);
			AddDataPointsForChart("Dec", 45, 29, 2.80);
		}

		/// <summary>
		/// Method to populate the array required for all the series.
		/// </summary>
		/// <param name="month">Month.</param>
		/// <param name="high">High Temperature.</param>
		/// <param name="low">Low Temperature.</param>
		/// <param name="ppt">Precipitation.</param>
		void AddDataPointsForChart(String month, Double high, Double low, Double ppt)
		{
			highTemperature.Add(new SFChartDataPoint(NSObject.FromObject(month), NSObject.FromObject(high)));

			lowTemperature.Add(new SFChartDataPoint(NSObject.FromObject(month), NSObject.FromObject(low)));

			precipitation.Add(new SFChartDataPoint(NSObject.FromObject(month), NSObject.FromObject(ppt)));
		}

		[Export("numberOfSeriesInChart:")]
		public override nint NumberOfSeriesInChart(SFChart chart)
		{
			return 3;
			//returns no of series required for the chart.
		}

		[Export("chart:seriesAtIndex:")]
		public override SFSeries GetSeries(SFChart chart, nint index)
		{
			//returns the series for the chart.
			if (index == 1)
			{
				SFSplineSeries series = new SFSplineSeries();
				series.Label = new NSString("High");
				return series;
			}
			else if (index == 2)
			{
				SFSplineSeries series = new SFSplineSeries();
				series.Label = new NSString("Low");
				return series;
			}
			else
			{
				SFColumnSeries series = new SFColumnSeries();
				series.Label = new NSString("Precipitation");

				SFNumericalAxis axis = new SFNumericalAxis();
				axis.OpposedPosition = true;
				axis.ShowMajorGridLines = false;
				series.YAxis = axis;

				return series;
			}
		}

		[Export("chart:dataPointAtIndex:forSeriesAtIndex:")]
		public override SFChartDataPoint GetDataPoint(SFChart chart, nint index, nint seriesIndex)
		{
			//returns the datapoint for each series.
			if (seriesIndex == 1)
			{
				return highTemperature.GetItem<SFChartDataPoint>((nuint)index);
			}
			else if (seriesIndex == 2)
			{
				return lowTemperature.GetItem<SFChartDataPoint>((nuint)index);
			}
			else
			{
				return precipitation.GetItem<SFChartDataPoint>((nuint)index);
			}
		}

		[Export("chart:numberOfDataPointsForSeriesAtIndex:")]
		public override nint GetNumberOfDataPoints(SFChart chart, nint index)
		{
			return 12;
			//No of datapoints needed for each series.
		}
    }
}