using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Windows.Forms;
namespace ArchiElementDemo
{
    class LChuDemo
    {
        //将DataTable相关的信息导入到CSV各式中
        public static void dataTableToCsv(System.Data.DataTable table, string file)
        {
            string title = "";
            FileStream fs = new FileStream(file, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                title += table.Columns[i].ColumnName + "\t";
            }
            title = title.Substring(0, title.Length - 1) + "\n";
            sw.Write(title);
            foreach (DataRow row in table.Rows)
            {
                string line = "";
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    line += row[i].ToString().Trim() + "\t";
                }
                line = line.Substring(0, line.Length - 1) + "\n";
                sw.Write(line);
            }
            sw.Close();
            fs.Close();
            TaskDialog.Show("使用提示", "更新数据前请把当前的EXCEL关闭");
        }

        /// <summary>
        /// 这个是获取对象方法
        /// </summary>
        /// <returns></returns>
        public static string PickFolderInfo()
        {
            string urlRoomUrl = null;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择想要将EXCEL导入的地址";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                urlRoomUrl = dialog.SelectedPath + "\\";
            }
            return urlRoomUrl;
        }

    }
}
