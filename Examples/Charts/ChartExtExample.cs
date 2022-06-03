using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Examples.Charts
{
    internal class ChartExtExample : Example
    {
        internal Action<Chart> processChart { get; private set; }

        internal int chartAreaNumber { get; private set; }

        internal ChartExtExample(string path, List<string> codeList, string description , int chartAreaNumber, Action<Chart> processChart)
            : base(path, new List<string>(new string[] {
                "System.Windows.Forms" ,
                "WinformControlExt.ChartExts", 
                "WinformControlExt.ControlEvents"
            }), codeList , description)
        {
            this.chartAreaNumber = chartAreaNumber;
            this.processChart = processChart;
        }

        internal ChartExtExample(string path, List<string> namespaceList , List<string> codeList, string description, int chartAreaNumber ,  Action<Chart> processChart)
            :base(path, namespaceList , codeList , description)
        {
            this.chartAreaNumber = chartAreaNumber;
            this.processChart = processChart;
        }

        internal void SetFullCodeToRichTextBox(RichTextBox richTextBox)
        {
            richTextBox.Clear();
            for (int i = 0; i < namespaceList.Count; i++)
            {
                richTextBox.SelectionColor = Color.CornflowerBlue;
                richTextBox.SelectedText = "using ";

                richTextBox.SelectionColor = Color.White;
                richTextBox.SelectedText = namespaceList[i] + ";\r\n";
            }
            richTextBox.SelectedText = "\r\n";
            richTextBox.SelectionColor = Color.CornflowerBlue;
            richTextBox.SelectedText = "public partial class ";
            richTextBox.SelectionColor = Color.DeepSkyBlue;
            richTextBox.SelectedText = "ExampleForm";
            richTextBox.SelectionColor = Color.White;
            richTextBox.SelectedText = " : ";
            richTextBox.SelectionColor = Color.DeepSkyBlue;
            richTextBox.SelectedText = "Form\r\n";

            richTextBox.SelectionColor = Color.White;
            richTextBox.SelectedText = "{\r\n";

            richTextBox.SelectionColor = Color.DeepSkyBlue;
            richTextBox.SelectedText = "\tvoid ";
            richTextBox.SelectionColor = Color.YellowGreen;
            richTextBox.SelectedText = "Set";
            richTextBox.SelectionColor = Color.White;
            richTextBox.SelectedText = "(";
            richTextBox.SelectionColor = Color.DeepSkyBlue;
            richTextBox.SelectedText = "Chart ";
            richTextBox.SelectionColor = Color.GreenYellow;
            richTextBox.SelectedText = "chart";
            richTextBox.SelectionColor = Color.White;
            richTextBox.SelectedText = ")\r\n\t{\r\n";

            for (int i = 0; i < codeList.Count; i++)
            {
                string code = codeList[i];
                richTextBox.SelectionColor = Color.Yellow;
                richTextBox.SelectedText = "\t\t" + code + "\r\n";
            }
            richTextBox.SelectionColor = Color.White;
            richTextBox.SelectedText = "\t}";

            richTextBox.SelectionColor = Color.White;
            richTextBox.SelectedText = "\r\n}\r\n";

        }

        internal Chart CreateChart()
        {
            Chart chart = new Chart();
            Legend legend = new Legend("legend");
            chart.Legends.Add(legend);

            for(int i = 0; i < chartAreaNumber; i++)
            {
                string chartAreaName = string.Format("Chart Area {0}", i);
                ChartArea chartArea = new ChartArea(chartAreaName);
                chart.ChartAreas.Add(chartArea);

                string seriesName = string.Format("Series {0}", i);
                Series series = new Series(seriesName);
                series.ChartType = SeriesChartType.Line;
                series.ChartArea = chartAreaName;
                series.Legend = "legend";

                chart.Series.Add(series);
            }
            processChart(chart);
            return chart;
        }
    }
}
