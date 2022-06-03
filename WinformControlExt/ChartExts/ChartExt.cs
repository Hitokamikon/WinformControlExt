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
    static public partial class ChartExt
    {
        static ToolTip toolTip = new ToolTip();

        static void HideToolTip(IWin32Window window)
        {
            if (toolTip.Tag != null)
            {
                if(toolTip.Tag is MarkPoint)
                {
                    MarkPoint markPoint = toolTip.Tag as MarkPoint;
                    markPoint?.Reset();
                }
            }
            toolTip.Tag = null;
            toolTip.Hide(window);
        }

        static Color GetRandomColor()
        {
            Random ran = new Random();

            int R = ran.Next(255);
            int G = ran.Next(255);
            int B = ran.Next(255);
            B = (R + G > 400) ? R + G - 400 : B;//0 : 380 - R - G;
            B = (B > 255) ? 255 : B;

            return Color.FromArgb(R - 2, G, B);
        }

        static RectangleF ChartAreaClientRectangle(this Chart chart, ChartArea chartArea)
        {
            RectangleF rectangle = chartArea.Position.ToRectangleF();
            float pw = chart.ClientSize.Width / 100f;
            float ph = chart.ClientSize.Height / 100f;
            return new RectangleF(pw * rectangle.X, ph * rectangle.Y,
                pw * rectangle.Width, ph * rectangle.Height);
        }

        static public bool GetCoordinateOfMousePositionInChartArea(this Chart chart , ChartArea chartArea , Point mousePosition , out PointF coordinate)
        {
            RectangleF rectangle = chart.ChartAreaClientRectangle(chartArea);
            if (rectangle.Contains(mousePosition))
            {

                coordinate = new PointF((float)chartArea.AxisX.PixelPositionToValue(mousePosition.X), (float)chartArea.AxisY.PixelPositionToValue(mousePosition.Y));
                return true;
            }
            coordinate = PointF.Empty;
            return false;
        }

        static public ChartArea GetChartArea(this Chart chart , Point mousePosition)
        {
            for (int i = 0; i < chart.ChartAreas.Count; i++)
            {
                ChartArea chartArea = chart.ChartAreas[i];
                RectangleF rectangleF = chart.ChartAreaClientRectangle(chartArea);
                if (rectangleF.Contains(mousePosition))
                {
                    return chartArea;
                }
            }
            return null;
        }
    }
#endif
}
