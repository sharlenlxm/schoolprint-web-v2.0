using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace DALService
{
    /// <summary>
    /// 数据库访问
    /// </summary>
    public class DbHelperSQL
    {
        public static string databaseName = "SchoolPrintSys";//数据库名
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="databaseName">数据库名</param>
        /// <returns>连接字符串</returns>
        public static string GetConnectionString()
        {
            //获取服务IP地址
            string ip = System.Configuration.ConfigurationManager.AppSettings["IP"].ToString();

            //拼接连接字符串
            string cntstr = "Data Source={0};Initial Catalog=SchoolPrintSys;User Id=taas;Password=123456;";
            return string.Format(cntstr, ip);
        }

        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <param name="sql">要执行的查询语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>查询到的阅读器</returns>
        public static SqlDataReader ExecuteDataReader(string sql, params SqlParameter[] pars)
        {
            SqlConnection cnt = new SqlConnection(GetConnectionString());
            SqlCommand cmd = new SqlCommand(sql, cnt);
            foreach (SqlParameter par in pars)
                cmd.Parameters.Add(par);

            SqlDataReader read;
            cnt.Open();
            read = cmd.ExecuteReader();

            return read;
        }

        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <param name="sql">要执行的查询语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>查询到的结果集</returns>
        public static DataSet Query(string sql, params SqlParameter[] pars)
        {
            SqlConnection cnt = new SqlConnection(GetConnectionString());
            SqlDataAdapter da = new SqlDataAdapter(sql, cnt);
            foreach (SqlParameter par in pars)
                da.SelectCommand.Parameters.Add(par);

            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 执行单项操作Insert,Update,Delete
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="pars">参数列表</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteSql(string sql, params SqlParameter[] pars)
        {
            SqlConnection cnt = new SqlConnection(GetConnectionString());
            SqlCommand cmd = new SqlCommand(sql, cnt);
            foreach (SqlParameter par in pars)
                cmd.Parameters.Add(par);

            int t = 0;
            try
            {
                cnt.Open();
                t = cmd.ExecuteNonQuery();
            }
            finally
            {
                if (cnt.State == ConnectionState.Open)
                    cnt.Close();
            }
            return t;
        }

        /// <summary>
        /// 获取单一数据库单元格数值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingle(string sql)
        {
            object result = new object();
            SqlConnection cnt = new SqlConnection(GetConnectionString());
            SqlCommand cmd = new SqlCommand(sql, cnt);
            SqlDataReader read;
            cnt.Open();
            read = cmd.ExecuteReader();
            if (read.Read())
            {
                if (read[0] != null)
                    result = read[0];
            }
            return result;
        }

        /// <summary>
        /// 判断制定查询是否存在结果
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public static bool Exists(string sql, params SqlParameter[] pars)
        {
            bool result = false;
            SqlConnection cnt = new SqlConnection(GetConnectionString());
            SqlCommand cmd = new SqlCommand(sql, cnt);
            foreach (SqlParameter par in pars)
                cmd.Parameters.Add(par);

            SqlDataReader read;
            cnt.Open();
            read = cmd.ExecuteReader();
            if (read.Read())
            {
                int cell = (int)read[0];
                if (cell != 0)
                    result = true;
            }
            return result;
        }
    }
}
