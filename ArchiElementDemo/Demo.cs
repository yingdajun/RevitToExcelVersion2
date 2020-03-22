using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ArchiElementDemoDemo
{
    [Transaction(TransactionMode.Manual)]
    class Demo : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //throw new NotImplementedException();
            //MySQLManager.CreateMysqlDB();
            TaskDialog.Show("1","1");
            return Result.Succeeded;
        }
    }
}
