using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Examples
{
    internal class ChartExtExample : Example
    {
        internal Action<Chart> processChart { get; private set; }

        internal ChartExtExample(string path, string code , Action<Chart> processChart)
            :base(path, code)
        {
            this.processChart = processChart;
        }
    }
}
