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
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ArchiElementDemo
{
    class MySQLIntial
    {
        /// <summary>
        /// 别问我这个是干嘛，我也是从网上抄来的，哈哈哈哈
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        //把数据批量插入中



        public static int BulkInsert(string connectionString, DataTable table, string dataBaseName,string dataTableName, int i)
        {
            //附上标的名称
            if (string.IsNullOrEmpty(table.TableName))
                throw new Exception("请给DataTable的TableName属性附上表名称");
            //强行赋予
            table.TableName = dataTableName;

            if (table.Rows.Count == 0) return 0;
            int insertCount = 0;
            string tmpPath = Path.Combine(Directory.GetCurrentDirectory(), "Temp.csv"); string csv = DataTableToCsv(table);
            File.WriteAllText(tmpPath, csv);

            //开始连接数据
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    conn.Open();

                    //这一块是使用不同的函数进行不同的判断观察这是哪一类进行学习
                    //这才是代码的核心重点
                    bool result =
                    AlterTableExample(conn,i, dataBaseName,dataTableName);

                    MySqlBulkLoader bulk = new MySqlBulkLoader(conn)
                    {
                        FieldTerminator = ",",
                        FieldQuotationCharacter = '"',
                        EscapeCharacter = '"',
                        LineTerminator = "\r\n",
                        FileName = tmpPath,
                        NumberOfLinesToSkip = 0,
                        TableName = table.TableName,
                    };
                    insertCount = bulk.Load();
                    stopwatch.Stop();
                    conn.Close();
                    if (result)
                    {
                        TaskDialog.Show("批量导入数据库的时间", "耗时:" + stopwatch.ElapsedMilliseconds.ToString() + "毫秒");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("连接失败！", "测试结果");
                    TaskDialog.Show("REVIT", ex.ToString());
                    throw ex;
                }
            }
            File.Delete(tmpPath);
            return insertCount;
        }

        //导入数据按钮
        private static string DataTableToCsv(DataTable table)
        {
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colum = table.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else sb.Append(row[colum].ToString());
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }


        /// <summary>
        /// 创建数据库
        /// </summary>
        public static void CreateMysqlDB(string databaseName)
        {
            MySqlConnection conn = new MySqlConnection("Data Source=localhost;Persist Security Info=yes; " +
                                                   "UserId=root; PWD=123456");
            
            string sqlCom = "CREATE DATABASE foxDemo;";
            string useCom = sqlCom.Replace("foxDemo", databaseName);
            MySqlCommand cmd = new MySqlCommand(useCom, conn);


            conn.Open();

            //防止第二次启动时再次新建数据库
            try
            {
                cmd.ExecuteNonQuery();
                conn.Close();
                Console.WriteLine("建立数据库成功");
            }
            catch (Exception)
            {
                conn.Close();
                Console.WriteLine("建立数据库失败，已存在了");
                //throw;
            }
            //防止第二次启动时再次新建数据库
        }




        public static void btnCreateDB(object sender, System.EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("Data Source=localhost;Persist Security Info=yes;UserId=root; PWD=你的密码;");
            MySqlCommand cmd = new MySqlCommand("CREATE DATABASE +"+"mykeep", conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// 生成相应的插件
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="type"></param>
        /// <param name="dataBaseName"></param>
        /// <param name="dataTableName"></param>
        /// <returns></returns>
        public static bool AlterTableExample(MySqlConnection conn, int type,string dataBaseName,string dataTableName)
        {
            bool result = false;
            string createStatement = null;
            string usedata = "use " + dataBaseName;
                //"mytest";
            switch (type)
            {
                case 0:
                    //"WallTable "
                    createStatement = "CREATE TABLE " +
                         dataTableName+
                        "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,walltype VarChar(50),levelName VarChar(50),width Decimal(10,2),height Decimal(10,2))";
                    break;
                case 1:
                    //"FloorTable "
                    createStatement = "CREATE TABLE " +
                        dataTableName +
                        "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,floorType VarChar(50),levelName VarChar(50),thick Decimal(10,2) ,area Decimal(10,2),offset Decimal(10,2))";
                    break;
                case 2:
                    //"DoorTable "
                    createStatement = "CREATE TABLE " +
                       dataTableName+
                        "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,doortype VarChar(50),levelName VarChar(50),drma VarChar(50),kjma VarChar(50),width VarChar(50),bottomheight VarChar(50),height VarChar(50))";
                    break;
                case 3:
                    //"WindowTable "
                    createStatement = "CREATE TABLE " +
                         dataTableName+
                        "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,windowsType VarChar(50),levelName VarChar(50),outcha VarChar(50),incha VarChar(50),bolicha VarChar(50),width VarChar(50),bottomheight VarChar(50),height VarChar(50))";
                    break;
                case 4:
                    //"RoomTable "
                    createStatement = "CREATE TABLE " +
                        dataTableName+
                        "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,number VarChar(50),name VarChar(50),levelName VarChar(50),bottomOffset Decimal(10,2),area Decimal(10,2),topoffset Decimal(10,2))";
                    break;
                case 5:
                    //"StructColumnTable "
                    createStatement = "CREATE TABLE " +
                        dataTableName+
                        "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,columnType VarChar(50),category VarChar(50),type VarChar(50),material VarChar(50),width VarChar(50), height  VarChar(50), bottomLev VarChar(50), bottomOffset  VarChar(50), topLev VarChar(50), topOffset  VarChar(50),isMove VARCHAR(50))";
                    break;
                case 6:
                    //"BeamTable"
                    createStatement = "CREATE TABLE " +
                        dataTableName+
                        " (id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,beamName VarChar(50),Olev VarChar(50),material VarChar(50),length Decimal(10,2),y_duizheng VarChar(50),y_offset Decimal(10,2),yz_duizheng VarChar(50),z_duizheng VarChar(50),z_offset Decimal(10,2))";
                    break;
                case 7:
                    //"DuctTable "
                    createStatement = "CREATE TABLE " +
                        dataTableName+
                        "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,DuctName VarChar(50),Olev VarChar(50),Width  Decimal(10,2),Height Decimal(10,2),Offset Decimal(10,2),shuidui VarChar(50),chuizhidui VarChar(50),type VarChar(50))";
                    break;
                case 8:
                    //"PipeTable "
                    createStatement = "CREATE TABLE " +
                         dataTableName+
                        "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,pipeName VarChar(50),Olev VarChar(50),shuidui VarChar(50),chuizhidui VarChar(50),offset Decimal(10,2),slope VarChar(50),type VarChar(50),guanduan VarChar(50),r VarChar(50))";
                    break;
                case 9:
                    //"EletricalQTable"
                    createStatement = "CREATE TABLE " +
                         dataTableName+
                        " (id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,categoryName VarChar(50),Olev VarChar(50),shuidui VarChar(50),chuizhidui VarChar(50),offset Decimal(10,2),width VarChar(50),height VarChar(50),hendang VarChar(50),familyName VarChar(50))";
                    break;
            }
            try
            {
                using (MySqlCommand cmdb = new MySqlCommand(usedata, conn))
                {
                    cmdb.ExecuteNonQuery();
                }
                using (MySqlCommand cmd = new MySqlCommand(createStatement, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                TaskDialog.Show("建表成功", "建表成功");
                result = true;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("建表失败", "建表失败");
                TaskDialog.Show("建表失败的原因", ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 生成对应的表格
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dataBaseName"></param>
        /// <param name="dataTableName"></param>
        /// <returns></returns>
        //public static string MyTableName(int type,string dataBaseName,string dataTableName) {
        //    string createTableStatement=null;
        //    string createStatement = null;
        //    string usedata = "use " + dataBaseName;
        //    //"mytest";
        //    switch (type)
        //    {
        //        case 0:
        //            //"WallTable "
        //            createStatement = "CREATE TABLE " +
        //               dataTableName +
        //                "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,walltype VarChar(50),levelName VarChar(50),width Decimal(10,2),height Decimal(10,2))";
        //            break;
        //        case 1:
        //            //"FloorTable "
        //            createStatement = "CREATE TABLE " +
        //                dataTableName +
        //                "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,floorType VarChar(50),levelName VarChar(50),thick Decimal(10,2) ,area Decimal(10,2),offset Decimal(10,2))";
        //            break;
        //        case 2:
        //            //"DoorTable "
        //            createStatement = "CREATE TABLE " +
        //               dataTableName +
        //                "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,doortype VarChar(50),levelName VarChar(50),drma VarChar(50),kjma VarChar(50),width VarChar(50),bottomheight VarChar(50),height VarChar(50))";
        //            break;
        //        case 3:
        //            //"WindowTable "
        //            createStatement = "CREATE TABLE " +
        //                 dataTableName +
        //                "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,windowsType VarChar(50),levelName VarChar(50),outcha VarChar(50),incha VarChar(50),bolicha VarChar(50),width VarChar(50),bottomheight VarChar(50),height VarChar(50))";
        //            break;
        //        case 4:
        //            //"RoomTable "
        //            createStatement = "CREATE TABLE " +
        //                dataTableName +
        //                "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,number VarChar(50),name VarChar(50),levelName VarChar(50),bottomOffset Decimal(10,2),area Decimal(10,2),topoffset Decimal(10,2))";
        //            break;
        //        case 5:
        //            //"StructColumnTable "
        //            createStatement = "CREATE TABLE " +
        //                dataTableName +
        //                "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,columnType VarChar(50),category VarChar(50),type VarChar(50),material VarChar(50),width VarChar(50), height  VarChar(50), bottomLev VarChar(50), bottomOffset  VarChar(50), topLev VarChar(50), topOffset  VarChar(50),isMove VARCHAR(50))";
        //            break;
        //        case 6:
        //            //"BeamTable"
        //            createStatement = "CREATE TABLE " +
        //                dataTableName +
        //                " (id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,beamName VarChar(50),Olev VarChar(50),material VarChar(50),length Decimal(10,2),y_duizheng VarChar(50),y_offset Decimal(10,2),yz_duizheng VarChar(50),z_duizheng VarChar(50),z_offset Decimal(10,2))";
        //            break;
        //        case 7:
        //            //"DuctTable "
        //            createStatement = "CREATE TABLE " +
        //                dataTableName +
        //                "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,DuctName VarChar(50),Olev VarChar(50),Width  Decimal(10,2),Height Decimal(10,2),Offset Decimal(10,2),shuidui VarChar(50),chuizhidui VarChar(50),type VarChar(50))";
        //            break;
        //        case 8:
        //            //"PipeTable "
        //            createStatement = "CREATE TABLE " +
        //                 dataTableName +
        //                "(id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,pipeName VarChar(50),Olev VarChar(50),shuidui VarChar(50),chuizhidui VarChar(50),offset Decimal(10,2),slope VarChar(50),type VarChar(50),guanduan VarChar(50),r VarChar(50))";
        //            break;
        //        case 9:
        //            //"EletricalQTable"
        //            createStatement = "CREATE TABLE " +
        //                 dataTableName +
        //                " (id SMALLINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,categoryName VarChar(50),Olev VarChar(50),shuidui VarChar(50),chuizhidui VarChar(50),offset Decimal(10,2),width VarChar(50),height VarChar(50),hendang VarChar(50),familyName VarChar(50))";
        //            break;
        //    }
        //    return createStatement;
        //}
    
    }
}
