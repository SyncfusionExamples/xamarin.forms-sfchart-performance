using System;
using System.Reflection;
using Android.Content;
using ChartPerformance.Droid;
using Syncfusion.SfChart.XForms;
using Syncfusion.SfChart.XForms.Droid;
using Xamarin.Forms;
using Native = Com.Syncfusion.Charts;

[assembly: ExportRenderer(typeof(ChartPerformance.ChartExt), typeof(ChartRendererExt))]
namespace ChartPerformance.Droid
{
    public class ChartRendererExt : SfChartRenderer
    {
        public override SfChartExt CreateNativeChart()
        {
            return new NativeChart(Context);
        }

    }

    public class NativeChart : SfChartExt
    {
        public NativeChart(Context context) : base(context)
        {
        }

        protected override Native.ChartSeries CreateNativeChartSeries(ChartSeries formSeries)
        {

            if (formSeries is FastLineSeries)
            {
                return new ChartFastLineSeriesExt() { PixelLength = 4 };
            }

            return base.CreateNativeChartSeries(formSeries);
        }
    }

    public class ChartFastLineSeriesExt : Native.FastLineSeries
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
            PropertyInfo prop = typeof(Native.FastLineSeries).GetProperty("ToleranceCoefficient", BindingFlags.NonPublic | BindingFlags.Instance);

            prop?.SetValue(this, count);
        }

    }
}
