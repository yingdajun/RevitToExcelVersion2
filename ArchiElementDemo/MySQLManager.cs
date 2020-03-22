using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System;
using MySql.Data.MySqlClient;
namespace ArchiElementDemoDemo
{
     public class MySQLManager
    {
       
        /// <summary>
        /// 创建数据库
        /// </summary>
        public static void CreateMysqlDB(string databaseName)
        {
            MySqlConnection conn = new MySqlConnection("Data Source=localhost;Persist Security Info=yes; " +
                                                   "UserId=root; PWD=123456");
            
            string sqlCom = "CREATE DATABASE foxDemo;";
            string useCom = sqlCom.Replace("foxDemo",databaseName);
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

        /// <summary>
        /// 建表
        /// </summary>
        public static void AlterTableExample()
        {
            string connStr = "server=localhost;port=3306;database=123;user=root;password=123456;";
            string createStatement = "CREATE TABLE People (Name VarChar(50), Age Integer)";
            string alterStatement = "ALTER TABLE People ADD Sex Boolean";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                //防止第二次启动时再次新建数据表
                try
                {
                    // 建表  
                    using (MySqlCommand cmd = new MySqlCommand(createStatement, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // 改表或者增加行  
                    using (MySqlCommand cmd = new MySqlCommand(alterStatement, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    Console.WriteLine("建表成功");
                }
                catch (Exception)
                {
                    Console.WriteLine("建表失败，已存在");
                    //throw;
                }
                //防止第二次启动时再次新建数据表

            }
        }
    }

}

