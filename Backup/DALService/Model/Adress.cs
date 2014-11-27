using System;
namespace DALService
{
	/// <summary>
	/// Adress:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Adress
	{
		public Adress()
		{}
		#region Model
		private long _id;
		private string _customerid;
		private long _addressid;
		private string _area;
		private string _building;
		private string _room;
		private string _phone;
		private string _name;
		/// <summary>
		/// 
		/// </summary>
		public long ID
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
		public long AddressId
		{
			set{ _addressid=value;}
			get{return _addressid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Area
		{
			set{ _area=value;}
			get{return _area;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Building
		{
			set{ _building=value;}
			get{return _building;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Room
		{
			set{ _room=value;}
			get{return _room;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		#endregion Model

	}
}

