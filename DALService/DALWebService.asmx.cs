using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace DALService
{
    /// <summary>
    /// DALWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://202.114.18.229:2000")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class DALWebService : System.Web.Services.WebService
    {

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="customerId">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>登录是否成功</returns>
        [WebMethod]
        public bool Login(string CustomerId, string password)
        {
            return ServiceFunction.Login(CustomerId, password);
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [WebMethod]
        public List<Document> GetDocList(string shoperId)
        {
            return ServiceFunction.GetDocList(shoperId);
        }
        /// <summary>
        /// 确认文件已下载
        /// </summary>
        /// <param name="documentId">文件在数据库中对于的id</param>
        /// <returns>确认是否成功</returns>
        [WebMethod]
        public bool EnsureDownload(int ID,string state)
        {
            return ServiceFunction.EnsureDownload(ID,state);
        }

        /// <summary>
        ///   提交确认时间
        /// </summary>
        /// <param name="documentId">文件在数据库中对于的id</param>
        /// <param name="time">文件确认的时间</param>
        /// <returns>操作是否成功</returns>
        [WebMethod]
        public bool UpTime(int ID, string time)
        {
            return ServiceFunction.UpTime(ID, time);
        }

        /// <summary>
        ///   提交每个文档的金额
        /// </summary>
        /// <param name="documentId">文件在数据库中对应的ID</param>
        /// <param name="money">每个文件对应的钱</param>
        /// <return>操作是否成功</return>
        [WebMethod]
        public bool UpMoney(int ID, string money)
        {
            return ServiceFunction.UpMoney(ID, money);
        }
        
        /// <summary>
        /// 超级用户管理
        /// </summary>
        /// <param name="check">当天日期</param>
        /// <returns>符合要求的数据集</returns>
        [WebMethod]
        public List<Document> SuperCheck(string check)
        {
            return ServiceFunction.SuperCheck(check);
        }

        /// <summary>
        /// 确认文件属于哪一天
        /// </summary>
        /// <param name="documentId">文件在数据库中对于的id</param>
        /// <returns>确认是否成功</returns>
        [WebMethod]
        public bool EnsureCheck(int ID, string check)
        {
            return ServiceFunction.EnsureCheck(ID, check);
        }
    }
}
