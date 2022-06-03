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
        static Action<object, MouseEventArgs> GetDoubleClickAction(Chart chart, ChartClickFunction chartFunction, params object[] paras)
        {
            return (object sender, MouseEventArgs args) =>
            {
                Action<Chart> action = null;
                switch (args.Button)
                {
                    case MouseButtons.Left:
                        action = GetChartClickAction(chartFunction, args, paras);
                        break;
                }
                if (action != null)
                    action(chart);
            };
        }

        static Action<object, MouseEventArgs> GetLeftClickAction(Chart chart, ChartClickFunction chartFunction, params object[] paras)
        {
            return (object sender, MouseEventArgs args) =>
            {
                Action<Chart> action = null;
                switch (args.Button)
                {
                    case MouseButtons.Left:
                        action = GetChartClickAction(chartFunction, args, paras);
                        break;
                }
                if (action != null)
                    action(chart);
            };
        }

        static Action<object, MouseEventArgs> GetRightClickAction(Chart chart, ChartClickFunction chartFunction, params object[] paras)
        {
            return (object sender, MouseEventArgs args) =>
            {
                Action<Chart> action = null;
                switch (args.Button)
                {
                    case MouseButtons.Right:
                        action = GetChartClickAction(chartFunction, args, paras);
                        break;
                }
                if (action != null)
                    action(chart);
            };
        }

        static Action<Chart> GetChartClickAction(ChartClickFunction chartFunction, MouseEventArgs args, params object[] objs)
        {
            switch (chartFunction)
            {
                case ChartClickFunction.One_Multi_Switch:
                    return (Chart chart) =>
                    {
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
                                chartArea.Visible = rectangleF.Contains(args.Location);
                            }
                        }
                    };

                case ChartClickFunction.ChangeChartAreaLineColor:
                    return (Chart chart) =>
                    {
                        ChartArea chartArea = chart.GetChartArea(args.Location);
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

                case ChartClickFunction.GetChartAreaInnerCoordinate:
                    if (objs.Length > 0)
                    {
                        Action<ChartArea, PointF> processAction = objs[0] as Action<ChartArea, PointF>;
                        if (processAction != null)
                        {
                            return (Chart chart) =>
                            {
                                PointF coordinate;
                                for (int i = 0; i < chart.ChartAreas.Count; i++)
                                {
                                    ChartArea chartArea = chart.ChartAreas[i];
                                    if (chart.GetCoordinateOfMousePositionInChartArea(chartArea, args.Location, out coordinate))
                                    {
                                        processAction(chartArea, coordinate);
                                        break;
                                    }
                                }
                            };
                        }
                    }
                    break;

                case ChartClickFunction.CopyPoints:
                    return (Chart chart) =>
                    {
                        ChartArea chartArea = chart.GetChartArea(args.Location);
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
            }
            return null;
        }

        static public void RegistClickEvent(this Chart chart, ControlClickEvent controlEvent, ChartClickFunction chartFunction, params object[] paras)
        {
            Action<object, MouseEventArgs> action;
            switch (controlEvent)
            {
                case ControlClickEvent.DoubleClick:
                    action = GetDoubleClickAction(chart, chartFunction, paras);
                    chart.MouseDoubleClick += new MouseEventHandler(action);
                    break;

                case ControlClickEvent.LeftClick:
                    action = GetLeftClickAction(chart, chartFunction, paras);
                    chart.MouseClick += new MouseEventHandler(action);
                    break;

                case ControlClickEvent.RightClick:
                    action = GetRightClickAction(chart, chartFunction, paras);
                    chart.MouseClick += new MouseEventHandler(action);
                    break;

            }
        }
    }
#endif
}
