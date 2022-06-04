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
#if ComplieChart
    public partial class ChartExt
    {
        static public void RegistMouseWheelScaleEvent(this Chart chart, float velocity ,
            float xSizeMin , float ySizeMin , int xDigits , int yDigits)
        {
            MouseEventHandler mouseEventHandler = (object sender, MouseEventArgs args) =>
            {
                PointF coordinate = PointF.Empty;
                for (int i = 0; i < chart.ChartAreas.Count; i++)
                {
                    ChartArea chartArea = chart.ChartAreas[i];
                    if (chart.GetCoordinateOfMousePositionInChartArea(chartArea, args.Location, out coordinate))
                    {
                        double xSize = chartArea.AxisX.Maximum - chartArea.AxisX.Minimum;
                        double ySize = chartArea.AxisY.Maximum - chartArea.AxisY.Minimum;
                        double scale = 1 - velocity * args.Delta / 1000f;
                        double scaleMin = xSizeMin / xSize;
                        if (scale < scaleMin) scale = scaleMin;
                        scaleMin = ySizeMin / ySize;
                        if (scale < scaleMin) scale = scaleMin;

                        double xMaxD = chartArea.AxisX.Maximum - coordinate.X;
                        double xMinD = coordinate.X - chartArea.AxisX.Minimum;
                        chartArea.AxisX.Maximum = Math.Round(coordinate.X + xMaxD * scale, xDigits);
                        chartArea.AxisX.Minimum = Math.Round(coordinate.X - xMinD * scale, xDigits);

                        double yMaxD = chartArea.AxisY.Maximum - coordinate.Y;
                        double yMinD = coordinate.Y - chartArea.AxisY.Minimum;
                        chartArea.AxisY.Maximum = Math.Round(coordinate.Y + yMaxD * scale, yDigits);
                        chartArea.AxisY.Minimum = Math.Round(coordinate.Y - yMinD * scale, yDigits);

                        return;
                    }
                }
            };
            chart.MouseWheel += mouseEventHandler;
        }


    }
#endif
}
