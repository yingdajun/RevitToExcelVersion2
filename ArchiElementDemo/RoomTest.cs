using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Structure;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Data;
namespace ArchiElementDemo
{
    [Transaction(TransactionMode.Manual)]
    class RoomTest : IExternalCommand
    {
        public string excelPath = null;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            FilteredElementCollector roomCollector = RoomIntial.
                RoomCollector(doc);
            DataTable dt = RoomIntial.CreateRoomExcelTitle();
            //这里有问题MMP
            RoomIntial.RoomElementExcelPara(doc, roomCollector, dt);
            string t = "房间.xls";

            excelPath = LChuDemo.PickFolderInfo() + t;
            //TaskDialog.Show("EXCEL放置位置", excelPath.ToString());
            LChuDemo.dataTableToCsv(dt, excelPath);
            System.Diagnostics.Process.Start(excelPath);

            ////这里是倒进数据库
            //dt = RoomIntial.CreateRoomMySQLTitle();
            //dt.TableName = "RoomTable";
            //RoomIntial.RoomElementMySQLPara(doc, roomCollector, dt);
            //string connStr = "server=localhost;database=mytest;uid=root;pwd=123456";
            //var result = MySQLIntial.BulkInsert(connStr, dt, 4);
            //if (result != 0.0)
            //{
            //    TaskDialog.Show("导出到MYSQL中成功", "数据已经存入" + "数据库mytest" + dt.TableName + "中");
            //}

            RoomWpf doorWpf = new RoomWpf(dt);

            if (doorWpf.ShowDialog() == false)
            {
                return Result.Cancelled;
            }

            return Result.Succeeded;
        }
        public static double FeetTomm(double val)
        {
            return val * 304.8;
        }
    }
}
