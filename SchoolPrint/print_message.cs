using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using Ppt = Microsoft.Office.Interop.PowerPoint;
using System.Data.SQLite;
using System.IO;

namespace SchoolPrint
{
    public partial class print_message : UserControl
    {
        private frmMain FatherForm;
        private string Building;
        private string Name;
        //private double Money;
        private string Money;
        private string filePathGloble = System.Windows.Forms.Application.StartupPath+"\\文件数据\\";
        private string UserClass;

        static public string dayAddress;
        static public string check;

        private int TotalOrderNumber = 0;
        private int HandledOrderNumber = 0;

        public print_message(string building,string name,frmMain fatherForm,string userClass)
        {
            InitializeComponent();

            label1.Text = building + " " + name;

            this.FatherForm = fatherForm;
            this.Building = building;
            this.Name = name;
            this.UserClass = userClass;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Money = FatherForm.AllMoney(Building+Name);
            //if (Money == -100.0)
            //    return;
            SQLiteConnection connectionToDatabase = null;
            SQLiteCommand command = null;
            string fileAddress = "";
            if (UserClass == "7000")
                fileAddress = dayAddress + "\\everydayFile.db3";
            else if (UserClass == "7001")
                fileAddress = dayAddress + "\\everydayFile_VIP.db3";
            if (!File.Exists(fileAddress))
            {
                MessageBox.Show("没有已打印信息！");
                return;
            }
            if (CheckOrderHandled() > 0)
            {
                DialogResult result = MessageBox.Show("还有订单未处理，是否继续？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                    return;
            }
            connectionToDatabase = new SQLiteConnection("Data Source=" + fileAddress);
            connectionToDatabase.Open();
            command = new SQLiteCommand();
            command.CommandText = "select sum(TotalMoney) from printFiles where Address = '" + Building + "' and UserInformation = '" + Name + "'" + " and HasPrinted = 0";
            command.Connection = connectionToDatabase;
            SQLiteDataAdapter adapters = new SQLiteDataAdapter(command);
            DataTable userTable = new DataTable();
            adapters.Fill(userTable);
            connectionToDatabase.Close();
            if (userTable.Rows.Count <= 0 || userTable.Rows[0][0].ToString() == "")
            {
                MessageBox.Show("未查到该用户打印信息！");
                return;
            }
            Money = userTable.Rows[0][0].ToString();
            #region print_message
            string filepath = filePathGloble + "dower.doc";
            object fileobj = filepath;
            object nullobj = System.Reflection.Missing.Value;
            object missing = System.Reflection.Missing.Value;
            object bookmark = "dower";
            object save = false;
            Word.ApplicationClass app = new Word.ApplicationClass();
            Word.Document doc = null;
            try
            {
                doc = app.Documents.Open(ref fileobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj);
                app.Visible = false;
                if (doc.Bookmarks.Exists("dower"))
                {
                    //doc.Bookmarks.get_Item(ref bookmark).Range.Text = Money.ToString() + "元";
                    //doc.Bookmarks.get_Item(ref bookmark).Range.Text = Building + "\r\n";
                    //doc.Bookmarks.get_Item(ref bookmark).Range.Text = Name + "\r\n";
                    connectionToDatabase = new SQLiteConnection("Data Source=" + fileAddress);
                    connectionToDatabase.Open();
                    command = new SQLiteCommand();
                    command.CommandText = "select OrderId,Message from printFiles where Address = '" + Building + "' and UserInformation = '" + Name + "' and HasPrinted = 0";
                    command.Connection = connectionToDatabase;
                    adapters = new SQLiteDataAdapter(command);
                    userTable = new DataTable();
                    adapters.Fill(userTable);
                    connectionToDatabase.Close();
                    for (int i = 0; i < userTable.Rows.Count; i++)
                    {
                        if (i != 0)
                            doc.Bookmarks.get_Item(ref bookmark).Range.Text = "订单" + userTable.Rows[i]["OrderId"].ToString() + ":" + userTable.Rows[i]["Message"].ToString() + "\n";
                        else
                            doc.Bookmarks.get_Item(ref bookmark).Range.Text = "订单" + userTable.Rows[i]["OrderId"].ToString() + ":" + userTable.Rows[i]["Message"].ToString();
                    }
                    doc.Bookmarks.get_Item(ref bookmark).Range.Text = Building + "  " + Name + "  " + Money + "元\n";
                    doc.PrintOut(ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (doc != null)
                    //C#读取word文件之关闭文件  
                    doc.Close(ref save, ref nullobj, ref nullobj);
                if (app != null)
                    //C#读取word文件之关闭COM  
                    app.Quit(ref nullobj, ref nullobj, ref nullobj);

                connectionToDatabase = new SQLiteConnection("Data Source=" + fileAddress);
                connectionToDatabase.Open();
                command = new SQLiteCommand();
                for (int i = 0; i < userTable.Rows.Count; i++)
                {
                    command.CommandText = "update printFiles set HasPrinted = 1 where OrderId = '" + userTable.Rows[i]["OrderId"].ToString() + "' and Address = '" + Building + "' and UserInformation = '" + Name + "'";
                    command.Connection = connectionToDatabase;
                    command.ExecuteNonQuery();
                }
                connectionToDatabase.Close();
            }
            #endregion
        }

        private int CheckOrderHandled()
        {
            return TotalOrderNumber - HandledOrderNumber;
        }

        public void color_set(Color set)
        {
            this.BackColor = set;
        }

        public void SetTotalOrderNumber(int number)
        {
            TotalOrderNumber = number;
        }

        public void SetHandleOrderNumber()
        {
            HandledOrderNumber++;
        }

        public void DeleteOrderNumber()
        {
            TotalOrderNumber--;
        }

        public int CheckOrderRemaind()
        {
            if ((HandledOrderNumber == TotalOrderNumber) && (TotalOrderNumber != 0))
                return 100;
            else
                return TotalOrderNumber;
        }
    }
}
