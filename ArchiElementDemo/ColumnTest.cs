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
    class ColumnTest : IExternalCommand
    {
        //这里要进行改造
        string excelPath = null;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            FilteredElementCollector columnCollector = ColumnIntial.ColumnCollector(doc);

            DataTable dt = ColumnIntial.CreateColumnExcelTitle();

            ColumnIntial.ColumnElementExcelPara(doc, columnCollector, dt);

            string t = "结构柱.xls";

            excelPath = LChuDemo.PickFolderInfo() + t;

            LChuDemo.dataTableToCsv(dt, excelPath);

            System.Diagnostics.Process.Start(excelPath);


            //dt = ColumnIntial.CreateColumnMySQLTitle();

            //dt.TableName = "StructColumnTable";

            //ColumnIntial.ColumnElementMySQLPara(doc, columnCollector, dt);



            ////传入数据
            //ColumnWpf columnWpf = new ColumnWpf(dt);

            //if (columnWpf.ShowDialog() == false)
            //{
            //    return Result.Cancelled;
            //}

            //传入数据
            ColumnWpf columnWpf = new ColumnWpf(dt);

            if (columnWpf.ShowDialog() == false)
            {
                return Result.Cancelled;
            }

            return Result.Succeeded;
        }
    }
}
