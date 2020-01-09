using System;
using System.Collections.Generic;
using System.Text;

namespace ChartPerformance
{
    public class Data
    {

        public double YValue
        {
            get;
            set;
        }

        public int XValue
        {
            get;
            set;
        }

        public string TooltipString
        {
            get
            {
                return "Index:" + XValue + ", Value: " + Math.Round(YValue, 2);
            }
        }
    }
}
