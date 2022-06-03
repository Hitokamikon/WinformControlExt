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
    static public class ChartExt
    {
        static Dictionary<Chart, Action<object, MouseEventArgs>> doubleClickDic = new Dictionary<Chart, Action<object, MouseEventArgs>>();

        static Dictionary<Chart, Action<object, MouseEventArgs>> mouseWheelDic = new Dictionary<Chart, Action<object, MouseEventArgs>>();

        static Action<object, MouseEventArgs> GetDoubleClickAction(Chart chart, ChartFunction chartFunction, params object[] paras)
        {
            return (object sender, MouseEventArgs args) =>
            {
                Action<Chart> action = null;
                switch (args.Button)
                {
                    case MouseButtons.Left:
                        action = GetChartAction(chartFunction, args , paras);
                        break;
                }
                if (action != null)
                    action(chart);
            };
        }

        static Action<Chart> GetChartAction(ChartFunction chartFunction , MouseEventArgs args , params object[] objs)
        {
            switch (chartFunction)
            {
                case ChartFunction.One_Multi_Switch:
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
                                RectangleF rectangleF = ChartAreaClientRectangle(chart, chartArea);
                                chartArea.Visible = rectangleF.Contains(args.Location);
                            }
                        }
                    };

                case ChartFunction.ChangeChartAreaLineColor:
                    return (Chart chart) =>
                    {
                        for (int i = 0; i < chart.ChartAreas.Count; i++)
                        {
                            ChartArea chartArea = chart.ChartAreas[i];
                            RectangleF rectangleF = ChartAreaClientRectangle(chart, chartArea);
                            if(rectangleF.Contains(args.Location))
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
                                break;
                            }
                        }
                    };

                case ChartFunction.GetChartAreaInnerCoordinate:
                    if(objs.Length > 0)
                    {
                        Action<ChartArea , PointF> processAction = objs[0] as Action<ChartArea , PointF>;
                        if(processAction != null)
                        {
                            return (Chart chart) =>
                            {
                                PointF coordinate;
                                for (int i = 0; i < chart.ChartAreas.Count; i++)
                                {
                                    ChartArea chartArea = chart.ChartAreas[i];
                                    if(GetCoordinateOfMousePositionInChartArea(chart , chartArea , args.Location , out coordinate))
                                    {
                                        processAction(chartArea, coordinate);
                                        break;
                                    }
                                }
                            };
                        }
                    }
                    break;
            }
            return null;
        }

        static public void RegistEvent(this Chart chart , ControlEvent controlEvent , ChartFunction chartFunction , params object[] paras)
        {
            switch (controlEvent)
            {
                case ControlEvent.DoubleClick:
                    Action<object, MouseEventArgs> action = GetDoubleClickAction(chart, chartFunction, paras);
                    chart.MouseDoubleClick += new MouseEventHandler(action);
                    break;
            }
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

        static RectangleF ChartAreaClientRectangle(Chart chart, ChartArea chartArea)
        {
            RectangleF rectangle = chartArea.Position.ToRectangleF();
            float pw = chart.ClientSize.Width / 100f;
            float ph = chart.ClientSize.Height / 100f;
            return new RectangleF(pw * rectangle.X, ph * rectangle.Y,
                pw * rectangle.Width, ph * rectangle.Height);
        }

        static bool GetCoordinateOfMousePositionInChartArea(Chart chart , ChartArea chartArea , Point mousePosition , out PointF coordinate)
        {
            RectangleF rectangle = ChartAreaClientRectangle(chart, chartArea);
            if (rectangle.Contains(mousePosition))
            {

                coordinate = new PointF((float)chartArea.AxisX.PixelPositionToValue(mousePosition.X), (float)chartArea.AxisY.PixelPositionToValue(mousePosition.Y));
                return true;
            }
            coordinate = PointF.Empty;
            return false;
        }
    }
}
