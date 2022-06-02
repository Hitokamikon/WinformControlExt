using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examples
{
    public partial class ChartExtExampleForm : Form
    {
        public ChartExtExampleForm()
        {
            InitializeComponent();
        }

        public void AddChartExample(ChartExtExample chartExtExample)
        {
            treeView1.Nodes.Add(chartExtExample.path);
        }
    }
}
