using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace ArchiElementDemo
{
    //这里是创建相关按钮的面板
    public class Ribbon : IExternalApplication
    {
        //获取插件所在的位置
        string AddinPath = typeof(Ribbon).Assembly.Location;

        public Result OnShutdown(UIControlledApplication application)
        {
            TaskDialog.Show("提取数据插件关闭", "提取数据插件");
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            //对话框
            TaskDialog.Show("毕设插件成功安装", "功能为提取BIM模型信息");
            //毕设插件的名称
            string tab = "毕设插件";
            //这里是一个面板
            application.CreateRibbonTab(tab);
            //生成按钮面板
            RibbonPanel panel = application.CreateRibbonPanel(tab, "导出BIM构件信息到EXCEL表格");
            //生成按钮
            AddSpliteButton(panel);
            return Result.Succeeded;
        }

        /// <summary>
        /// 这里是生成各种按钮
        /// </summary>
        /// <param name="panel"></param>
        private void AddSpliteButton(RibbonPanel panel)
        {
            ///按钮分三组，分别为建筑/结构/机电
            //建筑按钮  负责导出  墙  楼板  门  窗  房间等相关参数信息

            //墙的按钮
            PushButtonData wallbtn = new PushButtonData("wallName", "墙信息导出", AddinPath, "ArchiElementDemo.WallTest");
            //给按钮上图片
            wallbtn.LargeImage = new BitmapImage(new Uri(AddinPath.Replace("ArchiElementDemo.dll", "wall.ico")));
            //按钮功能提示
            wallbtn.ToolTip = "将项目中墙参数信息导出";

            //楼板按钮
            PushButtonData floorbtn = new PushButtonData("floorName", "楼板信息导出", AddinPath, "ArchiElementDemo.FloorTest");
            //给按钮上图片
            floorbtn.LargeImage = new BitmapImage(new Uri(AddinPath.Replace("ArchiElementDemo.dll", "floor.ico")));
            //按钮功能提示
            floorbtn.ToolTip = "将项目中楼板参数信息导出";

            //门按钮
            PushButtonData doorbtn = new PushButtonData("doorName", "门信息导出", AddinPath, "ArchiElementDemo.DoorTest");
            //给按钮上图片
            doorbtn.LargeImage = new BitmapImage(new Uri(AddinPath.Replace("ArchiElementDemo.dll", "door.ico")));
            //按钮功能提示
            doorbtn.ToolTip = "将项目中门参数信息导出";

            //窗按钮
            PushButtonData windowbtn = new PushButtonData("windowName", "窗信息导出", AddinPath, "ArchiElementDemo.WindowTest");
            //给按钮上图片
            windowbtn.LargeImage = new BitmapImage(new Uri(AddinPath.Replace("ArchiElementDemo.dll", "window.ico")));
            //按钮功能提示
            windowbtn.ToolTip = "将项目中窗参数信息导出";

            //房间按钮
            PushButtonData roombtn = new PushButtonData("roomName", "房间信息导出", AddinPath, "ArchiElementDemo.RoomTest");
            //给按钮上图片
            roombtn.LargeImage = new BitmapImage(new Uri(AddinPath.Replace("ArchiElementDemo.dll", "room.ico")));
            //按钮功能提示
            roombtn.ToolTip = "将项目中房间参数信息导出";

            //生成建筑样板的拆分按钮
            SplitButtonData sp1 = new SplitButtonData("建筑", "建筑");
            //生成按钮
            SplitButton sb1 = panel.AddItem(sp1) as SplitButton;
            //把上述按钮给装进去
            sb1.AddPushButton(wallbtn);
            sb1.AddPushButton(floorbtn);
            sb1.AddPushButton(doorbtn);
            sb1.AddPushButton(windowbtn);
            sb1.AddPushButton(roombtn);

            //生成分割线
            panel.AddSeparator();

            //结构相关按钮
            //负责导出结构柱和梁
            PushButtonData columnbtn = new PushButtonData("columnName", "结构柱信息导出", AddinPath, "ArchiElementDemo.ColumnTest");

            columnbtn.LargeImage = new BitmapImage(new Uri(AddinPath.Replace("ArchiElementDemo.dll", "column.ico")));
            columnbtn.ToolTip = "将项目中结构柱参数信息导出";


            PushButtonData beambtn = new PushButtonData("beamName", "梁信息导出", AddinPath, "ArchiElementDemo.BeamTest");

            beambtn.LargeImage = new BitmapImage(new Uri(AddinPath.Replace("ArchiElementDemo.dll", "beam.ico")));
            beambtn.ToolTip = "将项目中梁参数信息导出";

            //结构相关按钮
            SplitButtonData sp2 = new SplitButtonData("结构", "结构");
            SplitButton sb2 = panel.AddItem(sp2) as SplitButton;
            sb2.AddPushButton(columnbtn);
            sb2.AddPushButton(beambtn);

            //生成分割线
            panel.AddSeparator();

            //生成机电相关按钮  导出风管，水，和电缆桥架
            PushButtonData ductbtn = new PushButtonData("ductName", "风管信息导出", AddinPath, "ArchiElementDemo.DuctTest");
            ductbtn.LargeImage = new BitmapImage(new Uri(AddinPath.Replace("ArchiElementDemo.dll", "duct.ico")));
            ductbtn.ToolTip = "将项目中风管参数信息导出";
            PushButtonData pipebtn = new PushButtonData("pipeguanName", "水管信息导出", AddinPath, "ArchiElementDemo.PipeTest");
            pipebtn.LargeImage = new BitmapImage(new Uri(AddinPath.Replace("ArchiElementDemo.dll", "pipe.ico")));
            pipebtn.ToolTip = "将项目中水管参数信息导出";
            PushButtonData qiaobtn = new PushButtonData("elqName", "桥架信息导出", AddinPath, "ArchiElementDemo.EletricalQTest");
            qiaobtn.LargeImage = new BitmapImage(new Uri(AddinPath.Replace("ArchiElementDemo.dll", "qiaojia.ico")));
            qiaobtn.ToolTip = "将项目中电缆桥架参数信息导出";
            SplitButtonData sp3 = new SplitButtonData("机电", "机电");
            SplitButton sb3 = panel.AddItem(sp3) as SplitButton;
            sb3.AddPushButton(ductbtn);
            sb3.AddPushButton(pipebtn);
            sb3.AddPushButton(qiaobtn);
        }
    }
}
