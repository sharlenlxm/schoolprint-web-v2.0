using System;

	/// <summary>
	/// Document:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Document
	{
		public Document()
		{}
		#region Model
		private string Tid;
		private string _customerid;
		private long _addressid;
		private string _shoperid;
		private string File_name;
		private string _sendway;
		private string _state;
		private string _address;
		private string _papertype;
		private string _copies;
		private string _printmode;
		private string _remark;
		private string _cost;
        //private DateTime Upload_time;
        private string Upload_time;
		private DateTime _suretime;
		private string File_url;
        private string _name;
        private string _shopname;
        private string _phone;
        private string _checkdate;
        private string File_msg;
        private string File_others;
        private string Send_store;
        private string Massage;
        private string Send_status;
        private string Loc;
        private string User;
        private string Send_time;
        private string User_id;
        private string Member_type;

        public string user_id
        {
            set { User_id = value; }
            get { return User_id; }
        }

        public string member_type
        {
            set { Member_type = value; }
            get { return Member_type; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string tid
		{
			set{ Tid=value;}
			get{return Tid;}
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
		public string file_name
		{
			set{ File_name=value;}
			get{return File_name;}
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
		public string upload_time
		{
			set{ Upload_time=value;}
			get{return Upload_time;}
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
		public string file_url
		{
			set{ File_url=value;}
            get { return "http://www.xiaoyintong.com/" + File_url; }
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

        public string file_msg
        {
            set { File_msg = value; }
            get { return File_msg; }
        }

        public string file_others
        {
            set { File_others = value; }
            get { return File_others; }
        }

        public string send_store
        {
            set { Send_store = value; }
            get { return Send_store; }
        }

        public string message
        {
            set { Massage = value; }
            get { return Massage; }
        }

        public string send_status
        {
            set { Send_status = value; }
            get { return Send_status; }
        }

        public string loc
        {
            set { Loc = value; }
            get { return Loc.Replace(",", " "); }
        }

        public string user
        {
            set { User = value; }
            get { return User; }
        }

        public string send_time
        {
            set { Send_time = value; }
            get { return Send_time; }
        }

		#endregion Model

	}

