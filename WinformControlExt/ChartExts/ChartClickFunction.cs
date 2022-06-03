using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformControlExt.ChartExts
{
#if ComplieChart
    public enum ChartClickFunction
    {
        One_Multi_Switch,
        ChangeChartAreaLineColor,
        GetChartAreaInnerCoordinate,
        CopyPoints,
    }
#endif
}
