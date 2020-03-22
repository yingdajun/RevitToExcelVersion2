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
    class BeamTest : IExternalCommand
    {
        //这里要进行改造
        string excelPath = null;
            //@"D:\梁demo.xls";
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //初始化相关元素
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            
            //创造梁过滤器

            FilteredElementCollector beamCollector = BeamIntial.BeamCollector(doc);
            
            //自动生成一个DataTable表格，将这份表格导入到CSV和MYSQL中

            //生成相关TITLE中
            DataTable dt = BeamIntial.CreateBeamExcelTitle();

            //调用静态函数 
            BeamIntial.BeamElementExcelPara(doc, beamCollector, dt);

            string t = "梁.xls";

            excelPath =LChuDemo.PickFolderInfo() + t;



            //将其导入到相关的位置下
            LChuDemo.dataTableToCsv(dt, excelPath);

            //导入到相关位置
            //TaskDialog.Show("表已经导入到当前位置",excelPath.ToString());


            System.Diagnostics.Process.Start(excelPath);




            ////生成一个新的MYSQL数据表格
            //dt = BeamIntial.CreateBeamMySQLTitle();


            ////这是为了给datatable赋值，放置不存在，嘻嘻嘻嘻
            ////赋予DATATABLE的值
            //dt.TableName = "BeamTable";

            ////将MYSQL数据导入到相关数据中
            //BeamIntial.BeamElementMySQLPara(doc, beamCollector, dt);


            ////这里是一个按钮

            //传入数据
            BeamWpf beamWpf = new BeamWpf(dt);

            if (beamWpf.ShowDialog() == false)
            {
                return Result.Cancelled;
            }






            return Result.Succeeded;
        }
    }
}
