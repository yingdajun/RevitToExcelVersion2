using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace ArchiElementDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
   
    public partial class FloorWpf : Window
    {
        public DataTable dt;
        //public string dataTableName;
        public FloorWpf(DataTable dataTable)
        {
            dt = dataTable;
           
            InitializeComponent();
        }

        private void CreateEnter_Click(object sender, RoutedEventArgs e)
        {

            //这里应该是一个载入数据的按钮
            
            //如果这两个不为0 那么确保都存在
            if (dataBaseName.Text!=null&& dataTableName.Text!=null) {
                
                //生成对应的表格
                MySQLIntial.CreateMysqlDB(dataBaseName.Text.ToString());

                //打开对应的表格进行相关的操作
                //这里应该是MYSQL打开数据按钮，这里可以通过改变换取不同的操作
                string connStr = "server=localhost;database=" + dataBaseName.Text.ToString()
                    + ";uid=root;pwd=123456";


                //批量生成对应的表格

                //打开对应的表格
                string usedata = "use " + dataBaseName.Text.ToString();

                //在这里应该生成相关的MYSQL的对话框
                //判断是否建表成功
                //建立表格成功

                //这里是插件表格
                var result = MySQLIntial.BulkInsert(connStr, dt, dataBaseName.Text.ToString(),dataTableName.Text.ToString(), 1);

                //如果建表成功弹出相关结果
                if (result != 0.0)
                {
                    TaskDialog.Show("导出到MYSQL中成功", "数据已经存入" + "数据库"+dataBaseName.Text.ToString() +"数据表" + dataTableName.Text.ToString() + "中");
                }

            } else{
                //数据库没有写
                if (dataBaseName.Text==null) {
                    TaskDialog.Show("错误","数据库名称没有写");
                }
                //数据表没有写
                if (dataTableName.Text ==null) {
                    TaskDialog.Show("错误", "数据表名称没有写");
                }
            }

            DialogResult = true;
        }


      

        private void DeleteEnter_Click(object sender, RoutedEventArgs e)
        {

            //链接必须打开同时开启 的
            //如果这两个不为0 那么确保都存在
            if (dataBaseName.Text != null && dataTableName.Text != null)
            {
                //删除
                //这里应该是MYSQL打开数据按钮，这里可以通过改变换取不同的操作
                string connStr = "server=localhost;database=" + dataBaseName.Text.ToString()
                    + ";uid=root;pwd=123456";
                TaskDialog.Show("1",connStr);

                string usedata = "use " + dataBaseName.Text.ToString();


                string dropStatement = "drop table " + dataTableName.Text.ToString()+";";

                //开始连接数据
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        using (MySqlCommand cmdb = new MySqlCommand(usedata, conn))
                        {
                            cmdb.ExecuteNonQuery();
                        }
                        using (MySqlCommand cmd = new MySqlCommand(dropStatement, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        TaskDialog.Show("操作", "删除数据表");
                        conn.Close();
                        //result = true;
                    }
                    catch (Exception ex)
                    {
                        //果然是这个原因。。。MMP
                        //TaskDialog.Show("报错", "删除数据表");
                        TaskDialog.Show("删除数据表失败的原因", ex.Message);
                    }
                }

            }
            else
            {
                //数据库没有写
                if (dataBaseName.Text == null)
                {
                    TaskDialog.Show("错误", "数据库名称没有写");
                }
                //数据表没有写
                if (dataTableName.Text == null)
                {
                    TaskDialog.Show("错误", "数据表名称没有写");
                }
            }

            DialogResult = true;
        }
    }
}
