using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Examples.Charts
{
    internal partial class ChartExtExampleForm
    {

        private void 正弦波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            正弦波();
        }

        void 正弦波()
        {
            for (int i = 0; i < showingChart.Series.Count; i++)
            {
                Series series = showingChart.Series[i];
                if (series != null)
                {
                    series.Points.Clear();
                    for (int j = 0; j < 100; j++)
                    {
                        series.Points.AddXY(j , Math.Sin(j * 0.1));
                    }
                }
            }
        }
    }
}
