using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DALService
{
    public class ServiceFunction
    {
        /// <summary>
        /// 登录验证，存在此id和密码就登录验证成功
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Login(string CustomerId, string Password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Customer");//??
            strSql.Append(" where CustomerId=@CustomerId and Password=@Password");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerId", SqlDbType.NVarChar,50)	,
                    new SqlParameter("@Password", SqlDbType.NVarChar,50)};
            parameters[0].Value = CustomerId;
            parameters[1].Value = Password;

            bool result = DbHelperSQL.Exists(strSql.ToString(), parameters);
            return result;
        }
        /// </summary>
        /// <param name="username">用户id</param>
        /// <returns>返回结果集</returns>

        public static List<Document> GetDocList(string shoperId)
        {
            List<Document> list = new List<Document>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Document");
            strSql.Append(" where ShoperId=@ShoperId");
            SqlParameter[] parameters = {
					new SqlParameter("@ShoperId", SqlDbType.NVarChar,50)};
            parameters[0].Value = shoperId;

            DataSet result = DbHelperSQL.Query(strSql.ToString(), parameters);
            DataTable table = result.Tables[0];
            int count = table.Rows.Count;
            string[] CustomerId = new string[count];
            string[] DocumentURL = new string[count];
            string[] DocumentId = new string[count];
            string[] DocumentName = new string[count];
            string[] DocumentType = new string[count];
            string[] DocumentWay = new string[count];
            string[] DocumentState = new string[count];
            string[] DocumentAddress = new string[count];
            string[] ShoperId = new string[count];
            string[] DocumentNum = new string[count];
            string[] SubmitTime = new string[count];
            foreach (DataRow row in table.Rows)
            {
                Document doc = new Document();
                doc.ID = Convert.ToInt64(row["ID"].ToString());
                doc.CustomerId = row["CustomerId"].ToString();

                doc.AddressId = Convert.ToInt64(row["AddressId"].ToString());
                doc.ShoperId = row["ShoperId"].ToString();
                doc.DocumentName = row["DocumentName"].ToString();
                doc.sendWay = row["sendWay"].ToString();
                doc.State = row["State"].ToString();
                doc.address = row["address"].ToString();
                doc.PaperType = row["PaperType"].ToString();
                doc.Copies = row["Copies"].ToString();
                doc.PrintMode = row["PrintMode"].ToString();
                doc.Remark = row["Remark"].ToString();
                doc.Cost = row["Cost"].ToString();
                if (row["UpdateTime"] == System.DBNull.Value)
                    //doc.UpdateTime = DateTime.Parse("2000-00-00 00:00:00");
                    ;
                else
                    doc.UpdateTime = Convert.ToDateTime(row["UpdateTime"]);
                if (row["SureTime"] == System.DBNull.Value)
                    //doc.SureTime = DateTime.Parse("2000-00-00 00:00:00");
                    ;
                else
                    doc.SureTime = Convert.ToDateTime(row["SureTime"]);
                doc.DocumentUrl = row["CustomerId"].ToString();
                doc.Name = row["Name"].ToString();
                doc.Phone = row["Phone"].ToString();
                doc.CheckDate = row["CheckDate"].ToString();
                list.Add(doc);
            }
            return list;
        }

        /// <summary>
        /// 确认文件已下载
        /// </summary>
        /// <param name="ID">文件在数据库中对于的id</param>
        /// <param name="check">确认时间，便于管理员查询</param>
        /// <returns>确认是否成功</returns>
        public static bool EnsureDownload(int ID,string state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Document set State=@State");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@State", SqlDbType.NVarChar,50),
                    new SqlParameter("@ID", SqlDbType.BigInt,64)};
            parameters[0].Value = state;
            parameters[1].Value = ID;//此处填写表示已下载的状态字符串，具体得问陈楷佳或黄凯;

            int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (result > 0)
                return true;
            else 
                return false;
        }

        /// <summary>
        ///   提交确认时间
        /// </summary>
        /// <param name="ID">文件在数据库中对于的id</param>
        /// <param name="time">文件确认的时间</param>
        /// <returns>操作是否成功</returns>
        public static bool UpTime(int ID, string time)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Document set SureTime=@SureTime");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters ={
                    new SqlParameter("@SureTime", SqlDbType.NVarChar,50),
                    new SqlParameter("@ID",SqlDbType.BigInt,64)};
            parameters[0].Value = time;
            parameters[1].Value = ID;

            int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        ///   提交每个文档的金额
        /// </summary>
        /// <param name="ID">文件在数据库中对应的ID</param>
        /// <param name="money">每个文件对应的钱</param>
        /// <return>操作是否成功</return>
        public static bool UpMoney(int ID, string money)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Document set Cost=@Cost");
            strSql.Append(" Where ID=@ID");
            SqlParameter[] parameters ={
                    new SqlParameter("@Cost", SqlDbType.NVarChar,50),
                    new SqlParameter("ID",SqlDbType.BigInt,64)};
            parameters[0].Value = money;
            parameters[1].Value = ID;

            int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 超级用户管理
        /// </summary>
        /// <param name="check">当天的时间</param>
        /// <returns>当天所有文件的数据集</returns>
        public static List<Document> SuperCheck(string check)
        {
            List<Document> list = new List<Document>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Document");
            strSql.Append(" where CheckDate=@CheckDate");
            SqlParameter[] parameters = {
					new SqlParameter("@CheckDate", SqlDbType.NVarChar,50)};
            parameters[0].Value = check;

            DataSet result = DbHelperSQL.Query(strSql.ToString(), parameters);
            DataTable table = result.Tables[0];
            int count = table.Rows.Count;
            string[] CustomerId = new string[count];
            string[] DocumentURL = new string[count];
            string[] DocumentId = new string[count];
            string[] DocumentName = new string[count];
            string[] DocumentType = new string[count];
            string[] DocumentWay = new string[count];
            string[] DocumentState = new string[count];
            string[] DocumentAddress = new string[count];
            string[] ShoperId = new string[count];
            string[] DocumentNum = new string[count];
            string[] SubmitTime = new string[count];
            foreach (DataRow row in table.Rows)
            {
                Document doc = new Document();
                doc.ID = Convert.ToInt64(row["ID"].ToString());
                doc.CustomerId = row["CustomerId"].ToString();
                doc.AddressId = Convert.ToInt64(row["AddressId"].ToString());
                doc.ShoperId = row["ShoperId"].ToString();
                doc.DocumentName = row["DocumentName"].ToString();
                doc.sendWay = row["sendWay"].ToString();
                doc.State = row["State"].ToString();
                doc.address = row["address"].ToString();
                doc.PaperType = row["PaperType"].ToString();
                doc.Copies = row["Copies"].ToString();
                doc.PrintMode = row["PrintMode"].ToString();
                doc.Remark = row["Remark"].ToString();
                doc.Cost = row["Cost"].ToString();
                if (row["UpdateTime"] == System.DBNull.Value)
                    //doc.UpdateTime = DateTime.Parse("2000-00-00 00:00:00");
                    ;
                else
                    doc.UpdateTime = Convert.ToDateTime(row["UpdateTime"]);
                if (row["SureTime"] == System.DBNull.Value)
                    //doc.SureTime = DateTime.TryParse("2000-00-00 00:00:00");
                    ;
                else
                    doc.SureTime = Convert.ToDateTime(row["SureTime"]);
                doc.DocumentUrl = row["CustomerId"].ToString();
                doc.Name = row["Name"].ToString();
                doc.Phone = row["Phone"].ToString();
                doc.CheckDate = row["CheckDate"].ToString();
                list.Add(doc);
            }
            return list;
        }

        /// <summary>
        /// 确认文件属于哪一天
        /// </summary>
        /// <param name="ID">文件在数据库中对于的id</param>
        /// <param name="check">确认时间，便于管理员查询</param>
        /// <returns>确认是否成功</returns>
        public static bool EnsureCheck(int ID, string check)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Document set CheckDate=@CheckDate");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@CheckDate", SqlDbType.NVarChar,50),
                    new SqlParameter("@ID", SqlDbType.BigInt,64)};
            parameters[0].Value = check;
            parameters[1].Value = ID;//此处填写表示已下载的状态字符串，具体得问陈楷佳或黄凯;

            int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (result > 0)
                return true;
            else
                return false;
        }

    }
}
