using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WinformControlExt.ChartExts;
using WinformControlExt.ControlEvents;

namespace Examples.Charts
{
    internal partial class ChartExtExampleForm : Form
    {
        internal Chart showingChart { get; private set; }

        internal ChartExtExampleForm()
        {
            InitializeComponent();
            Action<ChartArea, PointF> printCoordinate = (ChartArea chartArea, PointF point) =>
            {
                MessageBox.Show(string.Format("在ChartArea[{0}]中，点击了坐标{1}", chartArea.Name, point));
            };


            AddChartExample(new ChartExtExample("双击事件/切换显示", 
                new List<string>(new string[] { "chart.RegistEvent(ControlEvent.DoubleClick , ChartFunction.One_Multi_Switch);" }) ,
                "鼠标在一个ChartArea上双击可以切换[1个/多个]ChartArea显示", 4, 
                (Chart chart) => { chart.RegistEvent(ControlEvent.DoubleClick, ChartFunction.One_Multi_Switch); }));

            AddChartExample(new ChartExtExample("双击事件/改变线条颜色",
                new List<string>(new string[] { "chart.RegistEvent(ControlEvent.DoubleClick , ChartFunction.ChangeChartAreaLineColor);" }),
                "鼠标在一个ChartArea上双击可以改变该ChartArea的线条颜色", 4,
                (Chart chart) => { chart.RegistEvent(ControlEvent.DoubleClick, ChartFunction.ChangeChartAreaLineColor); }));

            AddChartExample(new ChartExtExample("双击事件/获取坐标",
                new List<string>(new string[] { "Action<ChartArea, PointF> printCoordinate = (ChartArea chartArea, PointF point) =>", 
                    "{" ,
                    "\t//获取坐标后的处理方法" ,
                    "}" ,
                    "chart.RegistEvent(ControlEvent.DoubleClick , ChartFunction.GetChartAreaInnerCoordinate , printCoordinate);" }),
                "鼠标在一个ChartArea上双击可以改变该ChartArea的线条颜色", 4,
                (Chart chart) => { chart.RegistEvent(ControlEvent.DoubleClick, ChartFunction.GetChartAreaInnerCoordinate , printCoordinate); }));
        }

        internal void AddChartExample(ChartExtExample chartExtExample)
        {
            ChartExampleTreeNode chartExampleTreeNode = new ChartExampleTreeNode(chartExtExample);
            List<string> pathList = new List<string>(chartExtExample.path.Split(new char[] {'/' }));
            AddNode(pathList, chartExampleTreeNode);
        }

        void AddNode(List<string> pathList , ChartExampleTreeNode chartExampleTreeNode)
        {
            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                TreeNode node = treeView1.Nodes[i];
                if (node.Text == pathList[0])
                {
                    AddNode(node, pathList, 1, chartExampleTreeNode);
                    return;
                }
            }

            TreeNode newNode = new TreeNode(pathList[0]);
            treeView1.Nodes.Add(newNode);
            newNode.Expand();
            AddNode(newNode, pathList, 1, chartExampleTreeNode);
        }

        void AddNode(TreeNode parentTreeNode , List<string> pathList , int index , ChartExampleTreeNode chartExampleTreeNode)
        {
            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                TreeNode node = treeView1.Nodes[i];
                if (node.Text == pathList[index])
                {
                    if (index == pathList.Count - 1)
                    {
                        node.Nodes.Add(chartExampleTreeNode);
                        chartExampleTreeNode.Expand();
                    }
                    else
                        AddNode(node, pathList, index + 1, chartExampleTreeNode);
                    return;
                }
            }

            if (index == pathList.Count - 1)
            {
                parentTreeNode.Nodes.Add(chartExampleTreeNode);
            }
            else
            {
                TreeNode newNode = new TreeNode(pathList[index]);
                newNode.Expand();
                parentTreeNode.Nodes.Add(newNode);
                AddNode(newNode, pathList, index + 1, chartExampleTreeNode);
            }
        }

        internal void ShowChartExample(ChartExtExample chartExtExample)
        {
            if (showingChart != null) showingChart.Dispose();
            showingChart = chartExtExample.CreateChart();
            chartPanel.Controls.Add(showingChart);
            showingChart.Dock = DockStyle.Fill;

            chartExtExample.SetFullCodeToRichTextBox(richTextBox1);

            descriptionToolStripStatusLabel.Text = chartExtExample.description;
            正弦波();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ChartExampleTreeNode chartExampleTreeNode = e.Node as ChartExampleTreeNode;
            if(chartExampleTreeNode != null)
            {
                ShowChartExample(chartExampleTreeNode.chartExtExample);
            }
        }

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
                        series.Points.Add(Math.Sin(j * 0.1));
                    }
                }
            }
        }
    }
}
