using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SchoolPrint
{
    public partial class Order : UserControl
    {
        static public string dayAddress;
        static public string check;

        const string NOSHOW = "100";
        const string DELETE = "101";
        const string INPRINT = "102";
        const string HAVEPRINT = "103";

        private string Printer;
        private string Message;
        private string UserAddress;
        private string UserInformation;
        private string Files;
        private int OrderNumber;
        private string OrderId;
        private string ClassNumber;
        private frmMain FatherForm;
        private string IsPrinting;

        public Order(frmMain fatherForm, Color backColor, string orderId, int order_number, string printer, string message, string user_address, string user_information, string files, string classNumber)
        {
            InitializeComponent();

            this.BackColor = backColor;
            label1.Text = label1.Text + orderId;
            label2.Text = printer + " " + message;

            this.Printer = printer;
            this.Message = message;
            this.UserAddress = user_address;
            this.UserInformation = user_information;
            this.Files = files;
            this.OrderNumber = order_number;
            this.OrderId = orderId;
            this.ClassNumber = classNumber;
            this.FatherForm = fatherForm;
            //this.IsPrinting = isPrinting;
            this.IsPrinting = "0";
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Printer + "\n" + Message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox1.Enabled)
            {
                MessageBox.Show("已上传了金额，不能修改！");
                return;
            }
            SQLiteConnection connectionToDatabase = null;
            SQLiteCommand command = null;
            double tryMoney;
            if (!double.TryParse(textBox1.Text, out tryMoney))
            {
                MessageBox.Show("请输入金额，并且金额只能为数字！");
                return;
            }
            if (FatherForm.AllMoney(OrderId, UserAddress, UserInformation, ClassNumber) == -100.0)
                return;
            if (tryMoney == 0.0)
            {
                MessageBox.Show("订单金额不能为0！");
                return; 
            }
            else if (tryMoney == -1.0)
            {
                DialogResult result = MessageBox.Show("确定订单为问题订单？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                    return;
            }
            //string jsonText = FatherForm.PostDataToUrl("order_tid=" + OrderId + "&money=" + tryMoney, "http://api.xiaoyintong.dev:8000/api/v1/desktop/order/printing/commit");
            string jsonText = FatherForm.PostDataToUrl("order_tid=" + OrderId + "&money=" + tryMoney, "http://api.xiaoyintong.com/api/v1/desktop/order/printing/commit");
            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
            string message = jo["message"].ToString(); ;
            bool status = (bool)jo["success"];
            //string message = "test";
            //bool status = true;
            if (!status)
            {
                MessageBox.Show(message);
                return;
            }

            if (ClassNumber == "7000")
            {
                if (!File.Exists(dayAddress + "\\everydayFile.db3"))
                {
                    SQLiteConnection.CreateFile(dayAddress + "\\everydayFile.db3");
                    connectionToDatabase = new SQLiteConnection("Data Source=" + dayAddress + "\\everydayFile.db3");
                    connectionToDatabase.Open();
                    command = new SQLiteCommand();
                    command.CommandText = "create table printFiles(Address char(15), UserInformation char(20), OrderNumber int, OrderId char(10), Printing char(50), Message vchar(100), Files vchar(2000), TotalMoney double, HasPrinted bool)";
                    command.Connection = connectionToDatabase;
                    command.ExecuteNonQuery();
                    connectionToDatabase.Close();
                }
                connectionToDatabase = new SQLiteConnection("Data Source=" + dayAddress + "\\everydayFile.db3");
                connectionToDatabase.Open();
                command = new SQLiteCommand();
                command.CommandText = "insert into printFiles values('" + UserAddress + "','" + UserInformation + "','" + OrderNumber + "','" + OrderId + "','" + Printer + "','" + Message + "','" + Files.Replace("'", "\"") + "'," + tryMoney + ",0)";
                command.Connection = connectionToDatabase;
                command.ExecuteNonQuery();
                connectionToDatabase.Close();
            }
            else
            {
                if (!File.Exists(dayAddress + "\\everydayFile_VIP.db3"))
                {
                    SQLiteConnection.CreateFile(dayAddress + "\\everydayFile_VIP.db3");
                    connectionToDatabase = new SQLiteConnection("Data Source=" + dayAddress + "\\everydayFile_VIP.db3");
                    connectionToDatabase.Open();
                    command = new SQLiteCommand();
                    command.CommandText = "create table printFiles(Address char(15), UserInformation char(20), OrderNumber int, OrderId char(10), Printing char(50), Message vchar(100), Files vchar(2000), TotalMoney double, HasPrinted bool)";
                    command.Connection = connectionToDatabase;
                    command.ExecuteNonQuery();
                    connectionToDatabase.Close();
                }
                connectionToDatabase = new SQLiteConnection("Data Source=" + dayAddress + "\\everydayFile_VIP.db3");
                connectionToDatabase.Open();
                command = new SQLiteCommand();
                command.CommandText = "insert into printFiles values('" + UserAddress + "','" + UserInformation + "','" + OrderNumber + "','" + OrderId + "','" + Printer + "','" + Message + "','" + Files.Replace("'", "\"") + "'," + tryMoney + ",0)";
                command.Connection = connectionToDatabase;
                command.ExecuteNonQuery();
                connectionToDatabase.Close();
            }

            //修改左边金额
            string ClassLevel;
            double classMoney = 0.0, historyMoney = 0.0;
            if(ClassNumber == "7000")
                ClassLevel = "nomal";
            else
                ClassLevel = "VIP";

            connectionToDatabase = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\文件数据\\AllReference");
            connectionToDatabase.Open();
            SQLiteCommand cmd = connectionToDatabase.CreateCommand();
            cmd.CommandText = "select " + ClassLevel + ",history from money";
            SQLiteDataReader reader = cmd.ExecuteReader();  
            if (reader.HasRows)  
            {
                reader.Read();
                classMoney = reader.GetDouble(0);
                historyMoney = reader.GetDouble(1);
            }
            connectionToDatabase.Close();

            classMoney += tryMoney;
            historyMoney += tryMoney;

            connectionToDatabase = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\文件数据\\AllReference");
            connectionToDatabase.Open();
            command = new SQLiteCommand();
            command.CommandText = "update money set " + ClassLevel + "=" + classMoney + ", history=" + historyMoney;
            command.Connection = connectionToDatabase;
            command.ExecuteNonQuery();
            connectionToDatabase.Close();

            if (ClassNumber == "7000")
                FatherForm.label6.Text = classMoney.ToString();
            else
                FatherForm.label18.Text = classMoney.ToString();
            FatherForm.label8.Text = historyMoney.ToString();
            textBox1.Enabled = false;
            
            //设置print_message中handledOrderNumber
            FatherForm.SetHandledOrderNumber(ClassLevel, UserAddress, UserInformation);
        }

        public bool ChangeMoney(double lastMoney, double nowMoney)
        {
            if (textBox1.Enabled == false)
            {
                MessageBox.Show("金额已上传，不能修改！");
                return true;
            }
            double allMoney;
            if (!double.TryParse(textBox1.Text, out allMoney))
                allMoney = 0.0;
            allMoney = allMoney - lastMoney + nowMoney;
            textBox1.Text = allMoney.ToString();
            return true;
        }

        bool IsFirst = true;

        public bool CheckOrder(string classLevel)
        {
            if (!IsFirst)
                return true;
            else if (IsPrinting == "1")
                return true;
            //string jsonText = FatherForm.PostDataToUrl("order_tid=" + OrderId, "http://api.xiaoyintong.dev:8000/api/v1/desktop/order/waiting/commit");
            string jsonText = FatherForm.PostDataToUrl("order_tid=" + OrderId, "http://api.xiaoyintong.com/api/v1/desktop/order/waiting/commit");
            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
            string message = jo["message"].ToString(); ;
            bool status = (bool)jo["success"];
            //string message = "test";
            //bool status = true;
            if (status)
            {
                IsFirst = false;
                return true;
            }
            else
            {
                string messageStatus = message.Substring(0, message.IndexOf('$'));
                switch (messageStatus)
                {
                    case NOSHOW:
                        MessageBox.Show(message);
                        return false;
                    case DELETE:
                        MessageBox.Show(message);
                        return false;
                    case INPRINT:
                        return true;
                    case HAVEPRINT:
                        MessageBox.Show(message);
                        return false;
                }
            }
            return false;
        }
    }
}
