using System;
namespace DALService
{
	/// <summary>
	/// Document:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Document
	{
		public Document()
		{}
		#region Model
		private long _id;
		private string _customerid;
		private long _addressid;
		private string _shoperid;
		private string _documentname;
		private string _sendway;
		private string _state;
		private string _address;
		private string _papertype;
		private string _copies;
		private string _printmode;
		private string _remark;
		private string _cost;
		private DateTime _updatetime;
		private DateTime _suretime;
		private string _documenturl;
        private string _name;
        private string _shopname;
        private string _phone;
        private string _checkdate;
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
		public string ShoperId
		{
			set{ _shoperid=value;}
			get{return _shoperid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DocumentName
		{
			set{ _documentname=value;}
			get{return _documentname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sendWay
		{
			set{ _sendway=value;}
			get{return _sendway;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PaperType
		{
			set{ _papertype=value;}
			get{return _papertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Copies
		{
			set{ _copies=value;}
			get{return _copies;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PrintMode
		{
			set{ _printmode=value;}
			get{return _printmode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Cost
		{
			set{ _cost=value;}
			get{return _cost;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime SureTime
		{
			set{ _suretime=value;}
			get{return _suretime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DocumentUrl
		{
			set{ _documenturl=value;}
			get{return _documenturl;}
		}

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ShopName
        {
            set { _shopname = value; }
            get { return _shopname; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }

        public string CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
		#endregion Model

	}
}

