using ChartPerformance.WPF;
using Syncfusion.SfChart.XForms.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms.Platform.WPF;
using Native = Syncfusion.UI.Xaml.Charts;

[assembly: ExportRenderer(typeof(ChartPerformance.ChartExt), typeof(ChartRendererExt))]
namespace ChartPerformance.WPF
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
