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
        static public void RegistClickOne_Multi_SwitchEvent(this Chart chart, ControlClickEvent controlClickEvent)
        {
            MouseEventHandler mouseEventHandler = (object sender, MouseEventArgs e) =>
            {
                switch (controlClickEvent)
                {
                    case ControlClickEvent.LeftClick:
                        if (e.Button != MouseButtons.Left) return;
                        break;

                    case ControlClickEvent.RightClick:
                        if (e.Button != MouseButtons.Right) return;
                        break;
                }

                int visibleAreaCount = chart.ChartAreas.Where(area => area.Visible).Count();
                if (visibleAreaCount == 1)
                {
                    for (int i = 0; i < chart.ChartAreas.Count; i++)
                    {
                        ChartArea chartArea = chart.ChartAreas[i];
                        chartArea.Visible = true;
                    }
                }
                else
                {
                    for (int i = 0; i < chart.ChartAreas.Count; i++)
                    {
                        ChartArea chartArea = chart.ChartAreas[i];
                        RectangleF rectangleF = chart.ChartAreaClientRectangle(chartArea);
                        chartArea.Visible = rectangleF.Contains(e.Location);
                    }
                }
            };

            switch (controlClickEvent)
            {
                case ControlClickEvent.DoubleClick:
                    chart.MouseDoubleClick += mouseEventHandler;
                    break;

                case ControlClickEvent.LeftClick:
                case ControlClickEvent.RightClick:
                    chart.MouseClick += mouseEventHandler;
                    break;
            }
        }

        static public void RegistClickChangeChartAreaLineColorEvent(this Chart chart , ControlClickEvent controlClickEvent)
        {
            MouseEventHandler mouseEventHandler = (object sender, MouseEventArgs e) =>
            {
                switch (controlClickEvent)
                {
                    case ControlClickEvent.LeftClick:
                        if (e.Button != MouseButtons.Left) return;
                        break;

                    case ControlClickEvent.RightClick:
                        if (e.Button != MouseButtons.Right) return;
                        break;
                }
                ChartArea chartArea = chart.GetChartArea(e.Location);
                if (chartArea != null)
                {
                    for (int j = 0; j < chart.Series.Count; j++)
                    {
                        Series series = chart.Series[j];
                        if (chartArea.Name == series.ChartArea)
                        {
                            series.Color = GetRandomColor();
                            break;
                        }
                    }
                }
            };

            switch (controlClickEvent)
            {
                case ControlClickEvent.DoubleClick:
                    chart.MouseDoubleClick += mouseEventHandler;
                    break;

                case ControlClickEvent.LeftClick:
                case ControlClickEvent.RightClick:
                    chart.MouseClick += mouseEventHandler;
                    break;
            }
        }

        static public void RegistClickGetChartAreaInnerCoordinateEvent(this Chart chart, ControlClickEvent controlClickEvent , Action<ChartArea, PointF> processAction)
        {
            MouseEventHandler mouseEventHandler = (object sender, MouseEventArgs e) =>
            {
                switch (controlClickEvent)
                {
                    case ControlClickEvent.LeftClick:
                        if (e.Button != MouseButtons.Left) return;
                        break;

                    case ControlClickEvent.RightClick:
                        if (e.Button != MouseButtons.Right) return;
                        break;
                }

                PointF coordinate;
                for (int i = 0; i < chart.ChartAreas.Count; i++)
                {
                    ChartArea chartArea = chart.ChartAreas[i];
                    if (chart.GetCoordinateOfMousePositionInChartArea(chartArea, e.Location, out coordinate))
                    {
                        processAction(chartArea, coordinate);
                        break;
                    }
                }

            };

            switch (controlClickEvent)
            {
                case ControlClickEvent.DoubleClick:
                    chart.MouseDoubleClick += mouseEventHandler;
                    break;

                case ControlClickEvent.LeftClick:
                case ControlClickEvent.RightClick:
                    chart.MouseClick += mouseEventHandler;
                    break;
            }
        }

        static public void RegistClickCopyPointsEvent(this Chart chart, ControlClickEvent controlClickEvent)
        {
            MouseEventHandler mouseEventHandler = (object sender, MouseEventArgs e) =>
            {
                switch (controlClickEvent)
                {
                    case ControlClickEvent.LeftClick:
                        if (e.Button != MouseButtons.Left) return;
                        break;

                    case ControlClickEvent.RightClick:
                        if (e.Button != MouseButtons.Right) return;
                        break;
                }
                ChartArea chartArea = chart.GetChartArea(e.Location);
                if (chartArea != null)
                {
                    StringBuilder copyText = new StringBuilder();
                    for (int j = 0; j < chart.Series.Count; j++)
                    {
                        Series series = chart.Series[j];
                        if (chartArea.Name == series.ChartArea)
                        {
                            copyText.Append("Series:");
                            copyText.Append(series.Name + "\r\n");
                            for (int k = 0; k < series.Points.Count; k++)
                            {
                                DataPoint point = series.Points[k];
                                copyText.Append(point.ToString() + "\r\n");
                            }
                            copyText.Append("\r\n");
                        }
                    }
                    Clipboard.SetText(copyText.ToString());
                }

            };

            switch (controlClickEvent)
            {
                case ControlClickEvent.DoubleClick:
                    chart.MouseDoubleClick += mouseEventHandler;
                    break;

                case ControlClickEvent.LeftClick:
                case ControlClickEvent.RightClick:
                    chart.MouseClick += mouseEventHandler;
                    break;
            }
        }
    }
#endif
}
