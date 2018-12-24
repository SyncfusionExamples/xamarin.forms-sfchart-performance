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

            Native.SfChart chart = this.Control as Native.SfChart;
            chart.Series.Clear();

            //Replace the fast line series with fast line bitmap series
            foreach (var formsSeries in e.NewElement.Series)
            {
                if (formsSeries is FastLineSeries)
                {
                    Native.FastLineBitmapSeries fastLine = new Native.FastLineBitmapSeries();

                    formsSeries.PropertyChanged += (sender, a) =>
                    {
                        if(a.PropertyName.Contains("ItemsSource"))
                        {
                            fastLine.ItemsSource = formsSeries.ItemsSource;
                        }
                    };
                    
                    var properties = SfChartRenderer.GetPropertiesChanged(typeof(ChartSeries), formsSeries);

                    foreach (var name in properties)
                    {
                        ChartSeriesMapping.OnXyDataSeriesPropertiesChanged(name, formsSeries as FastLineSeries,
                            fastLine);
                    }
                    chart.Series.Add(fastLine);
                }
            }
        }
    }
}
