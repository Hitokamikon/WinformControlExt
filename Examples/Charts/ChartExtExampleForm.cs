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
        Chart showingChart { get; set; }

        internal ChartExtExampleForm()
        {
            InitializeComponent();

            Action<ChartArea, PointF> printCoordinate = (ChartArea chartArea, PointF point) =>
            {
                MessageBox.Show(string.Format("在ChartArea[{0}]中，点击了坐标{1}", chartArea.Name, point));
            };

            Action<Series, DataPoint> processPoint = (Series series, DataPoint point) =>
           {
               textBox1.Text = point.ToString();
           };

            #region 双击事件
            AddChartExample(new ChartExtExample("双击事件/切换显示",
                new List<string>(new string[] { "chart.RegistClickOne_Multi_SwitchEvent(ControlClickEvent.DoubleClick);" }),
                "鼠标在一个ChartArea上双击可以切换[1个/多个]ChartArea显示", 4,
                (Chart chart) => { chart.RegistClickOne_Multi_SwitchEvent(ControlClickEvent.DoubleClick); }));

            AddChartExample(new ChartExtExample("双击事件/改变线条颜色",
                new List<string>(new string[] { "chart.RegistClickChangeChartAreaLineColorEvent(ControlClickEvent.DoubleClick);" }),
                "鼠标在一个ChartArea上双击可以改变该ChartArea的线条颜色", 4,
                (Chart chart) => { chart.RegistClickChangeChartAreaLineColorEvent(ControlClickEvent.DoubleClick); }));

            AddChartExample(new ChartExtExample("双击事件/获取坐标",
                new List<string>(new string[] { "Action<ChartArea, PointF> printCoordinate = (ChartArea chartArea, PointF point) =>",
                    "{" ,
                    "\t//获取坐标后的处理方法" ,
                    "}" ,
                    "chart.RegistClickGetChartAreaInnerCoordinateEvent(ControlClickEvent.DoubleClick, printCoordinate);" }),
                "鼠标在一个ChartArea上双击可以获取鼠标在该ChartArea上的坐标，并对该坐标进行自定义处理", 4,
                (Chart chart) => { chart.RegistClickGetChartAreaInnerCoordinateEvent(ControlClickEvent.DoubleClick, printCoordinate); }));

            AddChartExample(new ChartExtExample("双击事件/复制点列",
                new List<string>(new string[] { "chart.RegistClickCopyPointsEvent(ControlClickEvent.DoubleClick);" }),
                "鼠标在一个ChartArea上双击可以复制该ChartArea中的点列", 4,
                (Chart chart) => { chart.RegistClickCopyPointsEvent(ControlClickEvent.DoubleClick); }));
            #endregion

            #region 左键单击事件
            AddChartExample(new ChartExtExample("左键单击事件/切换显示",
                new List<string>(new string[] { "chart.RegistClickOne_Multi_SwitchEvent(ControlClickEvent.LeftClick);" }),
                "鼠标在一个ChartArea上左键单击可以切换[1个/多个]ChartArea显示", 4,
                (Chart chart) => { chart.RegistClickOne_Multi_SwitchEvent(ControlClickEvent.LeftClick); }));

            AddChartExample(new ChartExtExample("左键单击事件/改变线条颜色",
                new List<string>(new string[] { "chart.RegistClickChangeChartAreaLineColorEvent(ControlClickEvent.LeftClick);" }),
                "鼠标在一个ChartArea上左键单击可以改变该ChartArea的线条颜色", 4,
                (Chart chart) => { chart.RegistClickChangeChartAreaLineColorEvent(ControlClickEvent.LeftClick); }));

            AddChartExample(new ChartExtExample("左键单击事件/获取坐标",
                new List<string>(new string[] { "Action<ChartArea, PointF> printCoordinate = (ChartArea chartArea, PointF point) =>",
                    "{" ,
                    "\t//获取坐标后的处理方法" ,
                    "}" ,
                    "chart.RegistClickGetChartAreaInnerCoordinateEvent(ControlClickEvent.LeftClick, printCoordinate);" }),
                "鼠标在一个ChartArea上左键单击可以获取鼠标在该ChartArea上的坐标，并对该坐标进行自定义处理", 4,
                (Chart chart) => { chart.RegistClickGetChartAreaInnerCoordinateEvent(ControlClickEvent.LeftClick, printCoordinate); }));

            AddChartExample(new ChartExtExample("左键单击事件/复制点列",
                new List<string>(new string[] { "chart.RegistClickCopyPointsEvent(ControlClickEvent.LeftClick);" }),
                "鼠标在一个ChartArea上左键单击可以复制该ChartArea中的点列", 4,
                (Chart chart) => { chart.RegistClickCopyPointsEvent(ControlClickEvent.LeftClick); }));
            #endregion

            #region 右键单击事件
            AddChartExample(new ChartExtExample("右键单击事件/切换显示",
                new List<string>(new string[] { "chart.RegistClickOne_Multi_SwitchEvent(ControlClickEvent.RightClick);" }),
                "鼠标在一个ChartArea上右键单击可以切换[1个/多个]ChartArea显示", 4,
                (Chart chart) => { chart.RegistClickOne_Multi_SwitchEvent(ControlClickEvent.RightClick); }));

            AddChartExample(new ChartExtExample("右键单击事件/改变线条颜色",
                new List<string>(new string[] { "chart.RegistClickChangeChartAreaLineColorEvent(ControlClickEvent.RightClick);" }),
                "鼠标在一个ChartArea上右键单击可以改变该ChartArea的线条颜色", 4,
                (Chart chart) => { chart.RegistClickChangeChartAreaLineColorEvent(ControlClickEvent.RightClick); }));

            AddChartExample(new ChartExtExample("右键单击事件/获取坐标",
                new List<string>(new string[] { "Action<ChartArea, PointF> printCoordinate = (ChartArea chartArea, PointF point) =>",
                    "{" ,
                    "\t//获取坐标后的处理方法" ,
                    "}" ,
                    "chart.RegistClickGetChartAreaInnerCoordinateEvent(ControlClickEvent.RightClick, printCoordinate);" }),
                "鼠标在一个ChartArea上右键单击可以获取鼠标在该ChartArea上的坐标，并对该坐标进行自定义处理", 4,
                (Chart chart) => { chart.RegistClickGetChartAreaInnerCoordinateEvent(ControlClickEvent.RightClick, printCoordinate); }));

            AddChartExample(new ChartExtExample("右键单击事件/复制点列",
                new List<string>(new string[] { "chart.RegistClickCopyPointsEvent(ControlClickEvent.RightClick);" }),
                "鼠标在一个ChartArea上右键单击可以复制该ChartArea中的点列", 4,
                (Chart chart) => { chart.RegistClickCopyPointsEvent(ControlClickEvent.RightClick); }));
            #endregion

            #region 鼠标移动事件
            AddChartExample(new ChartExtExample("鼠标移动事件/显示数据点",
                new List<string>(new string[] { "chart.RegistMouseMovePointToolTipEvent(Color.Red, 10, MarkerStyle.Circle);" }),
                "鼠标移动到图表的一个点附近时可以查看该点坐标", 4,
                (Chart chart) => { chart.RegistMouseMovePointToolTipEvent(Color.Red, 10, MarkerStyle.Circle); }));

            AddChartExample(new ChartExtExample("鼠标移动事件/处理数据点",
                new List<string>(new string[] { "Action<Series, DataPoint> processPoint = (Series series, DataPoint point) =>",
                    "{",
                    "\t//自定义的操作",
                    "};",
                    "chart.RegistMouseMoveProcessPointEvent(processPoint);" }),
                "鼠标移动到图表的一个点附近时可以对该点进行处理", 4,
                (Chart chart) => { chart.RegistMouseMoveProcessPointEvent(processPoint); }));
            #endregion

            #region 鼠标滚轮事件
            AddChartExample(new ChartExtExample("鼠标滚轮事件/坐标轴缩放",
                new List<string>(new string[] { "chart.RegistMouseWheelScaleEvent(1f , 1f , 1f , 0 , 1);" }),
                "鼠标在一个ChartArea内转动滚轮时可以对该图表进行缩放", 4,
                (Chart chart) => { chart.RegistMouseWheelScaleEvent(1f , 1f , 1f , 0 , 1); }));

            #endregion
        }

        void AddChartExample(ChartExtExample chartExtExample)
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

        void ShowChartExample(ChartExtExample chartExtExample)
        {
            if (showingChart != null) showingChart.Dispose();
            showingChart = chartExtExample.CreateChart();
            chartPanel.Controls.Add(showingChart);
            showingChart.Dock = DockStyle.Fill;

            chartExtExample.SetFullCodeToRichTextBox(richTextBox1);

            descriptionToolStripStatusLabel.Text = chartExtExample.description;
            showingChart.MouseMove += ChartExtExampleForm_MouseMove;
            正弦波();
        }

        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ChartExampleTreeNode chartExampleTreeNode = e.Node as ChartExampleTreeNode;
            if(chartExampleTreeNode != null)
            {
                ShowChartExample(chartExampleTreeNode.chartExtExample);
            }
        }

        void ChartExtExampleForm_MouseMove(object sender, MouseEventArgs e)
        {
            if(showingChart != null)
            {
                PointF coordinate = e.Location;
                for(int i = 0; i < showingChart.ChartAreas.Count; i++)
                {
                    ChartArea chartArea = showingChart.ChartAreas[i];
                    if(showingChart.GetCoordinateOfMousePositionInChartArea(chartArea, e.Location, out coordinate))
                    {
                        coordinateToolStripStatusLabel.Text = String.Format("{0}-{1}" , chartArea.Name , coordinate);
                        return;
                    }
                }
            }
            coordinateToolStripStatusLabel.Text = "(0,0)";
        }
    }
}
