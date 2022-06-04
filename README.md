# 1 本项目目的
&emsp;&emsp;提供Winform中各种控件的扩展功能。

# 2 使用指南
&emsp;&emsp;使用本项目需要先将WinformControlExt项目包含进自己的项目。
## 2.1 Chart控件扩展

&emsp;&emsp;使用Chart控件的扩展功能，需要添加引用：
```C#
using WinformControlExt.ChartExts;
using WinformControlExt.ControlEvents;
```

&emsp;&emsp;Chart控件扩展功能的示例可以运行Examples项目中的<font color="#00aa44">ChartExtExampleForm</font>窗体进行查看。

### 2.1.1 <font color="#ff2222">[鼠标点击事件]</font>显示切换

<details>

&emsp;&emsp;本功能可以实现<font color="#0044ff">多个ChartArea</font>和<font color="#0044ff">一个ChartArea</font>之间的切换。使用函数<font color="#dd0000">RegistClickOne_Multi_SwitchEvent</font>来实现：<br/>
1. 鼠标双击切换
```C#
chart.RegistClickOne_Multi_SwitchEvent(ControlClickEvent.DoubleClick);
```
2. 鼠标左键单击切换
```C#
chart.RegistClickOne_Multi_SwitchEvent(ControlClickEvent.LeftClick);
```
3. 鼠标右键单击切换
```C#
chart.RegistClickOne_Multi_SwitchEvent(ControlClickEvent.RightClick);
```

</details>

### 2.1.2 <font color="#ff2222">[鼠标点击事件]</font>改变线条颜色
<details>
&emsp;&emsp;本功能可以实现一个ChartArea中的线条颜色随机切换。使用函数<font color="#dd0000">RegistClickChangeChartAreaLineColorEvent</font>来实现：<br/>

1. 鼠标双击改变线条颜色
```C#
chart.RegistClickChangeChartAreaLineColorEvent(ControlClickEvent.DoubleClick);
```
2. 鼠标左键单击改变线条颜色
```C#
chart.RegistClickChangeChartAreaLineColorEvent(ControlClickEvent.LeftClick);
```
3. 鼠标右键单击改变线条颜色
```C#
chart.RegistClickChangeChartAreaLineColorEvent(ControlClickEvent.RightClick);
```
</details>

### 2.1.3 <font color="#ff2222">[鼠标点击事件]</font>获取坐标

<details>

&emsp;&emsp;本功能可以在鼠标移动到某个ChartArea内时，获取鼠标在该ChartArea上的对应坐标，并对该坐标进行自定义处理。<br>
&emsp;&emsp;在注册本功能时需要先有一个<span id="数据处理回调函数">数据处理的回调函数</span>：
```C#
void ProcessPoint(Series series, DataPoint point)
{
    //进行自定义处理
}
```

&emsp;&emsp;或是一个Action变量：

```C#
Action<Series, DataPoint> ProcessPoint = (Series series, DataPoint point) =>
    {
        //进行自定义处理
    };
```

&emsp;&emsp;有了回调函数后，本功能可以使用函数<font color="#dd0000">RegistClickGetChartAreaInnerCoordinateEvent</font>来实现：<br/>

1. 鼠标双击获取坐标
```C#
chart.RegistClickGetChartAreaInnerCoordinateEvent(ControlClickEvent.DoubleClick , ProcessPoint);
```
2. 鼠标左键单击获取坐标
```C#
chart.RegistClickGetChartAreaInnerCoordinateEvent(ControlClickEvent.LeftClick , ProcessPoint);
```
3. 鼠标右键单击获取坐标
```C#
chart.RegistClickGetChartAreaInnerCoordinateEvent(ControlClickEvent.RightClick , ProcessPoint);
```

</details>

### 2.1.4 <font color="#ff2222">[鼠标点击事件]</font>复制点列

<details>

&emsp;&emsp;本功能可以实现复制一个ChartArea中各Series的所有点列。使用函数<font color="#dd0000">RegistClickCopyPointsEvent</font>来实现：<br/>

1. 鼠标双击复制点列
```C#
chart.RegistClickCopyPointsEvent(ControlClickEvent.DoubleClick);
```
2. 鼠标左键单击复制点列
```C#
chart.RegistClickCopyPointsEvent(ControlClickEvent.LeftClick);
```
3. 鼠标右键单击复制点列
```C#
chart.RegistClickCopyPointsEvent(ControlClickEvent.RightClick);
```

</details>

### 2.1.5 <font color="#ff2222">[鼠标移动事件]</font>显示数据点

<details>

&emsp;&emsp;本功能可以实现在鼠标经过一个ChartArea时查看距离最近的DataPoint的坐标。本功能使用函数<font color="#dd0000">RegistMouseMoveProcessPointEvent</font>来实现：<br/>

```C#
chart.RegistMouseMovePointToolTipEvent(Color.Red, 10, MarkerStyle.Circle);
```

&emsp;&emsp;其中，三个参数分别代表了选中的点的标记颜色、尺寸和形态。

</details>

### 2.1.6 <font color="#ff2222">[鼠标移动事件]</font>处理数据点

<details>

&emsp;&emsp;本功能可以实现在鼠标经过一个ChartArea时对距离鼠标最近的点进行自定义处理（同样，需要先定义一个[数据处理的回调函数](#数据处理回调函数)，如上）。本功能使用函数<font color="#dd0000">RegistMouseMoveProcessPointEvent</font>来实现：<br/>

```C#
chart.RegistMouseMoveProcessPointEvent(ProcessPoint);
```

</details>

### 2.1.7 <font color="#ff2222">[鼠标滚轮事件]</font>坐标轴缩放

<details>

&emsp;&emsp;本功能可以实现鼠标在一个ChartArea内转动滚轮时对该图表进行缩放。本功能使用函数<font color="#dd0000">RegistMouseWheelScaleEvent</font>来实现：<br/>

```C#
chart.RegistMouseWheelScaleEvent(1f , 1f , 1f , 0 , 1);
```

&emsp;&emsp;其中，5个参数依次代表：<br/>
* 缩放速度
* x轴能放缩到的最小长度
* y轴能放缩到的最小长度
* x轴最大最小值保留的小数点位数
* y轴最大最小值保留的小数点位数

</details>

### 2.1.8 <font color="#ff2222">[鼠标拖拽事件]</font>坐标轴移动

<details>

&emsp;&emsp;本功能可以实现鼠标在一个ChartArea内拖拽时对坐标轴进行移动。本功能使用函数<font color="#dd0000">RegistDragMoveEvent</font>来实现：<br/>

```C#
chart.RegistDragMoveEvent(0 , 1);
```

&emsp;&emsp;其中，2个参数依次代表：<br/>
* x轴最大最小值保留的小数点位数
* y轴最大最小值保留的小数点位数
  
</details>

