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

        static Action<object, MouseEventArgs> GetDoubleClickAction(Chart chart, ChartFunction chartFunction)
        {
            switch (chartFunction)
            {
                case ChartFunction.One_Multi_Switch:
                    return (object sender, MouseEventArgs args) =>
                    {
                        switch (args.Button)
                        {
                            case MouseButtons.Left:
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
                                break;
                        }

                    };
            }
            return null;
        }

        static public void RegistEvent(this Chart chart , ControlEvent controlEvent , ChartFunction chartFunction , params object[] paras)
        {
            switch (controlEvent)
            {
                case ControlEvent.DoubleClick:
                    Action<object, MouseEventArgs> action = GetDoubleClickAction(chart, chartFunction);
                    //doubleClickDic[chart] = action;
                    chart.MouseDoubleClick += new MouseEventHandler(action);
                    break;
            }
        }

        static RectangleF ChartAreaClientRectangle(Chart chart, ChartArea chartArea)
        {
            RectangleF rectangle = chartArea.Position.ToRectangleF();
            float pw = chart.ClientSize.Width / 100f;
            float ph = chart.ClientSize.Height / 100f;
            return new RectangleF(pw * rectangle.X, ph * rectangle.Y,
                pw * rectangle.Width, ph * rectangle.Height);
        }
    }
}
