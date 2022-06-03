using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinformControlExt.ChartExts
{
#if ComplieChart
    internal class MarkPoint
    {
        internal Series series { get; private set; }

        internal DataPoint dataPoint { get; private set; }

        internal MarkPoint(Series series , DataPoint dataPoint)
        {
            this.series = series;
            this.dataPoint = dataPoint;
        }

        internal void Reset()
        {
            dataPoint.MarkerColor = series.Color;
            dataPoint.MarkerSize = series.MarkerSize;
            dataPoint.MarkerStyle = series.MarkerStyle;
        }
    }
#endif
}
