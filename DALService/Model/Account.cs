using System;
namespace DALService
{
	/// <summary>
	/// Account:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserAccount
	{
        public UserAccount()
		{}
		#region Model
		private string _id;
		private string _customerid;
		private string _account= "NULL";
		private long _accountid;
		/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustomerId
		{
			set{ _customerid=value;}
			get{return _customerid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Account
		{
			set{ _account=value;}
			get{return _account;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long AccountId
		{
			set{ _accountid=value;}
			get{return _accountid;}
		}
		#endregion Model

	}
}

