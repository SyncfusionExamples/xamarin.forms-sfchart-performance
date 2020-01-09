using ChartPerformance.UWP;
using Syncfusion.SfChart.XForms;
using Syncfusion.SfChart.XForms.UWP;
using System;
using Xamarin.Forms.Platform.UWP;
using Native = Syncfusion.UI.Xaml.Charts;

[assembly: ExportRenderer(typeof(ChartPerformance.ChartExt), typeof(ChartRendererExt))]
namespace ChartPerformance.UWP
{
	public class ChartRendererExt : SfChartRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<SfChart> e)
		{
			base.OnElementChanged(e);

			if (Control is Native.SfChart)
			{
				var formsChart = Element as SfChart;

				for (int i = 0; i < formsChart.Series.Count; i++)
				{
					if (formsChart.Series[i] is FastLineSeries)
					{
						var formsSeries = formsChart.Series[i] as FastLineSeries;
						Control.Series.RemoveAt(i);

						Native.FastLineBitmapSeries series = new Native.FastLineBitmapSeries();

						var properties = SfChartRenderer.GetPropertiesChanged(typeof(ChartSeries), formsSeries);
						foreach (var name in properties)
						{
							ChartSeriesMapping.OnXyDataSeriesPropertiesChanged(name, formsSeries, series);
						}

						SfChartRenderer.SetNativeObject(typeof(ChartSeries), formsSeries, series);
						Control.Series.Insert(i, series);
					}
				}
			}
		}
	}
}
