using System;
using Syncfusion.SfChart.XForms;

namespace ChartPerformance
{
    public class ChartExt : SfChart
    {

    }

    public class CustomFastLineSeries : FastLineSeries
    {

        public int PixelCount { get; set; }

    }
}
