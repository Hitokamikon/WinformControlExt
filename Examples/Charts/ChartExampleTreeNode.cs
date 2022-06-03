using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Charts
{
    internal class ChartExampleTreeNode : ExampleTreeNode
    {
        internal ChartExtExample chartExtExample { get { return example as  ChartExtExample; } }

        internal ChartExampleTreeNode(ChartExtExample chartExtExample)
            :base(chartExtExample)
        {
            string[] paths = chartExtExample.path.Split('/');
            Name = Text = paths[paths.Length - 1];
            ToolTipText = chartExtExample.description;
            ForeColor = Color.Red;
        }
    }
}
