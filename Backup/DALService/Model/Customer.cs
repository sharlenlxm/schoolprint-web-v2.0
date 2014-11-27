using System;
namespace DALService
{
	/// <summary>
	/// Customer:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Customer
	{
		public Customer()
		{}
		#region Model
		private string _id;
		private string _customerid;
		private string _password;
		private string _pemission= "0";
		private string _ifchecked= "0";
		private string _school;
		private string _shopername;
		private string _checknum;
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
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pemission
		{
			set{ _pemission=value;}
			get{return _pemission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IfChecked
		{
			set{ _ifchecked=value;}
			get{return _ifchecked;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string School
		{
			set{ _school=value;}
			get{return _school;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShoperName
		{
			set{ _shopername=value;}
			get{return _shopername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckNum
		{
			set{ _checknum=value;}
			get{return _checknum;}
		}
		#endregion Model

	}
}

