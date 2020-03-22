﻿using System;
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
    class WallTest : IExternalCommand
    {
        public string excelPath = null;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            FilteredElementCollector wallCollector = WallIntial.WallCollector(doc);
            DataTable dt = WallIntial.CreateWallExcelTitle();
            WallIntial.
            WallElementExcelPara(doc, wallCollector, dt);

            string t = "墙体.xls";

            excelPath = LChuDemo.PickFolderInfo() + t;

            //TaskDialog.Show("EXCEL放置位置", excelPath.ToString());
            LChuDemo.
            dataTableToCsv(dt, excelPath);
            System.Diagnostics.Process.Start(excelPath);
            //dt = WallIntial.CreateWallMySQLTitle();
            //dt.TableName = "WallTable";
            //WallIntial.WallElementMySQLPara(doc, wallCollector, dt);
            //string connStr = "server=localhost;database=mytest;uid=root;pwd=123456";
            //var result = MySQLIntial.BulkInsert(connStr, dt, 0);
            //if (result != 0.0)
            //{
            //    TaskDialog.Show("导出到MYSQL中成功", "数据已经存入" + "数据库mytest" + "\n" + "数据表" + dt.TableName + "中");
            //}

            WallWpf doorWpf = new WallWpf(dt);

            if (doorWpf.ShowDialog() == false)
            {
                return Result.Cancelled;
            }
            return Result.Succeeded;
        }
    }
}
