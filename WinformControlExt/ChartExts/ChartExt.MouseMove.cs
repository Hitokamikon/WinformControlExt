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

        static public void RegistMouseMovePointToolTipEvent(this Chart chart, Color markerColor , int markerSize , MarkerStyle markerStyle)
        {
            MouseEventHandler mouseEventHandler = (object sender, MouseEventArgs e) =>
            {
                ChartArea chartArea = chart.GetChartArea(e.Location);
                if (chartArea != null)
                {
                    PointF coordinate = PointF.Empty;
                    if (chart.GetCoordinateOfMousePositionInChartArea(chartArea, e.Location, out coordinate))
                    {
                        DataPoint nearestPoint = null;
                        Series nearestSeries = null;
                        double minDistance = double.MaxValue;
                        for (int i = 0; i < chart.Series.Count; i++)
                        {
                            Series series = chart.Series[i];
                            if (series.ChartArea == chartArea.Name)
                            {
                                for (int j = 0; j < series.Points.Count; j++)
                                {
                                    DataPoint point = series.Points[j];
                                    double x = point.XValue - coordinate.X;
                                    double y = point.YValues[0] - coordinate.Y;
                                    double distance = Math.Sqrt(x * x + y * y);
                                    if (distance < minDistance)
                                    {
                                        minDistance = distance;
                                        nearestPoint = point;
                                        nearestSeries = series;
                                    }
                                }
                            }
                        }
                        if (nearestPoint != null)
                        {
                            if (toolTip.Tag != null && toolTip.Tag is MarkPoint && ((MarkPoint)toolTip.Tag).dataPoint == nearestPoint)
                            {
                            }
                            else
                            {
                                HideToolTip(chart);
                                MarkPoint markPoint = new MarkPoint(nearestSeries, nearestPoint);
                                nearestPoint.MarkerColor = markerColor;
                                nearestPoint.MarkerSize = markerSize;
                                nearestPoint.MarkerStyle = markerStyle;
                                toolTip.Tag = markPoint;
                                toolTip.Show(nearestPoint.ToString(), chart, e.Location);
                            }
                            return;
                        }
                    }
                }
                HideToolTip(chart);
            };
            chart.MouseMove += mouseEventHandler;
        }

        static public void RegistMouseMoveProcessPointEvent(this Chart chart , Action<Series, DataPoint> process)
        {
            MouseEventHandler mouseEventHandler = (object sender, MouseEventArgs e) =>
            {
                ChartArea chartArea = chart.GetChartArea(e.Location);
                if (chartArea != null)
                {
                    PointF coordinate = PointF.Empty;
                    if (chart.GetCoordinateOfMousePositionInChartArea(chartArea, e.Location, out coordinate))
                    {
                        DataPoint nearestPoint = null;
                        Series nearestSeries = null;
                        double minDistance = double.MaxValue;
                        for (int i = 0; i < chart.Series.Count; i++)
                        {
                            Series series = chart.Series[i];
                            if (series.ChartArea == chartArea.Name)
                            {
                                for (int j = 0; j < series.Points.Count; j++)
                                {
                                    DataPoint point = series.Points[j];
                                    double x = point.XValue - coordinate.X;
                                    double y = point.YValues[0] - coordinate.Y;
                                    double distance = Math.Sqrt(x * x + y * y);
                                    if (distance < minDistance)
                                    {
                                        minDistance = distance;
                                        nearestPoint = point;
                                        nearestSeries = series;
                                    }
                                }
                            }
                        }
                        if (nearestPoint != null)
                        {
                            process(nearestSeries, nearestPoint);
                            return;
                        }
                    }
                }
            };
            chart.MouseMove += mouseEventHandler;
        }

    }
#endif
}
