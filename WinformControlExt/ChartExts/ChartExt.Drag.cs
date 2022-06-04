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
    public partial class ChartExt
    {
        static public void RegistDragMoveEvent(this Chart chart , int xDigit , int yDigit)
        {
            MouseEventHandler mouseEventHandler = (object sender, MouseEventArgs e) =>
            {
                PointF coordinate = PointF.Empty;
                for (int i = 0; i < chart.ChartAreas.Count; i++)
                {
                    ChartArea chartArea = chart.ChartAreas[i];
                    if(chart.GetCoordinateOfMousePositionInChartArea(chartArea, e.Location, out coordinate))
                    {
                        chart.DoDragDrop(new ChartDragPoint(e.Location, chartArea , coordinate), DragDropEffects.All);
                        return;
                    }
                }
            };
            chart.MouseDown += mouseEventHandler;

            DragEventHandler dragEventHandler = (object sender, DragEventArgs e) =>
            {
                PointF coordinate1 = PointF.Empty;
                PointF coordinate2 = PointF.Empty;

                ChartDragPoint chartDragPoint = (ChartDragPoint)e.Data.GetData(typeof(ChartDragPoint));
                if (chartDragPoint == null) return;
                coordinate1 = chartDragPoint.coordinate;

                Point mousePoint2 = new Point(e.X, e.Y);
                mousePoint2 = chart.PointToClient(mousePoint2);
                if (mousePoint2.X == chartDragPoint.beginPoint.X) mousePoint2 = new Point((int)mousePoint2.X + 1, (int)mousePoint2.Y);
                if (mousePoint2.Y == chartDragPoint.beginPoint.Y) mousePoint2 = new Point((int)mousePoint2.X, (int)mousePoint2.Y + 1);

                if (chart.GetCoordinateOfMousePositionInChartArea(chartDragPoint.chartArea, mousePoint2, out coordinate2))
                {
                    if (!chartDragPoint.calculate)
                    {
                        chartDragPoint.xRatio = (coordinate2.X - coordinate1.X) / (mousePoint2.X - chartDragPoint.beginPoint.X);
                        chartDragPoint.yRatio = (coordinate2.Y - coordinate1.Y) / (mousePoint2.Y - chartDragPoint.beginPoint.Y);
                        chartDragPoint.calculate = true;
                    }
                    double deltaX = (mousePoint2.X - chartDragPoint.beginPoint.X) * chartDragPoint.xRatio;
                    double deltaY = (mousePoint2.Y - chartDragPoint.beginPoint.Y) * chartDragPoint.yRatio;

                    chartDragPoint.chartArea.AxisX.Maximum = Math.Round(chartDragPoint.xMax - deltaX, xDigit);
                    chartDragPoint.chartArea.AxisX.Minimum = Math.Round(chartDragPoint.xMin - deltaX, xDigit);
                    chartDragPoint.chartArea.AxisY.Maximum = Math.Round(chartDragPoint.yMax - deltaY, yDigit);
                    chartDragPoint.chartArea.AxisY.Minimum = Math.Round(chartDragPoint.yMin - deltaY, yDigit);
                }
            };
            chart.AllowDrop = true;
            chart.DragOver += dragEventHandler;
            //chart.DragDrop += dragEventHandler;
        }
    }
}
