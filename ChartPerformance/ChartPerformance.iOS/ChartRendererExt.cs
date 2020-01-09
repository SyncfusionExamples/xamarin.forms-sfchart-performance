using System;
using ChartPerformance.iOS;
using Native = Syncfusion.SfChart.iOS;
using Syncfusion.SfChart.XForms;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using Xamarin.Forms;
using System.Reflection;

[assembly: ExportRenderer(typeof(ChartPerformance.ChartExt), typeof(ChartRendererExt))]
namespace ChartPerformance.iOS
{
    public class ChartRendererExt : SfChartRenderer
    {
        public override Native.SFChart CreateNativeChart()
        {
            return new NativeChart();
        }
    }

    public class NativeChart : Native.SFChart
    {
        protected override Native.SFSeries CreateNativeChartSeries(ChartSeries formSeries)
        {

            if (formSeries is FastLineSeries)
            {
                return new SFFastLineSeriesExt() { PixelLength = 4 };
            }

            return base.CreateNativeChartSeries(formSeries);
        }
    }

    public class SFFastLineSeriesExt : Native.SFFastLineSeries
    {
        int pixelLength;

        public int PixelLength
        {
            get
            {
                return pixelLength;
            }
            set
            {
                pixelLength = value;
                SetPixelCount(value);
            }
        }

        void SetPixelCount(int count)
        {
            PropertyInfo prop = typeof(Native.SFSeries).GetProperty("PixelCount", BindingFlags.NonPublic | BindingFlags.Instance);

            prop.SetValue(this, count);
        }
    }
}
