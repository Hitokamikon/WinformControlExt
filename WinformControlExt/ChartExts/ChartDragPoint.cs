using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WinformControlExt.ControlEvents;

namespace WinformControlExt.ChartExts
{
    internal class ChartDragPoint
    {
        internal Point beginPoint { get; private set; }

        internal PointF coordinate { get; private set; }

        internal double xMin { get;private set; }
        internal double xMax { get; private set; }
        internal double yMin { get; private set; }
        internal double yMax { get; private set; }

        internal ChartArea chartArea { get; private set; }

        internal bool calculate { get; set; }

        internal double xRatio { get; set; }

        internal double yRatio { get; set; }

        internal ChartDragPoint(Point beginPoint , ChartArea chartArea , PointF coordinate)
        {
            this.beginPoint = beginPoint;
            this.chartArea = chartArea;
            xMin = chartArea.AxisX.Minimum;
            xMax = chartArea.AxisX.Maximum;
            yMin = chartArea.AxisY.Minimum;
            yMax = chartArea.AxisY.Maximum;
            this.coordinate = coordinate;
        }

    }
}
