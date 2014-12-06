using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SchoolPrintDomin;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Data.SQLite;

namespace SchoolPrint
{
    public partial class frmMain : Form
    {
        private double sum;
        const int MAXSIZE=100;
        //private string dayAddress = Application.StartupPath + "\\文件数据\\" + DateTime.Now.AddDays(1).ToLongDateString();

        //static DALService.DALWebService service = new DALService.DALWebService();
        //static WebReference.DALWebService service = new WebReference.DALWebService();
        //static DominInfo dominInfo = new DominInfo();
        private string dayAddress;
        private string check;
        public string uid;
        public static frmMain fatherForm;
        public double Sum
        {
            get
            {
                return sum;
            }
            set
            {
                sum = value;
            }
        }
        public frmMain()
        {
            InitializeComponent();
            button1.Text = "登录系统";
            //button1.Text = "刷新";
            //button1.Text = "刷新";
            label1.Text = "打印店：未登录";
            label5.Text = sum.ToString() + "元";
            //MessageBox.Show(times);
            //service.Credentials = new NetworkCredential(dominInfo.UserName, dominInfo.Password, dominInfo.Domin);
            fatherForm = this;
            try
            {
                StreamReader swr = File.OpenText(Application.StartupPath + "\\文件数据\\test");
                string name = swr.ReadLine();
                string password = swr.ReadLine();
                swr.Close();
                textBox1.Text = name;
                textBox2.Text = password;
            }
            catch 
            {
                ;
            }
            if (Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\temp"))
                Directory.Delete(System.Windows.Forms.Application.StartupPath + "\\temp", true);
        }

        //private ListboxItem[] item = new ListboxItem[500];
        private ListboxItem[] item;

        private void frmMain_Load(object sender, EventArgs e)
        {
            //初始化文档以及总金额
            if (!Directory.Exists(Application.StartupPath + "\\文件数据\\" + DateTime.Now.AddDays(1).ToLongDateString()))
            {
                if (!Directory.Exists(Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToLongDateString().ToString()))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToLongDateString().ToString());
                }
                dayAddress = Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToLongDateString().ToString();
                check = DateTime.Now.ToLongDateString().ToString();
                ListboxItem.dayAddress = dayAddress;
                ListboxItem.check = check;
                Order.dayAddress = dayAddress;
                Order.check = check;
                print_message.dayAddress = dayAddress;
                print_message.check = check;
            }
            else
            {
                dayAddress = Application.StartupPath + "\\文件数据\\" + DateTime.Now.AddDays(1).ToLongDateString();
                check = DateTime.Now.AddDays(1).ToLongDateString();
                ListboxItem.dayAddress = dayAddress;
                ListboxItem.check = check;
                Order.dayAddress = dayAddress;
                Order.check = check;
                print_message.dayAddress = dayAddress;
                print_message.check = check;
            }
            
            //#region
            //try
            //{
            //    /*从加密文件恢复原文件*/
            //    StreamReader srs = File.OpenText(dayAddress + "\\" + "今日打印.la");
            //    string temps = srs.ReadToEnd();
            //    srs.Close();
            //    byte[] changs = Encoding.Unicode.GetBytes(temps);

            //    for (int i = 0; i < changs.Length; i++)
            //    {
            //        changs[i] -= 21;
            //    }
            //    temps = Encoding.Unicode.GetString(changs);
            //    StreamWriter srsw = new StreamWriter(dayAddress + "\\" + "今日打印情况.txt", false);
            //    srsw.Write(temps);
            //    srsw.Close();
            //    /****************************************/
            //    if (File.Exists(dayAddress + "\\" + "今日打印情况.txt"))
            //    {
            //        StreamReader sr = File.OpenText(dayAddress + "\\" + "今日打印情况.txt");
            //        label5.Text = sr.ReadLine();
            //        sr.Close();
            //        label5.Text = label5.Text.Substring(label5.Text.IndexOf("：") + 1);
            //        sum = Convert.ToDouble(label5.Text.Substring(0, label5.Text.IndexOf("元")));
            //    }
            //    else
            //    {
            //        label5.Text = label19.Text = "0元";
            //    }

            //    if (File.Exists(dayAddress + "\\" + "今日打印情况_vip.txt"))
            //    {
            //        StreamReader sr_2 = File.OpenText(dayAddress + "\\" + "今日打印情况_vip.txt");
            //        label19.Text = sr_2.ReadLine();
            //        sr_2.Close();
            //        label19.Text = label19.Text.Substring(label19.Text.IndexOf("：") + 1);
            //        sum = Convert.ToDouble(label19.Text.Substring(0, label19.Text.IndexOf("元")));
            //    }
            //    else
            //    {
            //        label19.Text = label19.Text = "0元";
            //    }

            //    /*********************************************/
            //    if (!File.Exists(Application.StartupPath + "\\文件数据\\money"))
            //    {
            //        StreamWriter a1=File.CreateText(Application.StartupPath + "\\文件数据\\money");
            //        a1.Close();
            //    }
            //    StreamReader sr2 = File.OpenText(Application.StartupPath + "\\文件数据\\money");
            //    label6.Text = sr2.ReadLine();
            //    sr2.Close();
            //    if (label6.Text != "")
            //        //sum = Convert.ToDouble(label6.Text);
            //        ;
            //    else
            //    {
            //        //sum = 0;
            //        label6.Text = "0";
            //    }
            //    label6.Text += "元";

            //    /***********************************/
            //    if (!File.Exists(Application.StartupPath + "\\文件数据\\money2"))
            //    {
            //        StreamWriter a1 = File.CreateText(Application.StartupPath + "\\文件数据\\money2");
            //        a1.Close();
            //    }
            //    StreamReader sr3 = File.OpenText(Application.StartupPath + "\\文件数据\\money2");
            //    label8.Text = sr3.ReadLine();
            //    sr3.Close();
            //    if (label8.Text != "")
            //        //sum = Convert.ToDouble(label8.Text);
            //        ;
            //    else
            //    {
            //        //sum = 0;
            //        label8.Text = "0";
            //    }
            //    label8.Text += "元";

            //    /*******************************************/
            //    if (!File.Exists(Application.StartupPath + "\\文件数据\\money3"))
            //    {
            //        StreamWriter a1 = File.CreateText(Application.StartupPath + "\\文件数据\\money3");
            //        a1.Close();
            //    }
            //    StreamReader sr_single = File.OpenText(Application.StartupPath + "\\文件数据\\money3");
            //    label11.Text = sr_single.ReadLine();
            //    //sr_single.Close();
            //    if (label11.Text != "")
            //        //sum = Convert.ToDouble(label6.Text);
            //        ;
            //    else
            //    {
            //        //sum = 0;
            //        label11.Text = "0";
            //    }
            //    label11.Text += "元";
            //    label12.Text = sr_single.ReadLine();
            //    sr_single.Close();
            //    if (label12.Text != "")
            //        ;
            //    else
            //        label12.Text = "0";
            //    label12.Text += "元";

            //    /*******************************************************/
            //    if (!File.Exists(Application.StartupPath + "\\文件数据\\money4"))
            //    {
            //        StreamWriter a1 = File.CreateText(Application.StartupPath + "\\文件数据\\money4");
            //        a1.WriteLine();
            //        a1.WriteLine();
            //        a1.WriteLine();
            //        a1.Close();
            //    }
            //    StreamReader sr_Vip = File.OpenText(Application.StartupPath + "\\文件数据\\money4");
            //    label18.Text = sr_Vip.ReadLine() + "元";
            //    label15.Text = sr_Vip.ReadLine() + "元";
            //    label16.Text = sr_Vip.ReadLine() + "元";
            //    sr_Vip.Close();
            //}
            //catch 
            //{
            //    label5.Text = "0元";
            //    label19.Text = "0元";
            //    /*******************************************/
            //    if (!File.Exists(Application.StartupPath + "\\文件数据\\money"))
            //    {
            //        StreamWriter a1 = File.CreateText(Application.StartupPath + "\\文件数据\\money");
            //        a1.Close();
            //    }
            //    StreamReader sr = File.OpenText(Application.StartupPath + "\\文件数据\\money");
            //    label6.Text = sr.ReadLine();
            //    sr.Close();
            //    if (label6.Text != "")
            //        //sum = Convert.ToDouble(label6.Text);
            //        ;
            //    else
            //    {
            //        //sum = 0;
            //        label6.Text = "0";
            //    }
            //    label6.Text += "元";

            //    /***************************************/
            //    if (!File.Exists(Application.StartupPath + "\\文件数据\\money2"))
            //    {
            //        StreamWriter a1 = File.CreateText(Application.StartupPath + "\\文件数据\\money2");
            //        a1.Close();
            //    }
            //    StreamReader sr3 = File.OpenText(Application.StartupPath + "\\文件数据\\money2");
            //    label8.Text = sr3.ReadLine();
            //    sr3.Close();
            //    if (label8.Text != "")
            //        //sum = Convert.ToDouble(label8.Text);
            //        ;
            //    else
            //    {
            //        //sum = 0;
            //        label8.Text = "0";
            //    }
            //    label8.Text += "元";

            //    /*******************************************/
            //    if (!File.Exists(Application.StartupPath + "\\文件数据\\money3"))
            //    {
            //        StreamWriter a1 = File.CreateText(Application.StartupPath + "\\文件数据\\money3");
            //        a1.Close();
            //    }
            //    StreamReader sr_single = File.OpenText(Application.StartupPath + "\\文件数据\\money3");
            //    label11.Text = sr_single.ReadLine();
            //    //sr_single.Close();
            //    if (label11.Text != "")
            //        //sum = Convert.ToDouble(label6.Text);
            //        ;
            //    else
            //    {
            //        //sum = 0;
            //        label11.Text = "0";
            //    }
            //    label11.Text += "元";
            //    label12.Text = sr_single.ReadLine();
            //    sr_single.Close();
            //    if (label12.Text != "")
            //        ;
            //    else
            //        label12.Text = "0";
            //    label12.Text += "元";

            //    /*******************************************************/
            //    if (!File.Exists(Application.StartupPath + "\\文件数据\\money4"))
            //    {
            //        StreamWriter a1 = File.CreateText(Application.StartupPath + "\\文件数据\\money4");
            //        a1.WriteLine("0");
            //        a1.WriteLine("0");
            //        a1.WriteLine("0");
            //        a1.Close();
            //    }
            //    StreamReader sr_Vip = File.OpenText(Application.StartupPath + "\\文件数据\\money4");
            //    label18.Text = sr_Vip.ReadLine() + "元";
            //    label15.Text = sr_Vip.ReadLine() + "元";
            //    label16.Text = sr_Vip.ReadLine() + "元";
            //    sr_Vip.Close();
            //}
            //#endregion
            MessageBox.Show("请记得在左边登陆后查看今日打印信息！");
            //MessageBox.Show(Application.StartupPath + "\\文件数据\\" + DateTime.Now.AddDays(1).ToShortDateString() + "_morning");
            //MessageBox.Show(Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToShortDateString() + "_morning");
            //MessageBox.Show(DateTime.Now.ToLocalTime().ToString());

            SQLiteConnection connectionToDatabase = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\文件数据\\AllReference");
            connectionToDatabase.Open();

            SQLiteCommand cmd = connectionToDatabase.CreateCommand(); 
            cmd.CommandText = "select * from money";  
            SQLiteDataReader reader = cmd.ExecuteReader();  
            if (reader.HasRows)  
            {
                reader.Read();
                label6.Text = reader.GetDouble(0).ToString();
                label18.Text = reader.GetDouble(1).ToString();
                label8.Text = reader.GetDouble(2).ToString();
            }

            connectionToDatabase.Close();

            //if (File.Exists(Application.StartupPath + "\\文件数据\\"+DateTime.Now.ToShortDateString().Replace("/","-") + "_morning"))
            //{
            //    if (File.Exists(Application.StartupPath + "\\文件数据\\no_recive_morning"))
            //    {
            //        StreamWriter sw_add = new StreamWriter(Application.StartupPath + "\\文件数据\\no_recive_morning", true);
            //        StreamReader sw_read = new StreamReader(Application.StartupPath + "\\文件数据\\no_recive_morning");
            //        sw_add.Write(sw_read.ReadToEnd());
            //        sw_read.Close();
            //        sw_add.Close();
            //        File.Delete(Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToShortDateString().Replace("/", "-") + "_morning");
            //    }
            //    else
            //    {
            //        File.Move(Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToShortDateString().Replace("/", "-") + "_morning", Application.StartupPath + "\\文件数据\\no_recive_morning");
            //        File.Delete(Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToShortDateString().Replace("/", "-") + "_morning");
            //    }
            //}
            //if (File.Exists(Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToShortDateString().Replace("/", "-") + "_evening"))
            //{
            //    if (File.Exists(Application.StartupPath + "\\文件数据\\no_recive_evening"))
            //    {
            //        StreamWriter sw_add = new StreamWriter(Application.StartupPath + "\\文件数据\\no_recive_evening", true);
            //        StreamReader sw_read = new StreamReader(Application.StartupPath + "\\文件数据\\no_recive_evening");
            //        sw_add.Write(sw_read.ReadToEnd());
            //        sw_read.Close();
            //        sw_add.Close();
            //        File.Delete(Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToShortDateString().Replace("/", "-") + "_evening");
            //    }
            //    else
            //    {
            //        File.Move(Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToShortDateString().Replace("/", "-") + "_evening", Application.StartupPath + "\\文件数据\\no_recive_evening");
            //        File.Delete(Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToShortDateString().Replace("/", "-") + "_evening");
            //    }
            //}
        }

        private void refrash()
        {
            plDownList.Controls.Clear(); //刷新时先清空容器
            panel1.Controls.Clear();
            #region 原先从数据库获得信息
            //从服务器获取打印列表
            //DataSet dataSet = new DataSet();
            //int table, rows;
            //string[] paperType = null, copies = null, printMode = null, remark = null, documentUrl = null, documentName = null, address = null;
            //int[] ID=null;
            //dataSet = service.GetFileList(textBox1.Text);
            //table = dataSet.Tables.Count;
            //rows = dataSet.Tables["Columns"].Rows.Count;
            //for (int i = 0; i < rows; i++)
            //{
            //    paperType[i] = (string)dataSet.Tables["Columns"].Rows[i]["PaperType"];
            //    copies[i] = (string)dataSet.Tables["Columns"].Rows[i]["Copies"];
            //    printMode[i] = (string)dataSet.Tables["Columns"].Rows[i]["PrintType"];
            //    remark[i] = (string)dataSet.Tables["Columns"].Rows[i]["Remark"];
            //    documentUrl[i] = (string)dataSet.Tables["Columns"].Rows[i]["DocumentUrl"];
            //    documentName[i] = (string)dataSet.Tables["Columns"].Rows[i]["DocumentName"];
            //    address[i] = (string)dataSet.Tables["Columns"].Rows[i]["address"];
            //    ID[i] = (int)dataSet.Tables["Columns"].Rows[i]["ID"];
            //}
            ////int i = 0;
            ////foreach (DataRow dr1 in dataSet.Tables["tab2 "].Rows)
            ////{
            ////    paperType[i] = dr1["PaperType"].ToString();
            ////    copies[i] = dr1["Copies"].ToString();
            ////    printMode[i] = dr1["PrintMode"].ToString();


            ////}

            //MessageBox.Show(textBox1.Text);
            #endregion
            #region before list
            ////string jsonText = GetList(ShoperIds, textBox1.Text,Passwords,GetMD5Hash(textBox2.Text));

            ////JsonReader reader = new JsonTextReader(new StringReader(Text));
            ////while (reader.Read())
            ////{

            ////    MessageBox.Show(reader.TokenType + "\t\t" + reader.ValueType + "\t\t" + reader.Value);
            ////    if (reader.Value!=null||reader.Value.ToString() == "ture")
            ////        break;
            ////}
            ////if (reader.Value.ToString() == "ture")


            ////MessageBox.Show(uid);
            ////string jsonText = PostDataToUrl("uid" + "=" + uid, "http://www.xiaoyintong.com/v3_school_printer/list");
            //string jsonText = PostDataToUrl("", "http://api.xiaoyintong.dev:8000/api/v1/desktop/order/printing/list");
            ////StreamReader r = new StreamReader(@"L:\点维工作室\schoolprint-web\服务器数据.txt", Encoding.Default);
            ////string jsonText = r.ReadToEnd();
            ////r.Close();
            //if (jsonText.IndexOf("true") != -1)
            //{
            //    //string jsonText = @"[{""ID"":20547,""CustomerId"":""中南大学"",""ShoperId"":""central-south university"",""AddressId"":190}]";
            //    //MessageBox.Show(jsonText);
            //    //string jsonText = @"[{""ID"":20547,""CustomerId"":""中南大学"",""ShoperId"":""central-south university"",""AddressId"":190},{""ID"":20548,""CustomerId"":""湖南大学"",""ShoperId"":""Hunan university"",""AddressId"":190},{""ID"":20549,""CustomerId"":""湖南师范大学"",""ShoperId"":""hunan normal university"",""AddressId"":190}]";
            //    //MessageBox.Show(jsonText);
            //    //JsonReader reader = new JsonTextReader(new StringReader(jsonText));
            //    //while (true)
            //    //{
            //    //    reader.Read();
            //    //    MessageBox.Show(reader.TokenType + "\t\t" + reader.ValueType + "\t\t" + reader.Value);
            //    //}
            //    //JObject jo = JObject.Parse(jsonText);

            //    //string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();
            //    //MessageBox.Show(values[0].ToString());
            //    int k = 1;
            //    try
            //    {
            //        //StreamReader r = File.OpenText("D:\\new.txt");
            //        //jsonText=r.ReadToEnd();
            //        //r.Close();
            //        //jsonText = jsonText.Substring(jsonText.IndexOf("["));
            //        //int place = jsonText.LastIndexOf("]");
            //        //jsonText = jsonText.Substring(0, place + 1);
            //        if (jsonText != "[]")
            //        {
            //            //jsonText = jsonText.Replace(" ", "-");
            //            List<Document> result = new List<Document>();
            //            //result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Document>>(jsonText);
            //            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
            //            JArray jInfo = (JArray)jo["message"];

            //            //MessageBox.Show(result.Count.ToString());


            //            //WebReference.Document[] result2 = service.GetDocList(textBox1.Text);
            //            //int count = table.Rows.Count;
            //            //int count = result.Count;
            //            int count = jInfo.Count;
            //            item = new ListboxItem[count+2];
            //            count++;
            //            string[] CustomerId = new string[count];
            //            string[] DocumentUrl = new string[count];
            //            string[] ID = new string[count];
            //            string[] DocumentName = new string[count];
            //            string[] PaperType = new string[count];
            //            string[] sendWay = new string[count];
            //            string[] State = new string[count];
            //            string[] address = new string[count];
            //            string[] ShoperId = new string[count];
            //            string[] Copies = new string[count];
            //            string[] PrintMode = new string[count];
            //            string[] Remark = new string[count];
            //            string[] Phone = new string[count];
            //            string[] Names = new string[count];
            //            string[] upload_time = new string[count];
            //            int[] building = new int[count];
            //            string[] department = new string[count];
            //            int[] room_num = new int[count];
            //            string[] send_time = new string[count];
            //            string[] user_id = new string[count];
            //            string[] user_class = new string[count];
            //            string[] files = new string[count];
            //            //string[] DocumentNum = new string[count];
            //            //string[] SubmitTime = new string[count];
            //            //int k = 1;
            //            //foreach (Document row in result)
            //            foreach (JToken jt in jInfo)
            //            {
            //                if (k == 300)
            //                {
            //                    count = 301;
            //                    break;
            //                }
            //                //CustomerId[k] = row["CustomerId"].ToString();
            //                //user_id[k] = row.user_id;
            //                //user_class[k] = row.member_type;
            //                user_class[k] = jt["member_type"].ToString(); //row.member_type
            //                //CustomerId[k] = row.CustomerId;
            //                //DocumentUrl[k] = row.file_url;
            //                ID[k] = jt["order_tid"].ToString(); //row.tid;
            //                //DocumentName[k] = row.file_name;
            //                PaperType[k] = jt["printing"].ToString();//row.file_msg;
            //                //sendWay[k] = row.sendWay;
            //                //State[k] = row.send_status;
            //                address[k] = jt["user_address"].ToString();//row.loc;
            //                //ShoperId[k] = row.ShoperId;
            //                //Copies[k] = row.file_others;
            //                //PrintMode[k] = row.PrintMode;
            //                Remark[k] = jt["message"].ToString();//row.message;
            //                //Phone[k] = row.Phone;
            //                Names[k] = jt["user_information"].ToString();//row.user;
            //                upload_time[k] = jt["uploaded_time"].ToString();//row.upload_time;
            //                send_time[k] = jt["delivery_time"].ToString();//row.send_time;
            //                files[k] = jt["files"].ToString();
            //                //DateTime dat1 = DateTime.Parse(upload_time[k].Substring(0, upload_time[k].IndexOf(" ")));
            //                //DateTime dat2 = DateTime.Now.Date;
            //                //int time_compare_results = DateTime.Compare(dat1, dat2);
            //                //if (time_compare_results == -1)
            //                //{
            //                //    send_time[k] = send_time[k].Replace("明天", "今天");
            //                //}
            //                department[k] = address[k].Substring(0, 2);//row.loc.Substring(0, 2);
            //                building[k] = Convert.ToInt32(address[k].Substring(address[k].IndexOf(" ") + 1, address[k].IndexOf("栋") - address[k].IndexOf(" ") - 1));
            //                try
            //                {
            //                    room_num[k] = Convert.ToInt32(address[k].Substring(address[k].IndexOf("栋") + 2, address[k].IndexOf("室") - address[k].IndexOf('栋') - 2));
            //                }
            //                catch
            //                {
            //                    MessageBox.Show(address[k] + "|" + (address[k].IndexOf("室") - address[k].IndexOf('栋')));
            //                }
            //                //DocumentNum[i] = row["DocumentNum"].ToString();
            //                //SubmitTime[i] = row["SubmitTime"].ToString();
            //                k++;
            //            }

            //            int[,] zisong = new int[14, MAXSIZE];
            //            int[,] qinyuan = new int[14, MAXSIZE];
            //            int[,] yunyuan = new int[29, MAXSIZE];
            //            int[] z_room = new int[14];
            //            int[] q_room = new int[14];
            //            int[] y_room = new int[29];
            //            #region 普通用户
            //            for (int i = 1; i < count; i++)
            //            {
            //                if (user_class[i] == "7001")
            //                    continue;
            //                if (department[i] == "韵苑")
            //                {
            //                    if (y_room[building[i]] == 0)
            //                    {
            //                        yunyuan[building[i], y_room[building[i]]] = i;
            //                        y_room[building[i]]++;
            //                    }
            //                    else if ((y_room[building[i]] > 0) && (room_num[yunyuan[building[i], y_room[building[i]]-1]] <= room_num[i]))
            //                    {
            //                        yunyuan[building[i], y_room[building[i]]] = i;
            //                        y_room[building[i]]++;
            //                    }
            //                    else
            //                    {
            //                        int out_for = 0;
            //                        for (int x = 0; x < y_room[building[i]]; x++)
            //                        {
            //                            if (room_num[yunyuan[building[i], x]] > room_num[i])
            //                            {
            //                                for (int p = y_room[building[i]]; p > x; p--)
            //                                {
            //                                    yunyuan[building[i], p] = yunyuan[building[i], p - 1];
            //                                }
            //                                yunyuan[building[i], x] = i;
            //                                out_for = 1;
            //                            }
            //                            if (out_for == 1)
            //                                break;
            //                        }
            //                        y_room[building[i]]++;
            //                    }
            //                }
            //                else if (department[i] == "沁苑")
            //                {
            //                    if (q_room[building[i]] == 0)
            //                    {
            //                        qinyuan[building[i], q_room[building[i]]] = i;
            //                        q_room[building[i]]++;
            //                    }
            //                    else if ((q_room[building[i]] > 0) && (room_num[qinyuan[building[i], q_room[building[i]]-1]] <= room_num[i]))
            //                    {
            //                        qinyuan[building[i], q_room[building[i]]] = i;
            //                        q_room[building[i]]++;
            //                    }
            //                    else
            //                    {
            //                        int out_for = 0;
            //                        for (int x = 0; x < q_room[building[i]]; x++)
            //                        {
            //                            if (room_num[qinyuan[building[i], x]] > room_num[i])
            //                            {
            //                                for (int p = q_room[building[i]]; p > x; p--)
            //                                {
            //                                    qinyuan[building[i], p] = qinyuan[building[i], p - 1];
            //                                }
            //                                qinyuan[building[i], x] = i;
            //                                out_for = 1;
            //                            }
            //                            if (out_for == 1)
            //                                break;
            //                        }
            //                        q_room[building[i]]++;
            //                    }
            //                }
            //                else if (department[i] == "紫崧")
            //                {
            //                    if (z_room[building[i]] == 0)
            //                    {
            //                        zisong[building[i], z_room[building[i]]] = i;
            //                        z_room[building[i]]++;
            //                    }
            //                    else if ((z_room[building[i]] > 0) && (room_num[zisong[building[i], z_room[building[i]]-1]] <= room_num[i]))
            //                    {
            //                        zisong[building[i], z_room[building[i]]] = i;
            //                        z_room[building[i]]++;
            //                    }
            //                    else
            //                    {
            //                        int out_for = 0;
            //                        for (int x = 0; x < z_room[building[i]]; x++)
            //                        {
            //                            if (room_num[zisong[building[i], x]] > room_num[i])
            //                            {
            //                                for (int p = z_room[building[i]]; p > x; p--)
            //                                {
            //                                    zisong[building[i], p] = zisong[building[i], p - 1];
            //                                }
            //                                zisong[building[i], x] = i;
            //                                out_for = 1;
            //                            }
            //                            if (out_for == 1)
            //                                break;
            //                        }
            //                        z_room[building[i]]++;
            //                    }
            //                }
            //                else
            //                {
            //                    MessageBox.Show(department[i] + "-------地址有误!");
            //                }
            //            }

            //            int print_num = 0;
            //            int color_change = 0;
            //            string times_str = " ";
            //            string past = times_str;
            //            for (int time_num = 1; time_num <= 4; time_num++)
            //            {
            //                int is_any_print = 0;
            //                int time_unknow = 0;
            //                switch (time_num.ToString())
            //                {
            //                    case "4":
            //                        times_str = "今天, 12:15-13:00";
            //                        break;
            //                    case "3":
            //                        times_str = "今天, 22:00-23:00";
            //                        break;
            //                    case "2":
            //                        times_str = "明天, 12:15-13:00";
            //                        break;
            //                    case "1":
            //                        times_str = "明天, 22:00-23:00";
            //                        break;
            //                    case "5":
            //                        time_unknow = 1;
            //                        break;
            //                    default: break;
            //                }
            //                for (int p = 28; p > 0; p--)
            //                {
            //                    int order_count = 1;
            //                    for (int q = 0; q < y_room[p]; q++)
            //                    {
            //                        int i = yunyuan[p, q];
            //                        //DateTime dat1 = DateTime.Parse(upload_time[i].Substring(0, upload_time[i].IndexOf(" ")));
            //                        //DateTime dat2 = DateTime.Now.Date;
            //                        //int time_compare_results = DateTime.Compare(dat1, dat2);
            //                        //if (time_compare_results == -1)
            //                        //{
            //                        //    send_time[i] = send_time[i].Replace("明天", "今天");
            //                        //}
            //                        if ((times_str != send_time[i]) && time_unknow == 0)
            //                            continue;
            //                        if ((address[i] + Names[i]) != past)
            //                        {
            //                            color_change = (color_change + 1) % 2;
            //                            past = address[i] + Names[i];
            //                            print_message temp = new print_message(address[i], Names[i], this);
            //                            temp.Dock = System.Windows.Forms.DockStyle.Top;
            //                            isolation temp_isolation = new isolation();
            //                            temp_isolation.Dock = System.Windows.Forms.DockStyle.Top;
            //                            this.panel1.Controls.Add(temp_isolation);
            //                            this.panel1.Controls.Add(temp);
            //                            if (color_change == 1)
            //                                temp.color_set(Color.LightYellow);
            //                            else
            //                                temp.color_set(Color.LightBlue);
            //                            order_count = 1;
            //                        }
            //                        if (color_change == 1)
            //                        {
            //                            //JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
            //                            //JArray JAtemp = (JArray)files[i];
            //                            files[i] = "{'files': " + files[i] + "}";
            //                            JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
            //                            JArray JAtemp = (JArray)JOtemp["files"];
            //                            foreach (JToken temp_token in JAtemp)
            //                            {
            //                                item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightYellow, user_id[i], user_class[i]);
            //                                item[i].Name = i.ToString();
            //                                item[i].Dock = System.Windows.Forms.DockStyle.Top;
            //                                item[i].Location = new System.Drawing.Point(0, 80 * i);
            //                                //item[i].Name = "item" + i;
            //                                item[i].Name = address[i] + Names[i];
            //                                item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
            //                                item[i].MinimumSize = new Size(450, 80);
            //                                item[i].TabIndex = i;
            //                                this.panel1.Controls.Add(item[i]);
            //                                is_any_print = 1;
            //                                print_num++;
            //                            }
            //                            Order temp_order = new Order(this, Color.LightYellow, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
            //                            temp_order.Name = ID[i];
            //                            temp_order.Dock = System.Windows.Forms.DockStyle.Top;
            //                            this.panel1.Controls.Add(temp_order);
            //                        }
            //                        else
            //                        {
            //                            files[i] = "{'files': " + files[i] + "}";
            //                            JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
            //                            JArray JAtemp = (JArray)JOtemp["files"];
            //                            foreach (JToken temp_token in JAtemp)
            //                            {
            //                                item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightBlue, user_id[i], user_class[i]);
            //                                item[i].Name = i.ToString();
            //                                item[i].Dock = System.Windows.Forms.DockStyle.Top;
            //                                item[i].Location = new System.Drawing.Point(0, 80 * i);
            //                                //item[i].Name = "item" + i;
            //                                item[i].Name = address[i] + Names[i];
            //                                item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
            //                                item[i].MinimumSize = new Size(450, 80);
            //                                item[i].TabIndex = i;
            //                                this.panel1.Controls.Add(item[i]);
            //                                is_any_print = 1;
            //                                print_num++;
            //                            }
            //                            Order temp_order = new Order(this, Color.LightBlue, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
            //                            temp_order.Name = ID[i];
            //                            temp_order.Dock = System.Windows.Forms.DockStyle.Top;
            //                            this.panel1.Controls.Add(temp_order);
            //                        }
            //                        //item[i].Name = i.ToString();
            //                        //item[i].Dock = System.Windows.Forms.DockStyle.Top;
            //                        //item[i].Location = new System.Drawing.Point(0, 80 * i);
            //                        ////item[i].Name = "item" + i;
            //                        //item[i].Name = address[i];
            //                        //item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
            //                        //item[i].MinimumSize = new Size(450, 80);
            //                        //item[i].TabIndex = i;
            //                        //this.panel1.Controls.Add(item[i]);
            //                        //is_any_print = 1;
            //                        //print_num++;
            //                    }
            //                }

            //                for (int p = 13; p > 0; p--)
            //                {
            //                    int order_count = 1;
            //                    for (int q = 0; q < q_room[p]; q++)
            //                    {
            //                        int i = qinyuan[p, q];
            //                        //DateTime dat1 = DateTime.Parse(upload_time[i].Substring(0, upload_time[i].IndexOf(" ")));
            //                        //DateTime dat2 = DateTime.Now.Date;
            //                        //int time_compare_results = DateTime.Compare(dat1, dat2);
            //                        //if (time_compare_results == -1)
            //                        //{
            //                        //    send_time[i] = send_time[i].Replace("明天", "今天");
            //                        //}
            //                        //if ((times_str != send_time[i]) && time_unknow == 0)
            //                        //    continue;
            //                        //if (address[i] + Names[i] != past)
            //                        //{
            //                        //    color_change = (color_change + 1) % 2;
            //                        //    past = address[i] + Names[i];
            //                        //    print_message temp = new print_message(address[i] , Names[i],this);
            //                        //    temp.Dock = System.Windows.Forms.DockStyle.Top;
            //                        //    this.panel1.Controls.Add(temp);
            //                        //    if (color_change == 1)
            //                        //        temp.color_set(Color.LightYellow);
            //                        //    else
            //                        //        temp.color_set(Color.LightBlue);
            //                        //}
            //                        //if (color_change == 1)
            //                        //    item[i] = new ListboxItem(DocumentUrl[i], DocumentName[i], address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightYellow, user_id[i], user_class[i]);
            //                        //else
            //                        //    item[i] = new ListboxItem(DocumentUrl[i], DocumentName[i], address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightBlue, user_id[i], user_class[i]);
            //                        ////item[i] = new ListboxItem(DocumentUrl[i], DocumentName[i], address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], Color.LightYellow);
            //                        //item[i].Name = i.ToString();
            //                        //item[i].Dock = System.Windows.Forms.DockStyle.Top;
            //                        //item[i].Location = new System.Drawing.Point(0, 80 * i);
            //                        ////item[i].Name = "item" + i;
            //                        //item[i].Name = address[i];
            //                        //item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
            //                        //item[i].MinimumSize = new Size(450, 80);
            //                        //item[i].TabIndex = i;
            //                        //this.panel1.Controls.Add(item[i]);
            //                        //is_any_print = 1;
            //                        //print_num++;
            //                        if ((times_str != send_time[i]) && time_unknow == 0)
            //                            continue;
            //                        if ((address[i] + Names[i]) != past)
            //                        {
            //                            color_change = (color_change + 1) % 2;
            //                            past = address[i] + Names[i];
            //                            print_message temp = new print_message(address[i], Names[i], this);
            //                            temp.Dock = System.Windows.Forms.DockStyle.Top;
            //                            isolation temp_isolation = new isolation();
            //                            temp_isolation.Dock = System.Windows.Forms.DockStyle.Top;
            //                            this.panel1.Controls.Add(temp_isolation);
            //                            this.panel1.Controls.Add(temp);
            //                            if (color_change == 1)
            //                                temp.color_set(Color.LightYellow);
            //                            else
            //                                temp.color_set(Color.LightBlue);
            //                            order_count = 1;
            //                        }
            //                        if (color_change == 1)
            //                        {
            //                            //JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
            //                            //JArray JAtemp = (JArray)files[i];
            //                            files[i] = "{'files': " + files[i] + "}";
            //                            JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
            //                            JArray JAtemp = (JArray)JOtemp["files"];
            //                            foreach (JToken temp_token in JAtemp)
            //                            {
            //                                item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightYellow, user_id[i], user_class[i]);
            //                                item[i].Name = i.ToString();
            //                                item[i].Dock = System.Windows.Forms.DockStyle.Top;
            //                                item[i].Location = new System.Drawing.Point(0, 80 * i);
            //                                //item[i].Name = "item" + i;
            //                                item[i].Name = address[i] + Names[i];
            //                                item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
            //                                item[i].MinimumSize = new Size(450, 80);
            //                                item[i].TabIndex = i;
            //                                this.panel1.Controls.Add(item[i]);
            //                                is_any_print = 1;
            //                                print_num++;
            //                            }
            //                            Order temp_order = new Order(this, Color.LightYellow, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
            //                            temp_order.Name = ID[i];
            //                            temp_order.Dock = System.Windows.Forms.DockStyle.Top;
            //                            this.panel1.Controls.Add(temp_order);
            //                        }
            //                        else
            //                        {
            //                            files[i] = "{'files': " + files[i] + "}";
            //                            JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
            //                            JArray JAtemp = (JArray)JOtemp["files"];
            //                            foreach (JToken temp_token in JAtemp)
            //                            {
            //                                item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightBlue, user_id[i], user_class[i]);
            //                                item[i].Name = i.ToString();
            //                                item[i].Dock = System.Windows.Forms.DockStyle.Top;
            //                                item[i].Location = new System.Drawing.Point(0, 80 * i);
            //                                //item[i].Name = "item" + i;
            //                                item[i].Name = address[i] + Names[i];
            //                                item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
            //                                item[i].MinimumSize = new Size(450, 80);
            //                                item[i].TabIndex = i;
            //                                this.panel1.Controls.Add(item[i]);
            //                                is_any_print = 1;
            //                                print_num++;
            //                            }
            //                            Order temp_order = new Order(this, Color.LightBlue, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
            //                            temp_order.Name = ID[i];
            //                            temp_order.Dock = System.Windows.Forms.DockStyle.Top;
            //                            this.panel1.Controls.Add(temp_order);
            //                        }
            //                    }
            //                }

            //                for (int p = 13; p > 0; p--)
            //                {
            //                    int order_count = 1;
            //                    for (int q = 0; q < z_room[p]; q++)
            //                    {
            //                        int i = zisong[p, q];
            //                        //DateTime dat1 = DateTime.Parse(upload_time[i].Substring(0, upload_time[i].IndexOf(" ")));
            //                        //DateTime dat2 = DateTime.Now.Date;
            //                        //int time_compare_results = DateTime.Compare(dat1, dat2);
            //                        //if (time_compare_results == -1)
            //                        //{
            //                        //    send_time[i] = send_time[i].Replace("明天", "今天");
            //                        //}
            //                        //if ((times_str != send_time[i]) && time_unknow == 0)
            //                        //    continue;
            //                        //if (address[i] + Names[i] != past)
            //                        //{
            //                        //    color_change = (color_change + 1) % 2;
            //                        //    past = address[i] + Names[i];
            //                        //    print_message temp = new print_message(address[i] , Names[i],this);
            //                        //    temp.Dock = System.Windows.Forms.DockStyle.Top;
            //                        //    this.panel1.Controls.Add(temp);
            //                        //    if (color_change == 1)
            //                        //        temp.color_set(Color.LightYellow);
            //                        //    else
            //                        //        temp.color_set(Color.LightBlue);
            //                        //}
            //                        //if (color_change == 1)
            //                        //    item[i] = new ListboxItem(DocumentUrl[i], DocumentName[i], address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightYellow, user_id[i], user_class[i]);
            //                        //else
            //                        //    item[i] = new ListboxItem(DocumentUrl[i], DocumentName[i], address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightBlue, user_id[i], user_class[i]);
            //                        ////item[i] = new ListboxItem(DocumentUrl[i], DocumentName[i], address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], Color.LightYellow);
            //                        //item[i].Name = i.ToString();
            //                        //item[i].Dock = System.Windows.Forms.DockStyle.Top;
            //                        //item[i].Location = new System.Drawing.Point(0, 80 * i);
            //                        ////item[i].Name = "item" + i;
            //                        //item[i].Name = address[i];
            //                        //item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
            //                        //item[i].MinimumSize = new Size(450, 80);
            //                        //item[i].TabIndex = i;
            //                        //this.panel1.Controls.Add(item[i]);
            //                        //is_any_print = 1;
            //                        //print_num++;
            //                        if ((times_str != send_time[i]) && time_unknow == 0)
            //                            continue;
            //                        if ((address[i] + Names[i]) != past)
            //                        {
            //                            color_change = (color_change + 1) % 2;
            //                            past = address[i] + Names[i];
            //                            print_message temp = new print_message(address[i], Names[i], this);
            //                            temp.Dock = System.Windows.Forms.DockStyle.Top;
            //                            isolation temp_isolation = new isolation();
            //                            temp_isolation.Dock = System.Windows.Forms.DockStyle.Top;
            //                            this.panel1.Controls.Add(temp_isolation);
            //                            this.panel1.Controls.Add(temp);
            //                            if (color_change == 1)
            //                                temp.color_set(Color.LightYellow);
            //                            else
            //                                temp.color_set(Color.LightBlue);
            //                            order_count = 1;
            //                        }
            //                        if (color_change == 1)
            //                        {
            //                            //JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
            //                            //JArray JAtemp = (JArray)files[i];
            //                            files[i] = "{'files': " + files[i] + "}";
            //                            JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
            //                            JArray JAtemp = (JArray)JOtemp["files"];
            //                            foreach (JToken temp_token in JAtemp)
            //                            {
            //                                item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightYellow, user_id[i], user_class[i]);
            //                                item[i].Name = i.ToString();
            //                                item[i].Dock = System.Windows.Forms.DockStyle.Top;
            //                                item[i].Location = new System.Drawing.Point(0, 80 * i);
            //                                //item[i].Name = "item" + i;
            //                                item[i].Name = address[i] + Names[i];
            //                                item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
            //                                item[i].MinimumSize = new Size(450, 80);
            //                                item[i].TabIndex = i;
            //                                this.panel1.Controls.Add(item[i]);
            //                                is_any_print = 1;
            //                                print_num++;
            //                            }
            //                            Order temp_order = new Order(this, Color.LightYellow, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
            //                            temp_order.Name = ID[i];
            //                            temp_order.Dock = System.Windows.Forms.DockStyle.Top;
            //                            this.panel1.Controls.Add(temp_order);
            //                        }
            //                        else
            //                        {
            //                            files[i] = "{'files': " + files[i] + "}";
            //                            JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
            //                            JArray JAtemp = (JArray)JOtemp["files"];
            //                            foreach (JToken temp_token in JAtemp)
            //                            {
            //                                item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightBlue, user_id[i], user_class[i]);
            //                                item[i].Name = i.ToString();
            //                                item[i].Dock = System.Windows.Forms.DockStyle.Top;
            //                                item[i].Location = new System.Drawing.Point(0, 80 * i);
            //                                //item[i].Name = "item" + i;
            //                                item[i].Name = address[i] + Names[i];
            //                                item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
            //                                item[i].MinimumSize = new Size(450, 80);
            //                                item[i].TabIndex = i;
            //                                this.panel1.Controls.Add(item[i]);
            //                                is_any_print = 1;
            //                                print_num++;
            //                            }
            //                            Order temp_order = new Order(this, Color.LightBlue, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
            //                            temp_order.Name = ID[i];
            //                            temp_order.Dock = System.Windows.Forms.DockStyle.Top;
            //                            this.panel1.Controls.Add(temp_order);
            //                        }
            //                    }
            //                }
            //                if (is_any_print != 1)
            //                    continue;
            //                string time_str = "以下是" + times_str + "送货";
            //                if (time_unknow == 1)
            //                    time_str = "以下时间未能正确解析！";
            //                time_form times = new time_form(time_str);
            //                times.Dock = System.Windows.Forms.DockStyle.Top;
            //                this.panel1.Controls.Add(times);
            //            }
                            
                        
            //            #endregion

            //            #region VIP用户
            //            zisong = new int[14, MAXSIZE];
            //            qinyuan = new int[14, MAXSIZE];
            //            yunyuan = new int[29, MAXSIZE];
            //            z_room = new int[14];
            //            q_room = new int[14];
            //            y_room = new int[29];
            //            for (int i = 1; i < count; i++)
            //            {
            //                if (user_class[i] != "7001")
            //                    continue;
            //                if (department[i] == "韵苑")
            //                {
            //                    if (y_room[building[i]] == 0)
            //                    {
            //                        yunyuan[building[i], y_room[building[i]]] = i;
            //                        y_room[building[i]]++;
            //                    }
            //                    else if ((y_room[building[i]] > 0) && (room_num[yunyuan[building[i], y_room[building[i]]-1]] <= room_num[i]))
            //                    {
            //                        yunyuan[building[i], y_room[building[i]]] = i;
            //                        y_room[building[i]]++;
            //                    }
            //                    else
            //                    {
            //                        int out_for = 0;
            //                        for (int x = 0; x < y_room[building[i]]; x++)
            //                        {
            //                            if (room_num[yunyuan[building[i], x]] > room_num[i])
            //                            {
            //                                for (int p = y_room[building[i]]; p > x; p--)
            //                                {
            //                                    yunyuan[building[i], p] = yunyuan[building[i], p - 1];
            //                                }
            //                                yunyuan[building[i], x] = i;
            //                                out_for = 1;
            //                            }
            //                            if (out_for == 1)
            //                                break;
            //                        }
            //                        y_room[building[i]]++;
            //                    }
            //                }
            //                else if (department[i] == "沁苑")
            //                {
            //                    if (q_room[building[i]] == 0)
            //                    {
            //                        qinyuan[building[i], q_room[building[i]]] = i;
            //                        q_room[building[i]]++;
            //                    }
            //                    else if ((q_room[building[i]] > 0) && (room_num[qinyuan[building[i], q_room[building[i]]-1]] <= room_num[i]))
            //                    {
            //                        qinyuan[building[i], q_room[building[i]]] = i;
            //                        q_room[building[i]]++;
            //                    }
            //                    else
            //                    {
            //                        int out_for = 0;
            //                        for (int x = 0; x < q_room[building[i]]; x++)
            //                        {
            //                            if (room_num[qinyuan[building[i], x]] > room_num[i])
            //                            {
            //                                for (int p = q_room[building[i]]; p > x; p--)
            //                                {
            //                                    qinyuan[building[i], p] = qinyuan[building[i], p - 1];
            //                                }
            //                                qinyuan[building[i], x] = i;
            //                                out_for = 1;
            //                            }
            //                            if (out_for == 1)
            //                                break;
            //                        }
            //                        q_room[building[i]]++;
            //                    }
            //                }
            //                else if (department[i] == "紫崧")
            //                {
            //                    if (z_room[building[i]] == 0)
            //                    {
            //                        zisong[building[i], z_room[building[i]]] = i;
            //                        z_room[building[i]]++;
            //                    }
            //                    else if ((z_room[building[i]] > 0) && (room_num[zisong[building[i], z_room[building[i]]-1]] <= room_num[i]))
            //                    {
            //                        zisong[building[i], z_room[building[i]]] = i;
            //                        z_room[building[i]]++;
            //                    }
            //                    else
            //                    {
            //                        int out_for = 0;
            //                        for (int x = 0; x < z_room[building[i]]; x++)
            //                        {
            //                            if (room_num[zisong[building[i], x]] > room_num[i])
            //                            {
            //                                for (int p = z_room[building[i]]; p > x; p--)
            //                                {
            //                                    zisong[building[i], p] = zisong[building[i], p - 1];
            //                                }
            //                                zisong[building[i], x] = i;
            //                                out_for = 1;
            //                            }
            //                            if (out_for == 1)
            //                                break;
            //                        }
            //                        z_room[building[i]]++;
            //                    }
            //                }
            //                else
            //                {
            //                    MessageBox.Show(department[i] + "-------地址有误!");
            //                }
            //            }

            //            color_change = 0;
            //            past = " ";
            //            times_str = " ";
            //            for (int time_num = 1; time_num <= 4; time_num++)
            //            {
            //                int is_any_print = 0;
            //                switch (time_num.ToString())
            //                {
            //                    case "4":
            //                        times_str = "今天 12:30 ~ 13:00";
            //                        break;
            //                    case "3":
            //                        times_str = "今天 22:00 ~ 23:00";
            //                        break;
            //                    case "2":
            //                        times_str = "明天 12:30 ~ 13:00";
            //                        break;
            //                    case "1":
            //                        times_str = "明天 22:00 ~ 23:00";
            //                        break;
            //                    default: break;
            //                }
            //                for (int p = 28; p > 0; p--)
            //                {
            //                    for (int q = 0; q < y_room[p]; q++)
            //                    {
            //                        int i = yunyuan[p, q];
            //                        //DateTime dat1 = DateTime.Parse(upload_time[i].Substring(0, upload_time[i].IndexOf(" ")));
            //                        //DateTime dat2 = DateTime.Now.Date;
            //                        //int time_compare_results = DateTime.Compare(dat1, dat2);
            //                        //if (time_compare_results == -1)
            //                        //{
            //                        //    send_time[i] = send_time[i].Replace("明天", "今天");
            //                        //}
            //                        if ((times_str != send_time[i]))
            //                            continue;
            //                        if (address[i] != past)
            //                        {
            //                            color_change = (color_change + 1) % 2;
            //                            past = address[i];
            //                        }
            //                        if (color_change == 1)
            //                            item[i] = new ListboxItem(DocumentUrl[i], DocumentName[i], address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightYellow, user_id[i], user_class[i]);
            //                        else
            //                            item[i] = new ListboxItem(DocumentUrl[i], DocumentName[i], address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightBlue, user_id[i], user_class[i]);
            //                        item[i].Name = i.ToString();
            //                        item[i].Dock = System.Windows.Forms.DockStyle.Top;
            //                        item[i].Location = new System.Drawing.Point(0, 80 * i);
            //                        item[i].Name = "item" + i;
            //                        item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
            //                        item[i].MinimumSize = new Size(450, 80);
            //                        item[i].TabIndex = i;
            //                        this.plDownList.Controls.Add(item[i]);
            //                        is_any_print = 1;
            //                        print_num++;
            //                    }
            //                }

            //                for (int p = 13; p > 0; p--)
            //                {
            //                    for (int q = 0; q < q_room[p]; q++)
            //                    {
            //                        int i = qinyuan[p, q];
            //                        if ((times_str != send_time[i]))
            //                            continue;
            //                        //DateTime dat1 = DateTime.Parse(upload_time[i].Substring(0, upload_time[i].IndexOf(" ")));
            //                        //DateTime dat2 = DateTime.Now.Date;
            //                        //int time_compare_results = DateTime.Compare(dat1, dat2);
            //                        //if (time_compare_results == -1)
            //                        //{
            //                        //    send_time[i] = send_time[i].Replace("明天", "今天");
            //                        //}
            //                        if (address[i] != past)
            //                        {
            //                            color_change = (color_change + 1) % 2;
            //                            past = address[i];
            //                        }
            //                        if (color_change == 1)
            //                            item[i] = new ListboxItem(DocumentUrl[i], DocumentName[i], address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightYellow, user_id[i], user_class[i]);
            //                        else
            //                            item[i] = new ListboxItem(DocumentUrl[i], DocumentName[i], address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightBlue, user_id[i], user_class[i]);
            //                        //item[i] = new ListboxItem(DocumentUrl[i], DocumentName[i], address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], Color.LightYellow);
            //                        item[i].Name = i.ToString();
            //                        item[i].Dock = System.Windows.Forms.DockStyle.Top;
            //                        item[i].Location = new System.Drawing.Point(0, 80 * i);
            //                        item[i].Name = "item" + i;
            //                        item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
            //                        item[i].MinimumSize = new Size(450, 80);
            //                        item[i].TabIndex = i;
            //                        this.plDownList.Controls.Add(item[i]);
            //                        is_any_print = 1;
            //                        print_num++;
            //                    }
            //                }

            //                for (int p = 13; p > 0; p--)
            //                {
            //                    for (int q = 0; q < z_room[p]; q++)
            //                    {
            //                        int i = zisong[p, q];
            //                        //DateTime dat1 = DateTime.Parse(upload_time[i].Substring(0, upload_time[i].IndexOf(" ")));
            //                        //DateTime dat2 = DateTime.Now.Date;
            //                        //int time_compare_results = DateTime.Compare(dat1, dat2);
            //                        //if (time_compare_results == -1)
            //                        //{
            //                        //    send_time[i] = send_time[i].Replace("明天", "今天");
            //                        //}
            //                        if ((times_str != send_time[i]))
            //                            continue;
            //                        if (address[i] != past)
            //                        {
            //                            color_change = (color_change + 1) % 2;
            //                            past = address[i];
            //                        }
            //                        if (color_change == 1)
            //                            item[i] = new ListboxItem(DocumentUrl[i], DocumentName[i], address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightYellow, user_id[i], user_class[i]);
            //                        else
            //                            item[i] = new ListboxItem(DocumentUrl[i], DocumentName[i], address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightBlue, user_id[i], user_class[i]);
            //                        //item[i] = new ListboxItem(DocumentUrl[i], DocumentName[i], address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], Color.LightYellow);
            //                        item[i].Name = i.ToString();
            //                        item[i].Dock = System.Windows.Forms.DockStyle.Top;
            //                        item[i].Location = new System.Drawing.Point(0, 80 * i);
            //                        item[i].Name = "item" + i;
            //                        item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
            //                        item[i].MinimumSize = new Size(450, 80);
            //                        item[i].TabIndex = i;
            //                        this.plDownList.Controls.Add(item[i]);
            //                        is_any_print = 1;
            //                        print_num++;
            //                    }
            //                }
            //                if (is_any_print != 1)
            //                    continue;
            //                time_form times = new time_form("以下是" + times_str + "送货");
            //                times.Dock = System.Windows.Forms.DockStyle.Top;
            //                this.plDownList.Controls.Add(times);
            //            }
                            
            //            #endregion


            //            MessageBox.Show("共" + (count - 1).ToString() + "个订单任务");
            //            //MessageBox.Show("" + print_num);

            //            CustomerId = null;
            //            DocumentUrl = null;
            //            ID = null;
            //            DocumentName = null;
            //            PaperType = null;
            //            sendWay = null;
            //            State = null;
            //            address = null;
            //            ShoperId = null;
            //            Copies = null;
            //            PrintMode = null;
            //            Remark = null;
            //            Phone = null;
            //            Names = null;
            //            upload_time = null;
            //            building = null;
            //            department = null;
            //            room_num = null;
            //            send_time = null;
            //            user_id = null;
            //            user_class = null;
            //        }
            //        else
            //        {
            //            MessageBox.Show("没有打印任务！");
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        //MessageBox.Show(k.ToString());
            //        MessageBox.Show("出错了！\n" + e.ToString());

            //    }
            //}
            //GC.Collect();

            #endregion
            //int nomal = refreshNormal();
            //int VIP = refreshVIP();
            int nomal = refreshNormalWithDatabase();
            int VIP = refreshVIPWithDatabase();
            //int nomal = 0;
            //int VIP = 0;
            MessageBox.Show("一共有" + (nomal + VIP) + "订单任务，其中VIP有" + VIP + "个");
        }

        private int refreshNormalWithDatabase()
        {
            int number = 0;
            SQLiteCommand command;
            SQLiteConnection connectionToDatabase;

            if (File.Exists(Application.StartupPath + "\\tempPrint.db3"))
                File.Delete(Application.StartupPath + "\\tempPrint.db3");
            SQLiteConnection.CreateFile(Application.StartupPath + "\\tempPrint.db3");
            connectionToDatabase = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\tempPrint.db3");
            connectionToDatabase.Open();
            command = new SQLiteCommand();
            command.CommandText = "create table tempPrintFile(ID char(15), paperType char(50), address char(30), remark char(200), names char(50), uploadTime date, sendTime date, timeQuantum char(20), files vchar(2000))";
            command.Connection = connectionToDatabase;
            command.ExecuteNonQuery();
            connectionToDatabase.Close();

            //string jsonText = PostDataToUrl("", "http://api.xiaoyintong.dev:8000/api/v1/desktop/order/printing/normal/list");
            string jsonText = PostDataToUrl("", "http://api.xiaoyintong.com/api/v1/desktop/order/printing/normal/list");
            //StreamReader r = new StreamReader(@"F:\点维工作室\schoolprint-web-version2\schoolprint-web-v2.0\服务器数据.txt", Encoding.Default);
            //string jsonText = r.ReadToEnd();
            //r.Close();
            if (jsonText.IndexOf("true") != -1)
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
                JArray jInfo = (JArray)jo["message"];
                int count = jInfo.Count;
                item = new ListboxItem[count + 2];
                connectionToDatabase = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\tempPrint.db3");
                connectionToDatabase.Open();
                foreach (JToken jt in jInfo)
                {

                    int place = jt["delivery_time"].ToString().IndexOf(", ");
                    string uploadDate = jt["delivery_time"].ToString().Substring(0, place);
                    string uploadTimeQuantum = jt["delivery_time"].ToString().Substring(place + 2);
                    command = new SQLiteCommand();
                    command.CommandText = "insert into tempPrintFile values('" + jt["order_tid"].ToString() + "','" + jt["printing"].ToString() + "','" + jt["user_address"].ToString() + "','" + jt["message"].ToString() + "','" + jt["user_information"].ToString() + "','" + jt["uploaded_time"].ToString() + "','" + uploadDate + "','" + uploadTimeQuantum + "','" + jt["files"].ToString() + "')";
                    command.Connection = connectionToDatabase;
                    command.ExecuteNonQuery();
                }
                connectionToDatabase.Close();

                connectionToDatabase = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\tempPrint.db3");
                connectionToDatabase.Open();
                command.CommandText = "select distinct sendTime,timeQuantum from tempPrintFile group by sendTime,timeQuantum";
                command.Connection = connectionToDatabase;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                DataTable userTable = new DataTable();
                adapter.Fill(userTable);
                connectionToDatabase.Close();
                for (int k = userTable.Rows.Count - 1; k >= 0; k--)
                {
                    connectionToDatabase = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\tempPrint.db3");
                    connectionToDatabase.Open();
                    command.CommandText = "select ID,paperType,address,remark,names,files from tempPrintFile where sendTime = '" + Convert.ToDateTime(userTable.Rows[k]["sendTime"]).ToString("yyyy-MM-dd") + "' and timeQuantum = '" + userTable.Rows[k]["timeQuantum"] + "' group by names,address,ID";
                    command.Connection = connectionToDatabase;
                    SQLiteDataAdapter adapters = new SQLiteDataAdapter(command);
                    DataTable userTables = new DataTable();
                    adapters.Fill(userTables);
                    connectionToDatabase.Close();

                    number += userTables.Rows.Count;

                    string past = "";
                    int order_count = 1;
                    int color_change = 0;
                    for (int i = 0; i < userTables.Rows.Count; i++)
                    {
                        if (userTables.Rows[i]["address"].ToString() + userTables.Rows[i]["names"].ToString() != past)
                        {
                            if (past != "")
                                SetOrderNumber(order_count - 1, "7000", past);
                            color_change = (color_change + 1) % 2;
                            past = userTables.Rows[i]["address"].ToString() + userTables.Rows[i]["names"].ToString();
                            print_message temp = new print_message(userTables.Rows[i]["address"].ToString(), userTables.Rows[i]["names"].ToString(), this, "7000");
                            temp.Dock = System.Windows.Forms.DockStyle.Top;
                            temp.Name = userTables.Rows[i]["address"].ToString() + userTables.Rows[i]["names"].ToString();
                            isolation temp_isolation = new isolation();
                            temp_isolation.Dock = System.Windows.Forms.DockStyle.Top;
                            this.panel1.Controls.Add(temp_isolation);
                            this.panel1.Controls.Add(temp);
                            if (color_change == 1)
                                temp.color_set(Color.LightYellow);
                            else
                                temp.color_set(Color.LightBlue);
                            order_count = 1;
                        }

                        if (color_change == 1)
                        {
                            userTables.Rows[i]["files"] = "{'files': " + userTables.Rows[i]["files"].ToString() + "}";
                            JObject JOtemp = (JObject)JsonConvert.DeserializeObject(userTables.Rows[i]["files"].ToString());
                            JArray JAtemp = (JArray)JOtemp["files"];
                            foreach (JToken temp_token in JAtemp)
                            {
                                item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), userTables.Rows[i]["address"].ToString(), this, userTables.Rows[i]["paperType"].ToString(), "", "", userTables.Rows[i]["remark"].ToString(), userTables.Rows[i]["ID"].ToString(), userTables.Rows[i]["names"].ToString(), "", "", "", Color.LightYellow, "", "7000");
                                //item[i].Name = i.ToString();
                                item[i].Name = userTables.Rows[i]["ID"].ToString() + userTables.Rows[i]["address"].ToString() + userTables.Rows[i]["names"].ToString();
                                item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                item[i].Location = new System.Drawing.Point(0, 80 * i);
                                //item[i].Name = "item" + i;
                                //item[i].Name = address[i] + Names[i];
                                item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                item[i].MinimumSize = new Size(450, 80);
                                item[i].TabIndex = i;
                                this.panel1.Controls.Add(item[i]);
                                //is_any_print = 1;
                                //print_num++;
                            }
                            Order temp_order = new Order(this, Color.LightYellow, userTables.Rows[i]["ID"].ToString(), order_count++, userTables.Rows[i]["paperType"].ToString(), userTables.Rows[i]["remark"].ToString(), userTables.Rows[i]["address"].ToString(), userTables.Rows[i]["names"].ToString(), userTables.Rows[i]["files"].ToString(), "7000");
                            temp_order.Name = userTables.Rows[i]["ID"].ToString();
                            temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                            this.panel1.Controls.Add(temp_order);
                        }
                        else
                        {
                            userTables.Rows[i]["files"] = "{'files': " + userTables.Rows[i]["files"].ToString() + "}";
                            JObject JOtemp = (JObject)JsonConvert.DeserializeObject(userTables.Rows[i]["files"].ToString());
                            JArray JAtemp = (JArray)JOtemp["files"];
                            foreach (JToken temp_token in JAtemp)
                            {
                                item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), userTables.Rows[i]["address"].ToString(), this, userTables.Rows[i]["paperType"].ToString(), "", "", userTables.Rows[i]["remark"].ToString(), userTables.Rows[i]["ID"].ToString(), userTables.Rows[i]["names"].ToString(), "", "", "", Color.LightBlue, "", "7000");
                                //item[i].Name = i.ToString();
                                item[i].Name = userTables.Rows[i]["ID"].ToString() + userTables.Rows[i]["address"].ToString() + userTables.Rows[i]["names"].ToString();
                                item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                item[i].Location = new System.Drawing.Point(0, 80 * i);
                                //item[i].Name = "item" + i;
                                //item[i].Name = address[i] + Names[i];
                                item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                item[i].MinimumSize = new Size(450, 80);
                                item[i].TabIndex = i;
                                this.panel1.Controls.Add(item[i]);
                                //is_any_print = 1;
                                //print_num++;
                            }
                            Order temp_order = new Order(this, Color.LightBlue, userTables.Rows[i]["ID"].ToString(), order_count++, userTables.Rows[i]["paperType"].ToString(), userTables.Rows[i]["remark"].ToString(), userTables.Rows[i]["address"].ToString(), userTables.Rows[i]["names"].ToString(), userTables.Rows[i]["files"].ToString(), "7000");
                            temp_order.Name = userTables.Rows[i]["ID"].ToString();
                            temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                            this.panel1.Controls.Add(temp_order);
                        }
                    }
                    if (past != "")
                        SetOrderNumber(order_count - 1, "7000", past);
                    string time_str = "以下是" + userTable.Rows[k]["sendTime"].ToString().Substring(0, userTable.Rows[k]["sendTime"].ToString().IndexOf(' ')) + " " + userTable.Rows[k]["timeQuantum"].ToString() + "送货";
                    time_form times = new time_form(time_str);
                    times.Dock = System.Windows.Forms.DockStyle.Top;
                    this.panel1.Controls.Add(times);
                }
            }
            return number;
        }

        private int refreshVIPWithDatabase()
        {
            int number = 0;
            SQLiteCommand command;
            SQLiteConnection connectionToDatabase;

            if (File.Exists(Application.StartupPath + "\\tempPrint.db3"))
                File.Delete(Application.StartupPath + "\\tempPrint.db3");
            SQLiteConnection.CreateFile(Application.StartupPath + "\\tempPrint.db3");
            connectionToDatabase = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\tempPrint.db3");
            connectionToDatabase.Open();
            command = new SQLiteCommand();
            command.CommandText = "create table tempPrintFile(ID char(15), paperType char(50), address char(30), remark char(200), names char(50), uploadTime date, sendTime date, timeQuantum char(20), files vchar(2000))";
            command.Connection = connectionToDatabase;
            command.ExecuteNonQuery();
            connectionToDatabase.Close();

            //string jsonText = PostDataToUrl("", "http://api.xiaoyintong.dev:8000/api/v1/desktop/order/printing/vip/list");
            string jsonText = PostDataToUrl("", "http://api.xiaoyintong.com/api/v1/desktop/order/printing/vip/list");
            //StreamReader r = new StreamReader(@"F:\点维工作室\schoolprint-web-version2\schoolprint-web-v2.0\服务器数据.txt", Encoding.Default);
            //string jsonText = r.ReadToEnd();
            //r.Close();
            if (jsonText.IndexOf("true") != -1)
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
                JArray jInfo = (JArray)jo["message"];
                int count = jInfo.Count;
                item = new ListboxItem[count + 2];
                connectionToDatabase = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\tempPrint.db3");
                connectionToDatabase.Open();
                foreach (JToken jt in jInfo)
                {

                    int place = jt["delivery_time"].ToString().IndexOf(", ");
                    string uploadDate = jt["delivery_time"].ToString().Substring(0, place);
                    string uploadTimeQuantum = jt["delivery_time"].ToString().Substring(place + 2);
                    command = new SQLiteCommand();
                    command.CommandText = "insert into tempPrintFile values('" + jt["order_tid"].ToString() + "','" + jt["printing"].ToString() + "','" + jt["user_address"].ToString() + "','" + jt["message"].ToString() + "','" + jt["user_information"].ToString() + "','" + jt["uploaded_time"].ToString() + "','" + uploadDate + "','" + uploadTimeQuantum + "','" + jt["files"].ToString() + "')";
                    command.Connection = connectionToDatabase;
                    command.ExecuteNonQuery();
                }
                connectionToDatabase.Close();

                connectionToDatabase = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\tempPrint.db3");
                connectionToDatabase.Open();
                command.CommandText = "select distinct sendTime,timeQuantum from tempPrintFile group by sendTime,timeQuantum";
                command.Connection = connectionToDatabase;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                DataTable userTable = new DataTable();
                adapter.Fill(userTable);
                connectionToDatabase.Close();
                for (int k = userTable.Rows.Count - 1; k >= 0; k--)
                {
                    connectionToDatabase = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\tempPrint.db3");
                    connectionToDatabase.Open();
                    command.CommandText = "select ID,paperType,address,remark,names,files from tempPrintFile where sendTime = '" + Convert.ToDateTime(userTable.Rows[k]["sendTime"]).ToString("yyyy-MM-dd") + "' and timeQuantum = '" + userTable.Rows[k]["timeQuantum"] + "' group by names,address,ID";
                    command.Connection = connectionToDatabase;
                    SQLiteDataAdapter adapters = new SQLiteDataAdapter(command);
                    DataTable userTables = new DataTable();
                    adapters.Fill(userTables);
                    connectionToDatabase.Close();

                    number += userTables.Rows.Count;

                    string past = "";
                    int order_count = 1;
                    int color_change = 0;
                    for (int i = 0; i < userTables.Rows.Count; i++)
                    {
                        if (userTables.Rows[i]["address"].ToString() + userTables.Rows[i]["names"].ToString() != past)
                        {
                            if (past != "")
                                SetOrderNumber(order_count - 1, "7001", past);
                            color_change = (color_change + 1) % 2;
                            past = userTables.Rows[i]["address"].ToString() + userTables.Rows[i]["names"].ToString();
                            print_message temp = new print_message(userTables.Rows[i]["address"].ToString(), userTables.Rows[i]["names"].ToString(), this, "7001");
                            temp.Dock = System.Windows.Forms.DockStyle.Top;
                            temp.Name = userTables.Rows[i]["address"].ToString() + userTables.Rows[i]["names"].ToString();
                            isolation temp_isolation = new isolation();
                            temp_isolation.Dock = System.Windows.Forms.DockStyle.Top;
                            this.plDownList.Controls.Add(temp_isolation);
                            this.plDownList.Controls.Add(temp);
                            if (color_change == 1)
                                temp.color_set(Color.LightYellow);
                            else
                                temp.color_set(Color.LightBlue);
                            order_count = 1;
                        }

                        if (color_change == 1)
                        {
                            userTables.Rows[i]["files"] = "{'files': " + userTables.Rows[i]["files"].ToString() + "}";
                            JObject JOtemp = (JObject)JsonConvert.DeserializeObject(userTables.Rows[i]["files"].ToString());
                            JArray JAtemp = (JArray)JOtemp["files"];
                            foreach (JToken temp_token in JAtemp)
                            {
                                item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), userTables.Rows[i]["address"].ToString(), this, userTables.Rows[i]["paperType"].ToString(), "", "", userTables.Rows[i]["remark"].ToString(), userTables.Rows[i]["ID"].ToString(), userTables.Rows[i]["names"].ToString(), "", "", "", Color.LightYellow, "", "7001");
                                //item[i].Name = i.ToString();
                                item[i].Name = userTables.Rows[i]["ID"].ToString() + userTables.Rows[i]["address"].ToString() + userTables.Rows[i]["names"].ToString();
                                item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                item[i].Location = new System.Drawing.Point(0, 80 * i);
                                //item[i].Name = "item" + i;
                                //item[i].Name = address[i] + Names[i];
                                item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                item[i].MinimumSize = new Size(450, 80);
                                item[i].TabIndex = i;
                                this.plDownList.Controls.Add(item[i]);
                                //is_any_print = 1;
                                //print_num++;
                            }
                            Order temp_order = new Order(this, Color.LightYellow, userTables.Rows[i]["ID"].ToString(), order_count++, userTables.Rows[i]["paperType"].ToString(), userTables.Rows[i]["remark"].ToString(), userTables.Rows[i]["address"].ToString(), userTables.Rows[i]["names"].ToString(), userTables.Rows[i]["files"].ToString(), "7001");
                            temp_order.Name = userTables.Rows[i]["ID"].ToString();
                            temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                            this.plDownList.Controls.Add(temp_order);
                        }
                        else
                        {
                            userTables.Rows[i]["files"] = "{'files': " + userTables.Rows[i]["files"].ToString() + "}";
                            JObject JOtemp = (JObject)JsonConvert.DeserializeObject(userTables.Rows[i]["files"].ToString());
                            JArray JAtemp = (JArray)JOtemp["files"];
                            foreach (JToken temp_token in JAtemp)
                            {
                                item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), userTables.Rows[i]["address"].ToString(), this, userTables.Rows[i]["paperType"].ToString(), "", "", userTables.Rows[i]["remark"].ToString(), userTables.Rows[i]["ID"].ToString(), userTables.Rows[i]["names"].ToString(), "", "", "", Color.LightBlue, "", "7001");
                                //item[i].Name = i.ToString();
                                item[i].Name = userTables.Rows[i]["ID"].ToString() + userTables.Rows[i]["address"].ToString() + userTables.Rows[i]["names"].ToString();
                                item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                item[i].Location = new System.Drawing.Point(0, 80 * i);
                                //item[i].Name = "item" + i;
                                //item[i].Name = address[i] + Names[i];
                                item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                item[i].MinimumSize = new Size(450, 80);
                                item[i].TabIndex = i;
                                this.plDownList.Controls.Add(item[i]);
                                //is_any_print = 1;
                                //print_num++;
                            }
                            Order temp_order = new Order(this, Color.LightBlue, userTables.Rows[i]["ID"].ToString(), order_count++, userTables.Rows[i]["paperType"].ToString(), userTables.Rows[i]["remark"].ToString(), userTables.Rows[i]["address"].ToString(), userTables.Rows[i]["names"].ToString(), userTables.Rows[i]["files"].ToString(), "7001");
                            temp_order.Name = userTables.Rows[i]["ID"].ToString();
                            temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                            this.plDownList.Controls.Add(temp_order);
                        }
                    }
                    if (past != "")
                        SetOrderNumber(order_count - 1, "7001", past);
                    string time_str = "以下是" + userTable.Rows[k]["sendTime"].ToString().Substring(0, userTable.Rows[k]["sendTime"].ToString().IndexOf(' ')) + " " + userTable.Rows[k]["timeQuantum"].ToString() + "送货";
                    time_form times = new time_form(time_str);
                    times.Dock = System.Windows.Forms.DockStyle.Top;
                    this.plDownList.Controls.Add(times);
                }
            }
            return number;
        }

        private int refreshNormal()
        {
            int number = 0;

            //string jsonText = PostDataToUrl("uid" + "=" + uid, "http://www.xiaoyintong.com/v3_school_printer/list");
            //string jsonText = PostDataToUrl("", "http://api.xiaoyintong.dev:8000/api/v1/desktop/order/printing/normal/list");
            StreamReader r = new StreamReader(@"F:\点维工作室\schoolprint-web-version2\schoolprint-web-v2.0\服务器数据.txt", Encoding.Default);
            string jsonText = r.ReadToEnd();
            r.Close();
            if (jsonText.IndexOf("true") != -1)
            {
                int k = 1;
                try
                {
                    if (jsonText != "[]")
                    {
                        JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
                        JArray jInfo = (JArray)jo["message"];
                        int count = jInfo.Count;
                        item = new ListboxItem[count + 2];
                        count++;
                        string[] CustomerId = new string[count];
                        string[] DocumentUrl = new string[count];
                        string[] ID = new string[count];
                        string[] DocumentName = new string[count];
                        string[] PaperType = new string[count];
                        string[] sendWay = new string[count];
                        string[] State = new string[count];
                        string[] address = new string[count];
                        string[] ShoperId = new string[count];
                        string[] Copies = new string[count];
                        string[] PrintMode = new string[count];
                        string[] Remark = new string[count];
                        string[] Phone = new string[count];
                        string[] Names = new string[count];
                        string[] upload_time = new string[count];
                        int[] building = new int[count];
                        string[] department = new string[count];
                        int[] room_num = new int[count];
                        string[] send_time = new string[count];
                        string[] user_id = new string[count];
                        string[] user_class = new string[count];
                        string[] files = new string[count];
                        foreach (JToken jt in jInfo)
                        {
                            if (k == 300)
                            {
                                count = 301;
                                break;
                            }
                            //CustomerId[k] = row["CustomerId"].ToString();
                            //user_id[k] = row.user_id;
                            //user_class[k] = row.member_type;
                            //user_class[k] = jt["member_type"].ToString(); //row.member_type
                            user_class[k] = "7000";
                            //CustomerId[k] = row.CustomerId;
                            //DocumentUrl[k] = row.file_url;
                            ID[k] = jt["order_tid"].ToString(); //row.tid;
                            //DocumentName[k] = row.file_name;
                            PaperType[k] = jt["printing"].ToString();//row.file_msg;
                            //sendWay[k] = row.sendWay;
                            //State[k] = row.send_status;
                            address[k] = jt["user_address"].ToString();//row.loc;
                            //ShoperId[k] = row.ShoperId;
                            //Copies[k] = row.file_others;
                            //PrintMode[k] = row.PrintMode;
                            Remark[k] = jt["message"].ToString();//row.message;
                            //Phone[k] = row.Phone;
                            Names[k] = jt["user_information"].ToString();//row.user;
                            upload_time[k] = jt["uploaded_time"].ToString();//row.upload_time;
                            send_time[k] = jt["delivery_time"].ToString();//row.send_time;
                            files[k] = jt["files"].ToString();
                            department[k] = address[k].Substring(0, 2);//row.loc.Substring(0, 2);
                            building[k] = Convert.ToInt32(address[k].Substring(address[k].IndexOf(" ") + 1, address[k].IndexOf("栋") - address[k].IndexOf(" ") - 1));
                            try
                            {
                                room_num[k] = Convert.ToInt32(address[k].Substring(address[k].IndexOf("栋") + 2, address[k].IndexOf("室") - address[k].IndexOf('栋') - 2));
                            }
                            catch
                            {
                                MessageBox.Show(address[k] + "|" + (address[k].IndexOf("室") - address[k].IndexOf('栋')));
                            }

                            k++;
                        }

                        int[,] zisong = new int[14, MAXSIZE];
                        int[,] qinyuan = new int[14, MAXSIZE];
                        int[,] yunyuan = new int[29, MAXSIZE];
                        int[] z_room = new int[14];
                        int[] q_room = new int[14];
                        int[] y_room = new int[29];
                        #region 普通用户
                        for (int i = 1; i < count; i++)
                        {
                            if (user_class[i] == "7001")
                                continue;
                            if (department[i] == "韵苑")
                            {
                                if (y_room[building[i]] == 0)
                                {
                                    yunyuan[building[i], y_room[building[i]]] = i;
                                    y_room[building[i]]++;
                                }
                                else if ((y_room[building[i]] > 0) && (room_num[yunyuan[building[i], y_room[building[i]] - 1]] <= room_num[i]))
                                {
                                    yunyuan[building[i], y_room[building[i]]] = i;
                                    y_room[building[i]]++;
                                }
                                else
                                {
                                    int out_for = 0;
                                    for (int x = 0; x < y_room[building[i]]; x++)
                                    {
                                        if (room_num[yunyuan[building[i], x]] > room_num[i])
                                        {
                                            for (int p = y_room[building[i]]; p > x; p--)
                                            {
                                                yunyuan[building[i], p] = yunyuan[building[i], p - 1];
                                            }
                                            yunyuan[building[i], x] = i;
                                            out_for = 1;
                                        }
                                        if (out_for == 1)
                                            break;
                                    }
                                    y_room[building[i]]++;
                                }
                            }
                            else if (department[i] == "沁苑")
                            {
                                if (q_room[building[i]] == 0)
                                {
                                    qinyuan[building[i], q_room[building[i]]] = i;
                                    q_room[building[i]]++;
                                }
                                else if ((q_room[building[i]] > 0) && (room_num[qinyuan[building[i], q_room[building[i]] - 1]] <= room_num[i]))
                                {
                                    qinyuan[building[i], q_room[building[i]]] = i;
                                    q_room[building[i]]++;
                                }
                                else
                                {
                                    int out_for = 0;
                                    for (int x = 0; x < q_room[building[i]]; x++)
                                    {
                                        if (room_num[qinyuan[building[i], x]] > room_num[i])
                                        {
                                            for (int p = q_room[building[i]]; p > x; p--)
                                            {
                                                qinyuan[building[i], p] = qinyuan[building[i], p - 1];
                                            }
                                            qinyuan[building[i], x] = i;
                                            out_for = 1;
                                        }
                                        if (out_for == 1)
                                            break;
                                    }
                                    q_room[building[i]]++;
                                }
                            }
                            else if (department[i] == "紫崧")
                            {
                                if (z_room[building[i]] == 0)
                                {
                                    zisong[building[i], z_room[building[i]]] = i;
                                    z_room[building[i]]++;
                                }
                                else if ((z_room[building[i]] > 0) && (room_num[zisong[building[i], z_room[building[i]] - 1]] <= room_num[i]))
                                {
                                    zisong[building[i], z_room[building[i]]] = i;
                                    z_room[building[i]]++;
                                }
                                else
                                {
                                    int out_for = 0;
                                    for (int x = 0; x < z_room[building[i]]; x++)
                                    {
                                        if (room_num[zisong[building[i], x]] > room_num[i])
                                        {
                                            for (int p = z_room[building[i]]; p > x; p--)
                                            {
                                                zisong[building[i], p] = zisong[building[i], p - 1];
                                            }
                                            zisong[building[i], x] = i;
                                            out_for = 1;
                                        }
                                        if (out_for == 1)
                                            break;
                                    }
                                    z_room[building[i]]++;
                                }
                            }
                            else
                            {
                                MessageBox.Show(department[i] + "-------地址有误!");
                            }
                        }

                        int print_num = 0;
                        int color_change = 0;
                        string times_str = " ";
                        string past = "";
                        int order_count = 1;
                        for (int time_num = 1; time_num <= 4; time_num++)
                        {
                            int is_any_print = 0;
                            int time_unknow = 0;
                            switch (time_num.ToString())
                            {
                                case "4":
                                    times_str = "今天, 12:15-13:00";
                                    break;
                                case "3":
                                    times_str = "今天, 22:00-23:00";
                                    break;
                                case "2":
                                    times_str = "明天, 12:15-13:00";
                                    break;
                                case "1":
                                    times_str = "明天, 22:00-23:00";
                                    break;
                                case "5":
                                    time_unknow = 1;
                                    break;
                                default: break;
                            }
                            for (int p = 28; p > 0; p--)
                            {
                                //int order_count = 1;
                                for (int q = 0; q < y_room[p]; q++)
                                {
                                    int i = yunyuan[p, q];
                                    if ((times_str != send_time[i]) && time_unknow == 0)
                                        continue;
                                    if ((address[i] + Names[i]) != past)
                                    {
                                        if (past != "")
                                            SetOrderNumber(order_count - 1, user_class[i], past);
                                        color_change = (color_change + 1) % 2;
                                        past = address[i] + Names[i];
                                        print_message temp = new print_message(address[i], Names[i], this, user_class[i]);
                                        temp.Dock = System.Windows.Forms.DockStyle.Top;
                                        temp.Name = address[i] + Names[i];
                                        isolation temp_isolation = new isolation();
                                        temp_isolation.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.panel1.Controls.Add(temp_isolation);
                                        this.panel1.Controls.Add(temp);
                                        if (color_change == 1)
                                            temp.color_set(Color.LightYellow);
                                        else
                                            temp.color_set(Color.LightBlue);
                                        order_count = 1;
                                    }
                                    if (color_change == 1)
                                    {
                                        files[i] = "{'files': " + files[i] + "}";
                                        JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
                                        JArray JAtemp = (JArray)JOtemp["files"];
                                        foreach (JToken temp_token in JAtemp)
                                        {
                                            item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightYellow, user_id[i], user_class[i]);
                                            //item[i].Name = i.ToString();
                                            item[i].Name = ID[i] + address[i] + Names[i];
                                            item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                            item[i].Location = new System.Drawing.Point(0, 80 * i);
                                            //item[i].Name = "item" + i;
                                            //item[i].Name = address[i] + Names[i];
                                            item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                            item[i].MinimumSize = new Size(450, 80);
                                            item[i].TabIndex = i;
                                            this.panel1.Controls.Add(item[i]);
                                            is_any_print = 1;
                                            print_num++;
                                        }
                                        Order temp_order = new Order(this, Color.LightYellow, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
                                        temp_order.Name = ID[i];
                                        temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.panel1.Controls.Add(temp_order);
                                    }
                                    else
                                    {
                                        files[i] = "{'files': " + files[i] + "}";
                                        JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
                                        JArray JAtemp = (JArray)JOtemp["files"];
                                        foreach (JToken temp_token in JAtemp)
                                        {
                                            item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightBlue, user_id[i], user_class[i]);
                                            //item[i].Name = i.ToString();
                                            item[i].Name = ID[i] + address[i] + Names[i];
                                            item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                            item[i].Location = new System.Drawing.Point(0, 80 * i);
                                            //item[i].Name = "item" + i;
                                            //item[i].Name = address[i] + Names[i];
                                            item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                            item[i].MinimumSize = new Size(450, 80);
                                            item[i].TabIndex = i;
                                            this.panel1.Controls.Add(item[i]);
                                            is_any_print = 1;
                                            print_num++;
                                        }
                                        Order temp_order = new Order(this, Color.LightBlue, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
                                        temp_order.Name = ID[i];
                                        temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.panel1.Controls.Add(temp_order);
                                    }
                                }
                            }

                            for (int p = 13; p > 0; p--)
                            {
                                //int order_count = 1;
                                for (int q = 0; q < q_room[p]; q++)
                                {
                                    int i = qinyuan[p, q];
                                    if ((times_str != send_time[i]) && time_unknow == 0)
                                        continue;
                                    if ((address[i] + Names[i]) != past)
                                    {
                                        if (past != "")
                                            SetOrderNumber(order_count - 1, user_class[i], past);
                                        color_change = (color_change + 1) % 2;
                                        past = address[i] + Names[i];
                                        print_message temp = new print_message(address[i], Names[i], this, user_class[i]);
                                        temp.Dock = System.Windows.Forms.DockStyle.Top;
                                        temp.Name = address[i] + Names[i];
                                        isolation temp_isolation = new isolation();
                                        temp_isolation.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.panel1.Controls.Add(temp_isolation);
                                        this.panel1.Controls.Add(temp);
                                        if (color_change == 1)
                                            temp.color_set(Color.LightYellow);
                                        else
                                            temp.color_set(Color.LightBlue);
                                        order_count = 1;
                                    }
                                    if (color_change == 1)
                                    {
                                        files[i] = "{'files': " + files[i] + "}";
                                        JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
                                        JArray JAtemp = (JArray)JOtemp["files"];
                                        foreach (JToken temp_token in JAtemp)
                                        {
                                            item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightYellow, user_id[i], user_class[i]);
                                            //item[i].Name = i.ToString();
                                            item[i].Name = ID[i] + address[i] + Names[i];
                                            item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                            item[i].Location = new System.Drawing.Point(0, 80 * i);
                                            //item[i].Name = "item" + i;
                                            //item[i].Name = address[i] + Names[i];
                                            item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                            item[i].MinimumSize = new Size(450, 80);
                                            item[i].TabIndex = i;
                                            this.panel1.Controls.Add(item[i]);
                                            is_any_print = 1;
                                            print_num++;
                                        }
                                        Order temp_order = new Order(this, Color.LightYellow, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
                                        temp_order.Name = ID[i];
                                        temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.panel1.Controls.Add(temp_order);
                                    }
                                    else
                                    {
                                        files[i] = "{'files': " + files[i] + "}";
                                        JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
                                        JArray JAtemp = (JArray)JOtemp["files"];
                                        foreach (JToken temp_token in JAtemp)
                                        {
                                            item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightBlue, user_id[i], user_class[i]);
                                            //item[i].Name = i.ToString();
                                            item[i].Name = ID[i] + address[i] + Names[i];
                                            item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                            item[i].Location = new System.Drawing.Point(0, 80 * i);
                                            //item[i].Name = "item" + i;
                                            //item[i].Name = address[i] + Names[i];
                                            item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                            item[i].MinimumSize = new Size(450, 80);
                                            item[i].TabIndex = i;
                                            this.panel1.Controls.Add(item[i]);
                                            is_any_print = 1;
                                            print_num++;
                                        }
                                        Order temp_order = new Order(this, Color.LightBlue, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
                                        temp_order.Name = ID[i];
                                        temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.panel1.Controls.Add(temp_order);
                                    }
                                }
                            }

                            for (int p = 13; p > 0; p--)
                            {
                                //int order_count = 1;
                                for (int q = 0; q < z_room[p]; q++)
                                {
                                    int i = zisong[p, q];
                                    if ((times_str != send_time[i]) && time_unknow == 0)
                                        continue;
                                    if ((address[i] + Names[i]) != past)
                                    {
                                        if (past != "")
                                            SetOrderNumber(order_count - 1, user_class[i], past);
                                        color_change = (color_change + 1) % 2;
                                        past = address[i] + Names[i];
                                        print_message temp = new print_message(address[i], Names[i], this, user_class[i]);
                                        temp.Dock = System.Windows.Forms.DockStyle.Top;
                                        temp.Name = address[i] + Names[i];
                                        isolation temp_isolation = new isolation();
                                        temp_isolation.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.panel1.Controls.Add(temp_isolation);
                                        this.panel1.Controls.Add(temp);
                                        if (color_change == 1)
                                            temp.color_set(Color.LightYellow);
                                        else
                                            temp.color_set(Color.LightBlue);
                                        order_count = 1;
                                    }
                                    if (color_change == 1)
                                    {
                                        files[i] = "{'files': " + files[i] + "}";
                                        JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
                                        JArray JAtemp = (JArray)JOtemp["files"];
                                        foreach (JToken temp_token in JAtemp)
                                        {
                                            item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightYellow, user_id[i], user_class[i]);
                                            //item[i].Name = i.ToString();
                                            item[i].Name = ID[i] + address[i] + Names[i];
                                            item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                            item[i].Location = new System.Drawing.Point(0, 80 * i);
                                            //item[i].Name = "item" + i;
                                            //item[i].Name = address[i] + Names[i];
                                            item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                            item[i].MinimumSize = new Size(450, 80);
                                            item[i].TabIndex = i;
                                            this.panel1.Controls.Add(item[i]);
                                            is_any_print = 1;
                                            print_num++;
                                        }
                                        Order temp_order = new Order(this, Color.LightYellow, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
                                        temp_order.Name = ID[i];
                                        temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.panel1.Controls.Add(temp_order);
                                    }
                                    else
                                    {
                                        files[i] = "{'files': " + files[i] + "}";
                                        JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
                                        JArray JAtemp = (JArray)JOtemp["files"];
                                        foreach (JToken temp_token in JAtemp)
                                        {
                                            item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightBlue, user_id[i], user_class[i]);
                                            //item[i].Name = i.ToString();
                                            item[i].Name = ID[i] + address[i] + Names[i];
                                            item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                            item[i].Location = new System.Drawing.Point(0, 80 * i);
                                            //item[i].Name = "item" + i;
                                            //item[i].Name = address[i] + Names[i];
                                            item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                            item[i].MinimumSize = new Size(450, 80);
                                            item[i].TabIndex = i;
                                            this.panel1.Controls.Add(item[i]);
                                            is_any_print = 1;
                                            print_num++;
                                        }
                                        Order temp_order = new Order(this, Color.LightBlue, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
                                        temp_order.Name = ID[i];
                                        temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.panel1.Controls.Add(temp_order);
                                    }
                                }
                            }
                            if (past != "")
                                SetOrderNumber(order_count - 1, "7000", past);
                            past = "";
                            if (is_any_print != 1)
                                continue;
                            string time_str = "以下是" + times_str + "送货";
                            if (time_unknow == 1)
                                time_str = "以下时间未能正确解析！";
                            time_form times = new time_form(time_str);
                            times.Dock = System.Windows.Forms.DockStyle.Top;
                            this.panel1.Controls.Add(times);
                        }


                        #endregion

                        CustomerId = null;
                        DocumentUrl = null;
                        ID = null;
                        DocumentName = null;
                        PaperType = null;
                        sendWay = null;
                        State = null;
                        address = null;
                        ShoperId = null;
                        Copies = null;
                        PrintMode = null;
                        Remark = null;
                        Phone = null;
                        Names = null;
                        upload_time = null;
                        building = null;
                        department = null;
                        room_num = null;
                        send_time = null;
                        user_id = null;
                        user_class = null;

                        number = count - 1;
                    }
                    else
                    {
                        number = 0;
                    }
                }
                catch (Exception e)
                {
                    //MessageBox.Show(k.ToString());
                    MessageBox.Show("普通用户部分出错了！\n" + e.ToString());
                    number = 0;
                }
            }
            GC.Collect();

            return number;
        }

        private int refreshVIP()
        {
            int number = 0;

            //string jsonText = PostDataToUrl("uid" + "=" + uid, "http://www.xiaoyintong.com/v3_school_printer/list");
            string jsonText = PostDataToUrl("", "http://api.xiaoyintong.dev:8000/api/v1/desktop/order/printing/vip/list");
            //StreamReader r = new StreamReader(@"L:\点维工作室\schoolprint-web\服务器数据.txt", Encoding.Default);
            //string jsonText = r.ReadToEnd();
            //r.Close();
            if (jsonText.IndexOf("true") != -1)
            {
                int k = 1;
                try
                {
                    if (jsonText != "[]")
                    {
                        JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
                        JArray jInfo = (JArray)jo["message"];
                        int count = jInfo.Count;
                        item = new ListboxItem[count + 2];
                        count++;
                        string[] CustomerId = new string[count];
                        string[] DocumentUrl = new string[count];
                        string[] ID = new string[count];
                        string[] DocumentName = new string[count];
                        string[] PaperType = new string[count];
                        string[] sendWay = new string[count];
                        string[] State = new string[count];
                        string[] address = new string[count];
                        string[] ShoperId = new string[count];
                        string[] Copies = new string[count];
                        string[] PrintMode = new string[count];
                        string[] Remark = new string[count];
                        string[] Phone = new string[count];
                        string[] Names = new string[count];
                        string[] upload_time = new string[count];
                        int[] building = new int[count];
                        string[] department = new string[count];
                        int[] room_num = new int[count];
                        string[] send_time = new string[count];
                        string[] user_id = new string[count];
                        string[] user_class = new string[count];
                        string[] files = new string[count];
                        foreach (JToken jt in jInfo)
                        {
                            if (k == 300)
                            {
                                count = 301;
                                break;
                            }
                            //CustomerId[k] = row["CustomerId"].ToString();
                            //user_id[k] = row.user_id;
                            //user_class[k] = row.member_type;
                            //user_class[k] = jt["member_type"].ToString(); //row.member_type
                            user_class[k] = "7001";
                            //CustomerId[k] = row.CustomerId;
                            //DocumentUrl[k] = row.file_url;
                            ID[k] = jt["order_tid"].ToString(); //row.tid;
                            //DocumentName[k] = row.file_name;
                            PaperType[k] = jt["printing"].ToString();//row.file_msg;
                            //sendWay[k] = row.sendWay;
                            //State[k] = row.send_status;
                            address[k] = jt["user_address"].ToString();//row.loc;
                            //ShoperId[k] = row.ShoperId;
                            //Copies[k] = row.file_others;
                            //PrintMode[k] = row.PrintMode;
                            Remark[k] = jt["message"].ToString();//row.message;
                            //Phone[k] = row.Phone;
                            Names[k] = jt["user_information"].ToString();//row.user;
                            upload_time[k] = jt["uploaded_time"].ToString();//row.upload_time;
                            send_time[k] = jt["delivery_time"].ToString();//row.send_time;
                            files[k] = jt["files"].ToString();
                            department[k] = address[k].Substring(0, 2);//row.loc.Substring(0, 2);
                            building[k] = Convert.ToInt32(address[k].Substring(address[k].IndexOf(" ") + 1, address[k].IndexOf("栋") - address[k].IndexOf(" ") - 1));
                            try
                            {
                                room_num[k] = Convert.ToInt32(address[k].Substring(address[k].IndexOf("栋") + 2, address[k].IndexOf("室") - address[k].IndexOf('栋') - 2));
                            }
                            catch
                            {
                                MessageBox.Show(address[k] + "|" + (address[k].IndexOf("室") - address[k].IndexOf('栋')));
                            }

                            k++;
                        }

                        int[,] zisong = new int[14, MAXSIZE];
                        int[,] qinyuan = new int[14, MAXSIZE];
                        int[,] yunyuan = new int[29, MAXSIZE];
                        int[] z_room = new int[14];
                        int[] q_room = new int[14];
                        int[] y_room = new int[29];
                        #region VIP
                        for (int i = 1; i < count; i++)
                        {
                            if (department[i] == "韵苑")
                            {
                                if (y_room[building[i]] == 0)
                                {
                                    yunyuan[building[i], y_room[building[i]]] = i;
                                    y_room[building[i]]++;
                                }
                                else if ((y_room[building[i]] > 0) && (room_num[yunyuan[building[i], y_room[building[i]] - 1]] <= room_num[i]))
                                {
                                    yunyuan[building[i], y_room[building[i]]] = i;
                                    y_room[building[i]]++;
                                }
                                else
                                {
                                    int out_for = 0;
                                    for (int x = 0; x < y_room[building[i]]; x++)
                                    {
                                        if (room_num[yunyuan[building[i], x]] > room_num[i])
                                        {
                                            for (int p = y_room[building[i]]; p > x; p--)
                                            {
                                                yunyuan[building[i], p] = yunyuan[building[i], p - 1];
                                            }
                                            yunyuan[building[i], x] = i;
                                            out_for = 1;
                                        }
                                        if (out_for == 1)
                                            break;
                                    }
                                    y_room[building[i]]++;
                                }
                            }
                            else if (department[i] == "沁苑")
                            {
                                if (q_room[building[i]] == 0)
                                {
                                    qinyuan[building[i], q_room[building[i]]] = i;
                                    q_room[building[i]]++;
                                }
                                else if ((q_room[building[i]] > 0) && (room_num[qinyuan[building[i], q_room[building[i]] - 1]] <= room_num[i]))
                                {
                                    qinyuan[building[i], q_room[building[i]]] = i;
                                    q_room[building[i]]++;
                                }
                                else
                                {
                                    int out_for = 0;
                                    for (int x = 0; x < q_room[building[i]]; x++)
                                    {
                                        if (room_num[qinyuan[building[i], x]] > room_num[i])
                                        {
                                            for (int p = q_room[building[i]]; p > x; p--)
                                            {
                                                qinyuan[building[i], p] = qinyuan[building[i], p - 1];
                                            }
                                            qinyuan[building[i], x] = i;
                                            out_for = 1;
                                        }
                                        if (out_for == 1)
                                            break;
                                    }
                                    q_room[building[i]]++;
                                }
                            }
                            else if (department[i] == "紫崧")
                            {
                                if (z_room[building[i]] == 0)
                                {
                                    zisong[building[i], z_room[building[i]]] = i;
                                    z_room[building[i]]++;
                                }
                                else if ((z_room[building[i]] > 0) && (room_num[zisong[building[i], z_room[building[i]] - 1]] <= room_num[i]))
                                {
                                    zisong[building[i], z_room[building[i]]] = i;
                                    z_room[building[i]]++;
                                }
                                else
                                {
                                    int out_for = 0;
                                    for (int x = 0; x < z_room[building[i]]; x++)
                                    {
                                        if (room_num[zisong[building[i], x]] > room_num[i])
                                        {
                                            for (int p = z_room[building[i]]; p > x; p--)
                                            {
                                                zisong[building[i], p] = zisong[building[i], p - 1];
                                            }
                                            zisong[building[i], x] = i;
                                            out_for = 1;
                                        }
                                        if (out_for == 1)
                                            break;
                                    }
                                    z_room[building[i]]++;
                                }
                            }
                            else
                            {
                                MessageBox.Show(department[i] + "-------地址有误!");
                            }
                        }

                        int print_num = 0;
                        int color_change = 0;
                        string times_str = " ";
                        string past = "";
                        for (int time_num = 1; time_num <= 4; time_num++)
                        {
                            int is_any_print = 0;
                            int time_unknow = 0;
                            int order_count = 1;
                            switch (time_num.ToString())
                            {
                                case "4":
                                    times_str = "今天, 12:15-13:00";
                                    break;
                                case "3":
                                    times_str = "今天, 22:00-23:00";
                                    break;
                                case "2":
                                    times_str = "明天, 12:15-13:00";
                                    break;
                                case "1":
                                    times_str = "明天, 22:00-23:00";
                                    break;
                                case "5":
                                    time_unknow = 1;
                                    break;
                                default: break;
                            }
                            for (int p = 28; p > 0; p--)
                            {
                                //int order_count = 1;
                                for (int q = 0; q < y_room[p]; q++)
                                {
                                    int i = yunyuan[p, q];
                                    if ((times_str != send_time[i]) && time_unknow == 0)
                                        continue;
                                    if ((address[i] + Names[i]) != past)
                                    {
                                        if (past != "")
                                            SetOrderNumber(order_count - 1, user_class[i], past);
                                        color_change = (color_change + 1) % 2;
                                        past = address[i] + Names[i];
                                        print_message temp = new print_message(address[i], Names[i], this, user_class[i]);
                                        temp.Dock = System.Windows.Forms.DockStyle.Top;
                                        temp.Name = address[i] + Names[i];
                                        isolation temp_isolation = new isolation();
                                        temp_isolation.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.plDownList.Controls.Add(temp_isolation);
                                        this.plDownList.Controls.Add(temp);
                                        if (color_change == 1)
                                            temp.color_set(Color.LightYellow);
                                        else
                                            temp.color_set(Color.LightBlue);
                                        order_count = 1;
                                    }
                                    if (color_change == 1)
                                    {
                                        files[i] = "{'files': " + files[i] + "}";
                                        JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
                                        JArray JAtemp = (JArray)JOtemp["files"];
                                        foreach (JToken temp_token in JAtemp)
                                        {
                                            item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightYellow, user_id[i], user_class[i]);
                                            //item[i].Name = i.ToString();
                                            item[i].Name = ID[i] + address[i] + Names[i];
                                            item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                            item[i].Location = new System.Drawing.Point(0, 80 * i);
                                            //item[i].Name = "item" + i;
                                            //item[i].Name = address[i] + Names[i];
                                            item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                            item[i].MinimumSize = new Size(450, 80);
                                            item[i].TabIndex = i;
                                            this.plDownList.Controls.Add(item[i]);
                                            is_any_print = 1;
                                            print_num++;
                                        }
                                        Order temp_order = new Order(this, Color.LightYellow, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
                                        temp_order.Name = ID[i];
                                        temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.plDownList.Controls.Add(temp_order);
                                    }
                                    else
                                    {
                                        files[i] = "{'files': " + files[i] + "}";
                                        JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
                                        JArray JAtemp = (JArray)JOtemp["files"];
                                        foreach (JToken temp_token in JAtemp)
                                        {
                                            item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightBlue, user_id[i], user_class[i]);
                                            //item[i].Name = i.ToString();
                                            item[i].Name = ID[i] + address[i] + Names[i];
                                            item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                            item[i].Location = new System.Drawing.Point(0, 80 * i);
                                            //item[i].Name = "item" + i;
                                            //item[i].Name = address[i] + Names[i];
                                            item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                            item[i].MinimumSize = new Size(450, 80);
                                            item[i].TabIndex = i;
                                            this.plDownList.Controls.Add(item[i]);
                                            is_any_print = 1;
                                            print_num++;
                                        }
                                        Order temp_order = new Order(this, Color.LightBlue, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
                                        temp_order.Name = ID[i];
                                        temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.plDownList.Controls.Add(temp_order);
                                    }
                                }
                            }

                            for (int p = 13; p > 0; p--)
                            {
                                //int order_count = 1;
                                for (int q = 0; q < q_room[p]; q++)
                                {
                                    int i = qinyuan[p, q];
                                    if ((times_str != send_time[i]) && time_unknow == 0)
                                        continue;
                                    if ((address[i] + Names[i]) != past)
                                    {
                                        if (past != "")
                                            SetOrderNumber(order_count - 1, user_class[i], past);
                                        color_change = (color_change + 1) % 2;
                                        past = address[i] + Names[i];
                                        print_message temp = new print_message(address[i], Names[i], this, user_class[i]);
                                        temp.Dock = System.Windows.Forms.DockStyle.Top;
                                        temp.Name = address[i] + Names[i];
                                        isolation temp_isolation = new isolation();
                                        temp_isolation.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.plDownList.Controls.Add(temp_isolation);
                                        this.plDownList.Controls.Add(temp);
                                        if (color_change == 1)
                                            temp.color_set(Color.LightYellow);
                                        else
                                            temp.color_set(Color.LightBlue);
                                        order_count = 1;
                                    }
                                    if (color_change == 1)
                                    {
                                        files[i] = "{'files': " + files[i] + "}";
                                        JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
                                        JArray JAtemp = (JArray)JOtemp["files"];
                                        foreach (JToken temp_token in JAtemp)
                                        {
                                            item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightYellow, user_id[i], user_class[i]);
                                            //item[i].Name = i.ToString();
                                            item[i].Name = ID[i] + address[i] + Names[i];
                                            item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                            item[i].Location = new System.Drawing.Point(0, 80 * i);
                                            //item[i].Name = "item" + i;
                                            //item[i].Name = address[i] + Names[i];
                                            item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                            item[i].MinimumSize = new Size(450, 80);
                                            item[i].TabIndex = i;
                                            this.plDownList.Controls.Add(item[i]);
                                            is_any_print = 1;
                                            print_num++;
                                        }
                                        Order temp_order = new Order(this, Color.LightYellow, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
                                        temp_order.Name = ID[i];
                                        temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.plDownList.Controls.Add(temp_order);
                                    }
                                    else
                                    {
                                        files[i] = "{'files': " + files[i] + "}";
                                        JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
                                        JArray JAtemp = (JArray)JOtemp["files"];
                                        foreach (JToken temp_token in JAtemp)
                                        {
                                            item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightBlue, user_id[i], user_class[i]);
                                            //item[i].Name = i.ToString();
                                            item[i].Name = ID[i] + address[i] + Names[i];
                                            item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                            item[i].Location = new System.Drawing.Point(0, 80 * i);
                                            //item[i].Name = "item" + i;
                                            //item[i].Name = address[i] + Names[i];
                                            item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                            item[i].MinimumSize = new Size(450, 80);
                                            item[i].TabIndex = i;
                                            this.plDownList.Controls.Add(item[i]);
                                            is_any_print = 1;
                                            print_num++;
                                        }
                                        Order temp_order = new Order(this, Color.LightBlue, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
                                        temp_order.Name = ID[i];
                                        temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.plDownList.Controls.Add(temp_order);
                                    }
                                }
                            }

                            for (int p = 13; p > 0; p--)
                            {
                                //int order_count = 1;
                                for (int q = 0; q < z_room[p]; q++)
                                {
                                    int i = zisong[p, q];
                                    if ((times_str != send_time[i]) && time_unknow == 0)
                                        continue;
                                    if ((address[i] + Names[i]) != past)
                                    {
                                        if (past != "")
                                            SetOrderNumber(order_count - 1, user_class[i], past);
                                        color_change = (color_change + 1) % 2;
                                        past = address[i] + Names[i];
                                        print_message temp = new print_message(address[i], Names[i], this, user_class[i]);
                                        temp.Dock = System.Windows.Forms.DockStyle.Top;
                                        temp.Name = address[i] + Names[i];
                                        isolation temp_isolation = new isolation();
                                        temp_isolation.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.plDownList.Controls.Add(temp_isolation);
                                        this.plDownList.Controls.Add(temp);
                                        if (color_change == 1)
                                            temp.color_set(Color.LightYellow);
                                        else
                                            temp.color_set(Color.LightBlue);
                                        order_count = 1;
                                    }
                                    if (color_change == 1)
                                    {
                                        files[i] = "{'files': " + files[i] + "}";
                                        JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
                                        JArray JAtemp = (JArray)JOtemp["files"];
                                        foreach (JToken temp_token in JAtemp)
                                        {
                                            item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightYellow, user_id[i], user_class[i]);
                                            //item[i].Name = i.ToString();
                                            item[i].Name = ID[i] + address[i] + Names[i];
                                            item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                            item[i].Location = new System.Drawing.Point(0, 80 * i);
                                            //item[i].Name = "item" + i;
                                            //item[i].Name = address[i] + Names[i];
                                            item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                            item[i].MinimumSize = new Size(450, 80);
                                            item[i].TabIndex = i;
                                            this.plDownList.Controls.Add(item[i]);
                                            is_any_print = 1;
                                            print_num++;
                                        }
                                        Order temp_order = new Order(this, Color.LightYellow, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
                                        temp_order.Name = ID[i];
                                        temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.plDownList.Controls.Add(temp_order);
                                    }
                                    else
                                    {
                                        files[i] = "{'files': " + files[i] + "}";
                                        JObject JOtemp = (JObject)JsonConvert.DeserializeObject(files[i]);
                                        JArray JAtemp = (JArray)JOtemp["files"];
                                        foreach (JToken temp_token in JAtemp)
                                        {
                                            item[i] = new ListboxItem(temp_token["fileurl"].ToString(), temp_token["filename"].ToString(), address[i], this, PaperType[i], Copies[i], PrintMode[i], Remark[i], ID[i], Names[i], Phone[i], upload_time[i], send_time[i], Color.LightBlue, user_id[i], user_class[i]);
                                            //item[i].Name = i.ToString();
                                            item[i].Name = ID[i] + address[i] + Names[i];
                                            item[i].Dock = System.Windows.Forms.DockStyle.Top;
                                            item[i].Location = new System.Drawing.Point(0, 80 * i);
                                            //item[i].Name = "item" + i;
                                            //item[i].Name = address[i] + Names[i];
                                            item[i].Size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, 80);
                                            item[i].MinimumSize = new Size(450, 80);
                                            item[i].TabIndex = i;
                                            this.plDownList.Controls.Add(item[i]);
                                            is_any_print = 1;
                                            print_num++;
                                        }
                                        Order temp_order = new Order(this, Color.LightBlue, ID[i], order_count++, PaperType[i], Remark[i], address[i], Names[i], files[i], user_class[i]);
                                        temp_order.Name = ID[i];
                                        temp_order.Dock = System.Windows.Forms.DockStyle.Top;
                                        this.plDownList.Controls.Add(temp_order);
                                    }
                                }
                            }
                            if (past != "")
                                SetOrderNumber(order_count - 1, "7001", past);
                            past = "";
                            if (is_any_print != 1)
                                continue;
                            string time_str = "以下是" + times_str + "送货";
                            if (time_unknow == 1)
                                time_str = "以下时间未能正确解析！";
                            time_form times = new time_form(time_str);
                            times.Dock = System.Windows.Forms.DockStyle.Top;
                            this.plDownList.Controls.Add(times);
                        }


                        #endregion

                        CustomerId = null;
                        DocumentUrl = null;
                        ID = null;
                        DocumentName = null;
                        PaperType = null;
                        sendWay = null;
                        State = null;
                        address = null;
                        ShoperId = null;
                        Copies = null;
                        PrintMode = null;
                        Remark = null;
                        Phone = null;
                        Names = null;
                        upload_time = null;
                        building = null;
                        department = null;
                        room_num = null;
                        send_time = null;
                        user_id = null;
                        user_class = null;

                        number = count - 1;
                    }
                    else
                    {
                        number = 0;
                    }
                }
                catch (Exception e)
                {
                    //MessageBox.Show(k.ToString());
                    MessageBox.Show("VIP用户部分出错了！\n" + e.ToString());
                    number = 0;
                }
            }
            GC.Collect();

            return number;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (button1.Text == "登录系统")
                {
                    //MessageBox.Show(GetMD5Hash(textBox2.Text));
                    string ShoperIds = "email";
                    string Passwords = "password";
                    //string Text = PostDataToUrl(ShoperIds + "=" + textBox1.Text + "&" + Passwords + "=" + GetMD5Hash(textBox2.Text).ToLower(), "http://www.xiaoyintong.com/v3_school_printer/login");
                    //string Text = PostDataToUrl(ShoperIds + "=" + textBox1.Text + "&" + Passwords + "=" + GetMD5Hash(textBox2.Text).ToLower(), "http://api.xiaoyintong.dev:8000/api/v1/desktop/login");
                    string Text = PostDataToUrl(ShoperIds + "=" + textBox1.Text + "&" + Passwords + "=" + GetMD5Hash(textBox2.Text).ToLower(), "http://api.xiaoyintong.com/api/v1/desktop/login");
                    //string Text = "{\"status\":true,\"uid\":5,\"info\":密码不正确}";
                    //byte[] buffer = Encoding.Unicode.GetBytes(Text);
                    //Text = Encoding.Unicode.GetString(buffer);
                    if (Text.IndexOf("true") != -1)//登陆成功
                    {
                        //uid = Text.Substring(Text.IndexOf("uid") + 6, 1);
                        label1.Text = "打印店：" + textBox1.Text;
                        //button1.Text = "注销登录";
                        button1.Text = "刷新";
                        try
                        {
                            StreamReader swr = File.OpenText(Application.StartupPath + "\\文件数据\\test");
                            string name=swr.ReadLine();
                            swr.Close();
                            if(string.Compare(name,textBox1.Text)!=0)
                            {
                                if (MessageBox.Show("记住密码？", "确认信息", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                {
                                    StreamWriter swrw = new StreamWriter(Application.StartupPath + "\\文件数据\\test", false);
                                    swrw.WriteLine(textBox1.Text);
                                    swrw.WriteLine(textBox2.Text);
                                    swrw.Close();
                                }
                            }
                        }
                        catch
                        {
                            if (MessageBox.Show("记住密码？", "确认信息", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            {
                                StreamWriter sw = File.CreateText(Application.StartupPath + "\\文件数据\\test");
                                sw.WriteLine(textBox1.Text);
                                sw.WriteLine(textBox2.Text);
                                sw.Close();
                            }
                        }
                        
                        refrash();
                        //timer1.Enabled = true;
                        //timer2.Enabled = false;
                    }
                    else
                    {
                        //MessageBox.Show("账户和密码错误！");
                        Text = Text.Substring(Text.IndexOf("info")+6);
                        
                        MessageBox.Show(Text);
                    }
                }
                else if(button1.Text=="刷新")
                {
                    refrash();
                    //timer1.Enabled = true;
                    //timer2.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button1.Text != "登录系统")
            {
                if (MessageBox.Show("确定将今天金额清零？", "确认信息", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    SureReset SureForm = new SureReset();
                    SureForm.Show();
                    //新建第二天的文件夹,文件夹名为第二天时间+星期
                    //dayAddress = Application.StartupPath + "\\文件数据\\" + DateTime.Now.AddDays(1).ToLongDateString();
                    //check = DateTime.Now.AddDays(1).ToLongDateString();
                    //ListboxItem.dayAddress = dayAddress;
                    //ListboxItem.check = check;
                    //Directory.CreateDirectory(dayAddress);
                    ////MessageBox.Show(address);
                    ////delete 
                    //sum = 0.0;
                    //label5.Text = sum.ToString() + "元";
                }
            }
            else
            {
                MessageBox.Show("您还未登录！");
            }
        }

        public void NewFileFolder()
        {
            //dayAddress = Application.StartupPath + "\\文件数据\\" + DateTime.Now.AddDays(1).ToLongDateString();
            //check = DateTime.Now.AddDays(1).ToLongDateString();
            //ListboxItem.dayAddress = dayAddress;
            //ListboxItem.check = check;
            //Directory.CreateDirectory(dayAddress);
            //MessageBox.Show(address);
            //delete 
            //Update("ShoperId",textBox1.Text,"money",label6.Text.Remove(label6.Text.IndexOf("元")));
            double money=Convert.ToDouble(label6.Text.Remove(label6.Text.IndexOf("元"))) + Convert.ToDouble(label18.Text.Remove(label18.Text.IndexOf("元")));
            string Text = PostDataToUrl("uid" + "=" + uid + "&" + "c_money" + "=" + money.ToString(), "http://www.xiaoyintong.com/v3_school_printer/commit");
            //MessageBox.Show("uid" + "=" + uid + "&" + "c_money" + "=" + label6.Text.Remove(label6.Text.IndexOf("元")));
            //string Text = "true";
            //MessageBox.Show(label6.Text.Remove(label6.Text.IndexOf("元")));
            if (Text.IndexOf("true") != -1)
            {
                File.Delete(Application.StartupPath + "\\文件数据\\money");
                StreamWriter sr1 = File.CreateText(Application.StartupPath + "\\文件数据\\money");
                sr1.Close();
                double change;
                StreamReader sr = File.OpenText(Application.StartupPath + "\\文件数据\\money2");
                change = Convert.ToDouble(sr.ReadLine()) + Convert.ToDouble(label6.Text.Remove(label6.Text.IndexOf("元")));
                sr.Close();
                File.Delete(Application.StartupPath + "\\文件数据\\money2");
                //File.CreateText(Application.StartupPath + "\\文件数据\\money2");
                StreamWriter srw = new StreamWriter(Application.StartupPath + "\\文件数据\\money2");
                srw.Write(change.ToString());
                srw.Close();
                label8.Text = change.ToString() + "元";

                File.Delete(Application.StartupPath + "\\文件数据\\money3");
                StreamWriter sr2 = File.CreateText(Application.StartupPath + "\\文件数据\\money3");
                sr2.Close();

                File.Delete(Application.StartupPath + "\\文件数据\\money4");

                sum = 0.0;
                label5.Text = sum.ToString() + "元";
                label6.Text = sum.ToString() + "元";
                label11.Text = sum.ToString() + "元";
                label12.Text = sum.ToString() + "元";
                label15.Text = sum.ToString() + "元";
                label16.Text = sum.ToString() + "元";
                label18.Text = sum.ToString() + "元";
                label19.Text = sum.ToString() + "元";
                MessageBox.Show("传送成功！");
            }
            else
            {
                MessageBox.Show("总金额传值错误！");
                Text = Text.Substring(Text.IndexOf("info") + 6);

                MessageBox.Show(Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button1.Text != "登录系统")
            {
                if (File.Exists(dayAddress + "\\" + "今日打印情况.txt"))
                {
                    System.Diagnostics.Process.Start(dayAddress + "\\" + "今日打印情况.txt");
                }
                else
                {
                    MessageBox.Show("今日没有打印数据");
                }
            }
            else
            {
                MessageBox.Show("您还未登录！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int err = 0;
            //MessageBox.Show("caca");
            for (int i = 0; i <= item.Length/*Convert.ToInt32((sender as ListboxItem).Name)*/; i++)
            {
                try
                {
                    item[i].ListboxItem_Start(sender, e);
                }
                catch (System.Exception ex)
                {
                    err++;
                }
            }
            //MessageBox.Show(err.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (CountNomal())
                System.Diagnostics.Process.Start(dayAddress + "\\" + "今日打印情况_按楼栋排列.txt");
            if (CountVIP())
                System.Diagnostics.Process.Start(dayAddress + "\\" + "今日打印情况_按楼栋排列_VIP.txt");

            //int error_line = -1;
            //int arr_mes, i;
            //int x;
            //int[] z_j = new int[14];
            //int[] q_j = new int[14];
            //int[] y_j = new int[29];
            //int[] z_room = new int[14];
            //int[] q_room = new int[14];
            //int[] y_room = new int[29];
            //int[] z_people = new int[14];
            //int[] q_people = new int[14];
            //int[] y_people = new int[29];
            ////MessageBox.Show(q_j[3]+"|");
            //string[,] zisong = new string[14, MAXSIZE];
            //string[,] qinyuan = new string[14, MAXSIZE];
            //string[,] yunyuan = new string[29, MAXSIZE];
            //string[,] zisong_room = new string[14, MAXSIZE];
            //string[,] qinyuan_room = new string[14, MAXSIZE];
            //string[,] yunyuan_room = new string[29, MAXSIZE];
            //double[] z_money = new double[14];
            //double[] q_money = new double[14];
            //double[] y_money = new double[29];
            //string temp, change, reader, head, people_message = "";
            //string only_use = people_message;
            //StreamReader sr;
            //double single_mode = 0.0, double_mode = 0.0, all = 0.0;
            //string single_mode_string = "", double_mode_string = "";
            //int total_people = 0;

//#region nomal user
//            //open the file
//            if (File.Exists(dayAddress + "\\" + "今日打印情况.txt"))
//            {
//                sr = File.OpenText(dayAddress + "\\" + "今日打印情况.txt");
//                //ignore two lines
//                head = sr.ReadLine();
//                temp = sr.ReadLine();
//                if (temp != null)
//                    error_line = 2;

//                //read the record and allocate to the array
//                while ((reader = sr.ReadLine()) != null)
//                {
//                    error_line++;
//                    temp = reader;
//                    if (temp.IndexOf("单面") >= 0)
//                    {
//                        single_mode += Convert.ToDouble(temp.Substring(temp.LastIndexOf("=") + 2));
//                    }
//                    else if (temp.IndexOf("双面") >= 0)
//                    {
//                        double_mode += Convert.ToDouble(temp.Substring(temp.LastIndexOf("=") + 2));
//                    }

//                    if ((arr_mes = temp.IndexOf("紫崧 ")) != -1)
//                    {
//                        temp = temp.Substring(arr_mes + 3);
//                        arr_mes = temp.IndexOf("栋 ");
//                        change = temp.Substring(0, arr_mes);
//                        i = Convert.ToInt32(change);
//                        //
//                        arr_mes = temp.IndexOf("室 ");
//                        change = temp.Substring(0, arr_mes);
//                        for (x = 0; x < z_room[i]; x++)
//                        {
//                            if (zisong_room[i, x] == change)
//                                break;
//                        }
//                        if (x == z_room[i])
//                        {
//                            zisong_room[i, x] = change;
//                            z_room[i]++;
//                            zisong[i, z_j[i]] = reader;
//                            z_j[i]++;
//                        }
//                        else
//                        {
//                            zisong[i, x] += "\r\n" + reader;
//                        }

//                        arr_mes = temp.LastIndexOf("= ");
//                        temp = temp.Substring(arr_mes + 2);
//                        z_money[i] += Convert.ToDouble(temp);
//                    }
//                    else if ((arr_mes = temp.IndexOf("沁苑 ")) != -1)
//                    {
//                        temp = temp.Substring(arr_mes + 3);
//                        arr_mes = temp.IndexOf("栋 ");
//                        change = temp.Substring(0, arr_mes);
//                        i = Convert.ToInt32(change);
//                        //
//                        arr_mes = temp.IndexOf("室 ");
//                        change = temp.Substring(0, arr_mes);
//                        for (x = 0; x < q_room[i]; x++)
//                        {
//                            if (qinyuan_room[i, x] == change)
//                                break;
//                        }
//                        if (x == q_room[i])
//                        {
//                            qinyuan_room[i, x] = change;
//                            q_room[i]++;
//                            qinyuan[i, q_j[i]] = reader;
//                            q_j[i]++;
//                        }
//                        else
//                        {
//                            qinyuan[i, x] += "\r\n" + reader;
//                        }

//                        arr_mes = temp.LastIndexOf("= ");
//                        temp = temp.Substring(arr_mes + 2);
//                        q_money[i] += Convert.ToDouble(temp);
//                    }
//                    else if ((arr_mes = temp.IndexOf("韵苑 ")) != -1)
//                    {
//                        temp = temp.Substring(arr_mes + 3);
//                        arr_mes = temp.IndexOf("栋 ");
//                        change = temp.Substring(0, arr_mes);
//                        i = Convert.ToInt32(change);
//                        //
//                        arr_mes = temp.IndexOf("室 ");
//                        change = temp.Substring(0, arr_mes);
//                        for (x = 0; x < y_room[i]; x++)
//                        {
//                            if (yunyuan_room[i, x] == change)
//                                break;
//                        }
//                        if (x == y_room[i])
//                        {
//                            yunyuan_room[i, x] = change;
//                            y_room[i]++;
//                            yunyuan[i, y_j[i]] = reader;
//                            y_j[i]++;
//                        }
//                        else
//                        {
//                            yunyuan[i, x] += "\r\n" + reader;
//                        }
//                        arr_mes = temp.LastIndexOf("= ");
//                        temp = temp.Substring(arr_mes + 2);
//                        y_money[i] += Convert.ToDouble(temp);
//                    }
//                    else if (temp == "")
//                    {
//                        ;
//                    }
//                    else
//                    {
//                        MessageBox.Show("今日打印情况文件中第" + error_line + "行有错误楼栋信息！");
//                        error_line = -1;
//                    }
//                    if (error_line == -1)
//                        break;
//                }
//                sr.Close();  //close the source file

//                //write to new file
//                if (error_line != -1)
//                {
//                    if (File.Exists(dayAddress + "\\" + "今日打印情况_按楼栋排列.txt"))
//                    {
//                        File.Delete(dayAddress + "\\" + "今日打印情况_按楼栋排列.txt");
//                    }
//                    if (File.Exists(dayAddress + "\\" + "打印小结.csv"))
//                    {
//                        File.Delete(dayAddress + "\\" + "打印小结.csv");
//                    }
//                    StreamWriter sw = File.CreateText(dayAddress + "\\" + "今日打印情况_按楼栋排列.txt");
//                    FileStream sw_csv = File.Create(dayAddress + "\\" + "打印小结.csv");
//                    sw.WriteLine(head);
//                    sw.WriteLine("单面总金额：" + single_mode);
//                    sw.WriteLine("双面总金额：" + double_mode);
//                    sw.WriteLine(" ");

//                    Excel.Application excel = new Excel.Application();
//                    excel.Visible = false;
//                    Excel.Workbooks workBooks = excel.Workbooks;
//                    Excel.Workbook workBook;
//                    Excel.Sheets sheet_old;
//                    Excel.Worksheet sheet;
//                    int row = 1;
//                    if (File.Exists(Application.StartupPath + "\\文件数据\\" + "xiaojie.xlsx"))
//                    {
//                        workBook = workBooks.Open(Application.StartupPath + "\\文件数据\\" + "xiaojie.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
//                        sheet_old = workBook.Sheets;
//                        sheet = (Excel.Worksheet)sheet_old.Add(sheet_old[1], Type.Missing, Type.Missing, Type.Missing);
//                        try
//                        {
//                            sheet.Name = DateTime.Now.ToLongDateString().ToString();
//                        }
//                        catch (System.Exception ex)
//                        {
//                            excel.DisplayAlerts = false;
//                            ((Excel.Worksheet)workBook.Worksheets[1]).Delete();
//                            try
//                            {
//                                ((Excel.Worksheet)workBook.Worksheets[1]).Delete();
//                            }
//                            catch
//                            {
//                                ;
//                            }
//                            excel.DisplayAlerts = true;
//                            sheet = (Excel.Worksheet)sheet_old.Add(sheet_old[1], Type.Missing, Type.Missing, Type.Missing);
//                            sheet.Name = DateTime.Now.ToLongDateString().ToString();
//                        }

//                    }
//                    else
//                    {
//                        object Nothing = System.Reflection.Missing.Value;
//                        workBook = excel.Workbooks.Add(Nothing);
//                        sheet = (Excel.Worksheet)workBook.Sheets[1];
//                        sheet.Name = DateTime.Now.ToLongDateString().ToString();
//                    }

//                    for (i = 1; i < 14; i++)
//                    {
//                        if (z_j[i] != 0)
//                        {
//                            int num_people;
//                            int p;
//                            string past = " ";
//                            for (p = 0, num_people = 0; p < z_j[i]; p++)
//                            {
//                                people_message = zisong[i, p].Substring(zisong[i, p].IndexOf(" "), zisong[i, p].IndexOf("紫崧") - zisong[i, p].IndexOf(" "));
//                                if (people_message != past)
//                                    num_people++;
//                            }
//                            sw.WriteLine("紫菘" + i + "栋" + ":" + z_money[i] + "元,共" + z_room[i] + "个寝室,共" + num_people + "人");
//                            total_people += num_people;
//                            sw.WriteLine();
//                            byte[] byteArray = System.Text.Encoding.Default.GetBytes("紫菘" + i + "栋," + z_money[i] + "," + z_room[i] + "\n");
//                            sw_csv.Write(byteArray, 0, byteArray.Length);

//                            sheet.Cells[row, 1] = "紫菘" + i + "栋";
//                            sheet.Cells[row, 2] = z_money[i].ToString();
//                            sheet.Cells[row, 3] = z_room[i].ToString();
//                            row++;

//                            for (int j = 0; j < z_j[i]; j++)
//                            {
//                                sw.WriteLine(zisong[i, j]);
//                                sw.WriteLine();
//                            }
//                            sw.WriteLine();
//                            sw.WriteLine();
//                            sw.WriteLine();
//                            //sw.WriteLine();
//                        }

//                    }
//                    for (i = 1; i < 14; i++)
//                    {
//                        if (q_j[i] != 0)
//                        {
//                            int num_people;
//                            int p;
//                            string past = " ";
//                            for (p = 0, num_people = 0; p < q_j[i]; p++)
//                            {
//                                people_message = qinyuan[i, p].Substring(qinyuan[i, p].IndexOf(" "), qinyuan[i, p].IndexOf("沁苑") - qinyuan[i, p].IndexOf(" "));
//                                if (people_message != past)
//                                    num_people++;
//                            }
//                            sw.WriteLine("沁苑" + i + "栋" + ":" + q_money[i] + "元,共" + q_room[i] + "个寝室,共" + num_people + "人");
//                            total_people += num_people;
//                            sw.WriteLine();
//                            byte[] byteArray = System.Text.Encoding.Default.GetBytes("沁苑" + i + "栋," + q_money[i] + "," + q_room[i] + "\n");
//                            sw_csv.Write(byteArray, 0, byteArray.Length);

//                            sheet.Cells[row, 1] = "沁苑" + i + "栋";
//                            sheet.Cells[row, 2] = q_money[i].ToString();
//                            sheet.Cells[row, 3] = q_room[i].ToString();
//                            row++;

//                            for (int j = 0; j < q_j[i]; j++)
//                            {
//                                sw.WriteLine(qinyuan[i, j]);
//                                sw.WriteLine();
//                            }
//                            sw.WriteLine();
//                            sw.WriteLine();
//                            sw.WriteLine();
//                            //sw.WriteLine();
//                        }

//                    }
//                    for (i = 1; i < 29; i++)
//                    {
//                        if (y_j[i] != 0)
//                        {
//                            int num_people;
//                            int p;
//                            string past = " ";
//                            for (p = 0, num_people = 0; p < y_j[i]; p++)
//                            {
//                                people_message = yunyuan[i, p].Substring(yunyuan[i, p].IndexOf(" "), yunyuan[i, p].IndexOf("韵苑") - yunyuan[i, p].IndexOf(" "));
//                                if (people_message != past)
//                                    num_people++;
//                            }
//                            sw.WriteLine("韵苑" + i + "栋" + ":" + y_money[i] + "元,共" + y_room[i] + "个寝室,共" + num_people + "人");
//                            total_people += num_people;
//                            sw.WriteLine();
//                            byte[] byteArray = System.Text.Encoding.Default.GetBytes("韵苑" + i + "栋," + y_money[i] + "," + y_room[i] + "\n");
//                            sw_csv.Write(byteArray, 0, byteArray.Length);

//                            sheet.Cells[row, 1] = "韵苑" + i + "栋";
//                            sheet.Cells[row, 2] = y_money[i].ToString();
//                            sheet.Cells[row, 3] = y_room[i].ToString();
//                            row++;

//                            for (int j = 0; j < y_j[i]; j++)
//                            {
//                                sw.WriteLine(yunyuan[i, j]);
//                                sw.WriteLine();
//                            }
//                            sw.WriteLine();
//                            sw.WriteLine();
//                            sw.WriteLine();
//                            //sw.WriteLine();
//                        }
//                    }

//                    sw.WriteLine("一共有" + total_people + "人");

//                    sw.Close();
//                    sw_csv.Close();

//                    sheet.Cells[row, 1] = "总金额";
//                    sheet.Cells[row, 2] = head.Substring(head.IndexOf("：") + 1, head.IndexOf("元") - head.IndexOf("：") - 1);

//                    if (File.Exists(Application.StartupPath + "\\文件数据\\" + "xiaojie.xlsx"))
//                    {
//                        Missing missing = Missing.Value;
//                        workBook.Save();
//                        workBook.Close(missing, missing, missing);
//                        excel.Quit();
//                    }
//                    else
//                    {
//                        Missing missing = Missing.Value;
//                        sheet.SaveAs(Application.StartupPath + "\\文件数据\\" + "xiaojie", missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlShared, missing, missing, missing);
//                        workBook.Close(missing, missing, missing);
//                        excel.Quit();
//                    }

//                    //open the file with default process
//                    System.Diagnostics.Process.Start(dayAddress + "\\" + "今日打印情况_按楼栋排列.txt");
//                }
//            }
//            else
//                MessageBox.Show("没有今日打印情况！");
//#endregion
//#region vip user
//            /*
//            //open the file
//            if (File.Exists(dayAddress + "\\" + "今日打印情况_vip.txt"))
//            {
//                sr = File.OpenText(dayAddress + "\\" + "今日打印情况_vip.txt");
//                //ignore two lines
//                head = sr.ReadLine();
//                temp = sr.ReadLine();
//                if (temp != null)
//                    error_line = 2;

//                //read the record and allocate to the array
//                while ((reader = sr.ReadLine()) != null)
//                {
//                    error_line++;
//                    temp = reader;
//                    if (temp.IndexOf("单面") >= 0)
//                    {
//                        single_mode += Convert.ToDouble(temp.Substring(temp.LastIndexOf("=") + 2));
//                    }
//                    else if (temp.IndexOf("双面") >= 0)
//                    {
//                        double_mode += Convert.ToDouble(temp.Substring(temp.LastIndexOf("=") + 2));
//                    }

//                    if ((arr_mes = temp.IndexOf("紫崧 ")) != -1)
//                    {
//                        temp = temp.Substring(arr_mes + 3);
//                        arr_mes = temp.IndexOf("栋 ");
//                        change = temp.Substring(0, arr_mes);
//                        i = Convert.ToInt32(change);
//                        //
//                        arr_mes = temp.IndexOf("室 ");
//                        change = temp.Substring(0, arr_mes);
//                        for (x = 0; x < z_room[i]; x++)
//                        {
//                            if (zisong_room[i, x] == change)
//                                break;
//                        }
//                        if (x == z_room[i])
//                        {
//                            zisong_room[i, x] = change;
//                            z_room[i]++;
//                            zisong[i, z_j[i]] = reader;
//                            z_j[i]++;
//                        }
//                        else
//                        {
//                            zisong[i, x] += "\r\n" + reader;
//                        }

//                        arr_mes = temp.LastIndexOf("= ");
//                        temp = temp.Substring(arr_mes + 2);
//                        z_money[i] += Convert.ToDouble(temp);
//                    }
//                    else if ((arr_mes = temp.IndexOf("沁苑 ")) != -1)
//                    {
//                        temp = temp.Substring(arr_mes + 3);
//                        arr_mes = temp.IndexOf("栋 ");
//                        change = temp.Substring(0, arr_mes);
//                        i = Convert.ToInt32(change);
//                        //
//                        arr_mes = temp.IndexOf("室 ");
//                        change = temp.Substring(0, arr_mes);
//                        for (x = 0; x < q_room[i]; x++)
//                        {
//                            if (qinyuan_room[i, x] == change)
//                                break;
//                        }
//                        if (x == q_room[i])
//                        {
//                            qinyuan_room[i, x] = change;
//                            q_room[i]++;
//                            qinyuan[i, q_j[i]] = reader;
//                            q_j[i]++;
//                        }
//                        else
//                        {
//                            qinyuan[i, x] += "\r\n" + reader;
//                        }

//                        arr_mes = temp.LastIndexOf("= ");
//                        temp = temp.Substring(arr_mes + 2);
//                        q_money[i] += Convert.ToDouble(temp);
//                    }
//                    else if ((arr_mes = temp.IndexOf("韵苑 ")) != -1)
//                    {
//                        temp = temp.Substring(arr_mes + 3);
//                        arr_mes = temp.IndexOf("栋 ");
//                        change = temp.Substring(0, arr_mes);
//                        i = Convert.ToInt32(change);
//                        //
//                        arr_mes = temp.IndexOf("室 ");
//                        change = temp.Substring(0, arr_mes);
//                        for (x = 0; x < y_room[i]; x++)
//                        {
//                            if (yunyuan_room[i, x] == change)
//                                break;
//                        }
//                        if (x == y_room[i])
//                        {
//                            yunyuan_room[i, x] = change;
//                            y_room[i]++;
//                            yunyuan[i, y_j[i]] = reader;
//                            y_j[i]++;
//                        }
//                        else
//                        {
//                            yunyuan[i, x] += "\r\n" + reader;
//                        }
//                        arr_mes = temp.LastIndexOf("= ");
//                        temp = temp.Substring(arr_mes + 2);
//                        y_money[i] += Convert.ToDouble(temp);
//                    }
//                    else if (temp == "")
//                    {
//                        ;
//                    }
//                    else
//                    {
//                        MessageBox.Show("今日打印情况_vip文件中第" + error_line + "行有错误楼栋信息！");
//                        error_line = -1;
//                    }
//                    if (error_line == -1)
//                        break;
//                }
//                sr.Close();  //close the source file

//                //write to new file
//                if (error_line != -1)
//                {
//                    if (File.Exists(dayAddress + "\\" + "今日打印情况_vip_按楼栋排列.txt"))
//                    {
//                        File.Delete(dayAddress + "\\" + "今日打印情况_vip_按楼栋排列.txt");
//                    }
//                    if (File.Exists(dayAddress + "\\" + "打印小结_vip.csv"))
//                    {
//                        File.Delete(dayAddress + "\\" + "打印小结_vip.csv");
//                    }
//                    StreamWriter sw = File.CreateText(dayAddress + "\\" + "今日打印情况_vip_按楼栋排列.txt");
//                    FileStream sw_csv = File.Create(dayAddress + "\\" + "打印小结_vip.csv");
//                    sw.WriteLine(head);
//                    sw.WriteLine("单面总金额：" + single_mode);
//                    sw.WriteLine("双面总金额：" + double_mode);
//                    sw.WriteLine(" ");

//                    Excel.Application excel = new Excel.Application();
//                    excel.Visible = false;
//                    Excel.Workbooks workBooks = excel.Workbooks;
//                    Excel.Workbook workBook;
//                    Excel.Sheets sheet_old;
//                    Excel.Worksheet sheet;
//                    int row = 1;
//                    if (File.Exists(Application.StartupPath + "\\文件数据\\" + "xiaojie_vip.xlsx"))
//                    {
//                        workBook = workBooks.Open(Application.StartupPath + "\\文件数据\\" + "xiaojie_vip.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
//                        sheet_old = workBook.Sheets;
//                        sheet = (Excel.Worksheet)sheet_old.Add(sheet_old[1], Type.Missing, Type.Missing, Type.Missing);
//                        try
//                        {
//                            sheet.Name = DateTime.Now.ToLongDateString().ToString();
//                        }
//                        catch (System.Exception ex)
//                        {
//                            excel.DisplayAlerts = false;
//                            ((Excel.Worksheet)workBook.Worksheets[1]).Delete();
//                            try
//                            {
//                                ((Excel.Worksheet)workBook.Worksheets[1]).Delete();
//                            }
//                            catch
//                            {
//                                ;
//                            }
//                            excel.DisplayAlerts = true;
//                            sheet = (Excel.Worksheet)sheet_old.Add(sheet_old[1], Type.Missing, Type.Missing, Type.Missing);
//                            sheet.Name = DateTime.Now.ToLongDateString().ToString();
//                        }

//                    }
//                    else
//                    {
//                        object Nothing = System.Reflection.Missing.Value;
//                        workBook = excel.Workbooks.Add(Nothing);
//                        sheet = (Excel.Worksheet)workBook.Sheets[1];
//                        sheet.Name = DateTime.Now.ToLongDateString().ToString();
//                    }

//                    for (i = 1; i < 14; i++)
//                    {
//                        if (z_j[i] != 0)
//                        {
//                            sw.WriteLine("紫菘" + i + "栋" + ":" + z_money[i] + "元,共" + z_room[i] + "个寝室");
//                            sw.WriteLine();
//                            byte[] byteArray = System.Text.Encoding.Default.GetBytes("紫菘" + i + "栋," + z_money[i] + "," + z_room[i] + "\n");
//                            sw_csv.Write(byteArray, 0, byteArray.Length);

//                            sheet.Cells[row, 1] = "紫菘" + i + "栋";
//                            sheet.Cells[row, 2] = z_money[i].ToString();
//                            sheet.Cells[row, 3] = z_room[i].ToString();
//                            row++;

//                            for (int j = 0; j < z_j[i]; j++)
//                            {
//                                sw.WriteLine(zisong[i, j]);
//                                sw.WriteLine();
//                            }
//                            sw.WriteLine();
//                            sw.WriteLine();
//                            sw.WriteLine();
//                            //sw.WriteLine();
//                        }

//                    }
//                    for (i = 1; i < 14; i++)
//                    {
//                        if (q_j[i] != 0)
//                        {
//                            sw.WriteLine("沁苑" + i + "栋" + ":" + q_money[i] + "元,共" + q_room[i] + "个寝室");
//                            sw.WriteLine();
//                            byte[] byteArray = System.Text.Encoding.Default.GetBytes("沁苑" + i + "栋," + q_money[i] + "," + q_room[i] + "\n");
//                            sw_csv.Write(byteArray, 0, byteArray.Length);

//                            sheet.Cells[row, 1] = "沁苑" + i + "栋";
//                            sheet.Cells[row, 2] = q_money[i].ToString();
//                            sheet.Cells[row, 3] = q_room[i].ToString();
//                            row++;

//                            for (int j = 0; j < q_j[i]; j++)
//                            {
//                                sw.WriteLine(qinyuan[i, j]);
//                                sw.WriteLine();
//                            }
//                            sw.WriteLine();
//                            sw.WriteLine();
//                            sw.WriteLine();
//                            //sw.WriteLine();
//                        }

//                    }
//                    for (i = 1; i < 29; i++)
//                    {
//                        if (y_j[i] != 0)
//                        {
//                            sw.WriteLine("韵苑" + i + "栋" + ":" + y_money[i] + "元,共" + y_room[i] + "个寝室");
//                            sw.WriteLine();
//                            byte[] byteArray = System.Text.Encoding.Default.GetBytes("韵苑" + i + "栋," + y_money[i] + "," + y_room[i] + "\n");
//                            sw_csv.Write(byteArray, 0, byteArray.Length);

//                            sheet.Cells[row, 1] = "韵苑" + i + "栋";
//                            sheet.Cells[row, 2] = y_money[i].ToString();
//                            sheet.Cells[row, 3] = y_room[i].ToString();
//                            row++;

//                            for (int j = 0; j < y_j[i]; j++)
//                            {
//                                sw.WriteLine(yunyuan[i, j]);
//                                sw.WriteLine();
//                            }
//                            sw.WriteLine();
//                            sw.WriteLine();
//                            sw.WriteLine();
//                            //sw.WriteLine();
//                        }

//                    }

//                    sw.Close();
//                    sw_csv.Close();

//                    sheet.Cells[row, 1] = "总金额";
//                    sheet.Cells[row, 2] = head.Substring(head.IndexOf("：") + 1, head.IndexOf("元") - head.IndexOf("：") - 1);

//                    if (File.Exists(Application.StartupPath + "\\文件数据\\" + "xiaojie_vip.xlsx"))
//                    {
//                        Missing missing = Missing.Value;
//                        workBook.Save();
//                        workBook.Close(missing, missing, missing);
//                        excel.Quit();
//                    }
//                    else
//                    {
//                        Missing missing = Missing.Value;
//                        sheet.SaveAs(Application.StartupPath + "\\文件数据\\" + "xiaojie_vip", missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlShared, missing, missing, missing);
//                        workBook.Close(missing, missing, missing);
//                        excel.Quit();
//                    }

//                    //open the file with default process
//                    System.Diagnostics.Process.Start(dayAddress + "\\" + "今日打印情况_vip_按楼栋排列.txt");
//                }
//            }
//            else
//                MessageBox.Show("没有今日打印情况_vip！");
//            */
//#endregion

//#region vip user name
//            string[] name = new string[100];
//            int[] massage_num = new int[100];
//            string[,] message = new string[100,100];
//            double[] document_money = new double[100];
//            int[] num = new int[100];
//            //open the file
//            if (File.Exists(dayAddress + "\\" + "今日打印情况_vip.txt"))
//            {
//                sr = File.OpenText(dayAddress + "\\" + "今日打印情况_vip.txt");
//                //ignore two lines
//                head = sr.ReadLine();
//                temp = sr.ReadLine();
//                if (temp != null)
//                    error_line = 2;

//                //read the record and allocate to the array
//                while ((reader = sr.ReadLine()) != null)
//                {
//                    error_line++;
//                    temp = reader;
//                    if (temp.IndexOf("单面") >= 0)
//                    {
//                        single_mode += Convert.ToDouble(temp.Substring(temp.LastIndexOf("=") + 2));
//                    }
//                    else if (temp.IndexOf("双面") >= 0)
//                    {
//                        double_mode += Convert.ToDouble(temp.Substring(temp.LastIndexOf("=") + 2));
//                    }
//                    string temp_2 = temp.Substring(temp.IndexOf(" ") + 1);
//                    string user_name = temp_2.Substring(0, temp_2.IndexOf(","));
//                    for (int index = 0; index < 100; index++)
//                    {
//                        if(name[index]==null)
//                        {
//                            message[index,massage_num[index]]=temp;
//                            massage_num[index]++;
//                            name[index] = user_name;
//                            int temp_money = temp.LastIndexOf("= ");
//                            temp = temp.Substring(temp_money + 2);
//                            document_money[index] += Convert.ToDouble(temp);
//                            break;
//                        }
//                        else if (name[index] == user_name)
//                        {
//                            message[index,massage_num[index]]=temp;
//                            massage_num[index]++;
//                            int temp_money = temp.LastIndexOf("= ");
//                            temp = temp.Substring(temp_money + 2);
//                            document_money[index] += Convert.ToDouble(temp);
//                            break;
//                        }
//                    }
//                    //if (temp == "")
//                    //{
//                    //    ;
//                    //}
//                    //else
//                    //{
//                    //    MessageBox.Show("今日打印情况_vip文件中第" + error_line + "行有错误楼栋信息！");
//                    //    error_line = -1;
//                    //}
//                    //if (error_line == -1)
//                    //    break;
//                }
//                sr.Close();  //close the source file

//                //write to new file
//                if (error_line != -1)
//                {
//                    if (File.Exists(dayAddress + "\\" + "今日打印情况_vip_按楼栋排列.txt"))
//                    {
//                        File.Delete(dayAddress + "\\" + "今日打印情况_vip_按楼栋排列.txt");
//                    }
//                    if (File.Exists(dayAddress + "\\" + "打印小结_vip.csv"))
//                    {
//                        File.Delete(dayAddress + "\\" + "打印小结_vip.csv");
//                    }
//                    StreamWriter sw = File.CreateText(dayAddress + "\\" + "今日打印情况_vip_按楼栋排列.txt");
//                    FileStream sw_csv = File.Create(dayAddress + "\\" + "打印小结_vip.csv");
//                    sw.WriteLine(head);
//                    sw.WriteLine("单面总金额：" + single_mode);
//                    sw.WriteLine("双面总金额：" + double_mode);
//                    sw.WriteLine(" ");

//                    Excel.Application excel = new Excel.Application();
//                    excel.Visible = false;
//                    Excel.Workbooks workBooks = excel.Workbooks;
//                    Excel.Workbook workBook;
//                    Excel.Sheets sheet_old;
//                    Excel.Worksheet sheet;
//                    int row = 1;
//                    if (File.Exists(Application.StartupPath + "\\文件数据\\" + "xiaojie_vip.xlsx"))
//                    {
//                        workBook = workBooks.Open(Application.StartupPath + "\\文件数据\\" + "xiaojie_vip.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
//                        sheet_old = workBook.Sheets;
//                        sheet = (Excel.Worksheet)sheet_old.Add(sheet_old[1], Type.Missing, Type.Missing, Type.Missing);
//                        try
//                        {
//                            sheet.Name = DateTime.Now.ToLongDateString().ToString();
//                        }
//                        catch (System.Exception ex)
//                        {
//                            excel.DisplayAlerts = false;
//                            ((Excel.Worksheet)workBook.Worksheets[1]).Delete();
//                            try
//                            {
//                                ((Excel.Worksheet)workBook.Worksheets[1]).Delete();
//                            }
//                            catch (System.Exception ex_2)
//                            {
//                                ;
//                            }
//                            excel.DisplayAlerts = true;
//                            sheet = (Excel.Worksheet)sheet_old.Add(sheet_old[1], Type.Missing, Type.Missing, Type.Missing);
//                            try
//                            {
//                                sheet.Name = DateTime.Now.ToLongDateString().ToString();
//                            }
//                            catch (System.Exception ex_3)
//                            {
//                                ;
//                            }
                            
//                        }

//                    }
//                    else
//                    {
//                        object Nothing = System.Reflection.Missing.Value;
//                        workBook = excel.Workbooks.Add(Nothing);
//                        sheet = (Excel.Worksheet)workBook.Sheets[1];
//                        sheet.Name = DateTime.Now.ToLongDateString().ToString();
//                    }

//                    for (i = 0; i < 100; i++)
//                    {
//                        if (name[i] != null)
//                        {
//                            sw.WriteLine("部门：" + name[i] + ",共" + document_money[i] + "元");
//                            sw.WriteLine();
//                            byte[] byteArray = System.Text.Encoding.Default.GetBytes("部门：" + name[i] + "," + document_money[i] + "\n");
//                            sw_csv.Write(byteArray, 0, byteArray.Length);

//                            sheet.Cells[row, 1] = name[i];
//                            sheet.Cells[row, 2] = document_money[i];
//                            row++;

//                            for (int k = 0; k < 100; k++)
//                            {
//                                if (message[i,k] != null)
//                                {
//                                    sw.WriteLine(message[i, k]);
//                                    sw.WriteLine();
//                                }
//                            }

//                            sw.WriteLine();
//                            sw.WriteLine();
//                            sw.WriteLine();
//                        }
//                    }

//                    sw.Close();
//                    sw_csv.Close();

//                    sheet.Cells[row, 1] = "总金额";
//                    sheet.Cells[row, 2] = head.Substring(head.IndexOf("：") + 1, head.IndexOf("元") - head.IndexOf("：") - 1);

//                    if (File.Exists(Application.StartupPath + "\\文件数据\\" + "xiaojie_vip.xlsx"))
//                    {
//                        Missing missing = Missing.Value;
//                        workBook.Save();
//                        workBook.Close(missing, missing, missing);
//                        excel.Quit();
//                    }
//                    else
//                    {
//                        Missing missing = Missing.Value;
//                        sheet.SaveAs(Application.StartupPath + "\\文件数据\\" + "xiaojie_vip", missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlShared, missing, missing, missing);
//                        workBook.Close(missing, missing, missing);
//                        excel.Quit();
//                    }

//                    //open the file with default process
//                    System.Diagnostics.Process.Start(dayAddress + "\\" + "今日打印情况_vip_按楼栋排列.txt");
//                }
//            }
//            else
//                MessageBox.Show("没有今日打印情况_vip！");
//#endregion
            
        }

        private bool CountNomal()
        {
            try
            {
                if (!File.Exists(dayAddress + "\\everydayFile.db3"))
                    return false;

                if (File.Exists(dayAddress + "\\今日打印情况_按楼栋排列.txt"))
                    File.Delete(dayAddress + "\\今日打印情况_按楼栋排列.txt");
                StreamWriter sw = new StreamWriter(dayAddress + "\\今日打印情况_按楼栋排列.txt");

                SQLiteConnection connectionToDatabase = new SQLiteConnection("Data Source=" + dayAddress + "\\everydayFile.db3");
                connectionToDatabase.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.CommandText = "select sum(totalMoney) from printFiles";
                command.Connection = connectionToDatabase;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                DataTable AllMoney = new DataTable();
                adapter.Fill(AllMoney);
                connectionToDatabase.Close();
                sw.WriteLine("今日总金额：" + AllMoney.Rows[0][0].ToString() + "元");
                sw.WriteLine();

                connectionToDatabase = new SQLiteConnection("Data Source=" + dayAddress + "\\everydayFile.db3");
                connectionToDatabase.Open();
                command.CommandText = "select Address,UserInformation from printFiles group by Address,UserInformation order by Address,UserInformation";
                command.Connection = connectionToDatabase;
                SQLiteDataAdapter adapters = new SQLiteDataAdapter(command);
                DataTable userTable = new DataTable();
                adapters.Fill(userTable);
                for (int i = 0; i < userTable.Rows.Count; i++)
                {
                    SQLiteCommand commands = new SQLiteCommand();
                    commands.CommandText = "select * from printFiles where Address='" + userTable.Rows[i]["Address"].ToString() + "' and UserInformation='" + userTable.Rows[i]["UserInformation"].ToString() + "' order by OrderNumber";
                    commands.Connection = connectionToDatabase;
                    SQLiteDataReader readers = commands.ExecuteReader();
                    if (readers.HasRows)
                    {
                        sw.WriteLine(userTable.Rows[i]["Address"].ToString() + "  " + userTable.Rows[i]["UserInformation"].ToString() + ":");
                        while (readers.Read())
                        {
                            sw.WriteLine("订单" + readers.GetInt32(2) + "  " + readers.GetString(3) + "  " + readers.GetString(4) + "  共" + readers.GetDouble(6) + "元");
                            sw.WriteLine("{");
                            string temp = readers.GetString(5);
                            JObject JOtemp = (JObject)JsonConvert.DeserializeObject(readers.GetString(5));
                            JArray ja = (JArray)JOtemp["files"];
                            foreach (JToken jk in ja)
                            {
                                sw.WriteLine("\t" + jk["filename"]);
                            }
                            sw.WriteLine("}");
                        }
                        sw.WriteLine();
                    }
                }
                sw.Close();
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        private bool CountVIP()
        {
            try
            {
                if (!File.Exists(dayAddress + "\\everydayFile_VIP.db3"))
                    return false;

                if (File.Exists(dayAddress + "\\今日打印情况_按楼栋排列_VIP.txt"))
                    File.Delete(dayAddress + "\\今日打印情况_按楼栋排列_VIP.txt");
                StreamWriter sw = new StreamWriter(dayAddress + "\\今日打印情况_按楼栋排列_VIP.txt");

                SQLiteConnection connectionToDatabase = new SQLiteConnection("Data Source=" + dayAddress + "\\everydayFile_VIP.db3");
                connectionToDatabase.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.CommandText = "select sum(totalMoney) from printFiles";
                command.Connection = connectionToDatabase;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                DataTable AllMoney = new DataTable();
                adapter.Fill(AllMoney);
                connectionToDatabase.Close();
                sw.WriteLine("今日总金额：" + AllMoney.Rows[0][0].ToString() + "元");
                sw.WriteLine();

                connectionToDatabase = new SQLiteConnection("Data Source=" + dayAddress + "\\everydayFile_VIP.db3");
                connectionToDatabase.Open();
                command.CommandText = "select Address,UserInformation from printFiles group by Address,UserInformation order by Address,UserInformation";
                command.Connection = connectionToDatabase;
                SQLiteDataAdapter adapters = new SQLiteDataAdapter(command);
                DataTable userTable = new DataTable();
                adapters.Fill(userTable);
                for (int i = 0; i < userTable.Rows.Count; i++)
                {
                    SQLiteCommand commands = new SQLiteCommand();
                    commands.CommandText = "select * from printFiles where Address='" + userTable.Rows[i]["Address"].ToString() + "' and UserInformation='" + userTable.Rows[i]["UserInformation"].ToString() + "' order by OrderNumber";
                    commands.Connection = connectionToDatabase;
                    SQLiteDataReader readers = commands.ExecuteReader();
                    if (readers.HasRows)
                    {
                        sw.WriteLine(userTable.Rows[i]["Address"].ToString() + "  " + userTable.Rows[i]["UserInformation"].ToString() + ":");
                        while (readers.Read())
                        {
                            sw.WriteLine("订单" + readers.GetInt32(2) + "  " + readers.GetString(3) + "  " + readers.GetString(4) + "  共" + readers.GetDouble(7) + "元");
                            sw.WriteLine("{");
                            //string temp = readers.GetString(6);
                            JObject JOtemp = (JObject)JsonConvert.DeserializeObject(readers.GetString(6));
                            JArray ja = (JArray)JOtemp["files"];
                            foreach (JToken jk in ja)
                            {
                                sw.WriteLine("\t" + jk["filename"]);
                            }
                            sw.WriteLine("}");
                        }
                        sw.WriteLine();
                    }
                }
                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //refrash();
            //MessageBox.Show("caca");
            //timer1.Enabled = false;
            //timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //refrash();
            //MessageBox.Show("dada");
            //timer1.Enabled = true;
            //timer2.Enabled = false;
        }

        private static string GetMD5Hash(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input));
            //char[] temp = new char[res.Length];
            //System.Array.Copy(res, temp, res.Length);
            //return new String(temp);
            return BitConverter.ToString(res).Replace("-", "");  
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //File.Delete(Application.StartupPath + "\\文件数据\\money");
            //StreamWriter srw = new StreamWriter(Application.StartupPath + "\\文件数据\\money");
            //srw.Write(label6.Text.Remove(label6.Text.IndexOf("元")));
            //srw.Close();

            //File.Delete(Application.StartupPath + "\\文件数据\\money3");
            //StreamWriter srw_single = new StreamWriter(Application.StartupPath + "\\文件数据\\money3");
            //srw_single.WriteLine(label11.Text.Remove(label11.Text.IndexOf("元")));
            //srw_single.WriteLine(label12.Text.Remove(label12.Text.IndexOf("元")));
            //srw_single.Close();

            //File.Delete(Application.StartupPath + "\\文件数据\\money4");
            //StreamWriter srw_vip = new StreamWriter(Application.StartupPath + "\\文件数据\\money4");
            //srw_vip.WriteLine(label18.Text.Remove(label18.Text.IndexOf("元")));
            //srw_vip.WriteLine(label15.Text.Remove(label15.Text.IndexOf("元")));
            //srw_vip.WriteLine(label16.Text.Remove(label16.Text.IndexOf("元")));
            //srw_vip.Close();

            //try
            //{
            //    if (Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\temp"))
            //        Directory.Delete(System.Windows.Forms.Application.StartupPath + "\\temp", true);
            //}
            //catch
            //{
            //    ;
            //}
        }

        private void SetOrderNumber(int number, string classLevel, string names)
        {
            System.Windows.Forms.Control[] temp = null;
            if (classLevel == "7000")
                temp = this.panel1.Controls.Find(names, true);
            else
                temp = this.plDownList.Controls.Find(names, true);
            for (int i = 0; i < temp.Length; i++)
            {
                ((print_message)temp[i]).SetTotalOrderNumber(number);
            }
        }

        #region
        /*
        private static bool IsCanConnect(string url)
        {
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            bool CanCn = true;   //设成可以连接； 
            try
            {
                req = (HttpWebRequest)WebRequest.Create(url);
                res = (HttpWebResponse)req.GetResponse();
            }
            catch (Exception e)
            {
                CanCn = false;   //无法连接
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (res != null)
                {
                    res.Close();
                }
            }
            return CanCn;
        }

       
        public void getPage(String url)
        {
            WebResponse result = null;

            try
            {
                WebRequest req = WebRequest.Create(url);
                result = req.GetResponse();
                Stream ReceiveStream = result.GetResponseStream();

                //read the stream into a string
                StreamReader sr = new StreamReader(ReceiveStream);
                string resultstring = sr.ReadToEnd();

                //Console.WriteLine("\r\nResponse stream received");
                //Console.WriteLine(resultstring);
                MessageBox.Show("Response stream received");
                MessageBox.Show(resultstring);
            }
            catch (Exception exp)
            {
                //Console.Write("\r\nRequest failed. Reason:");
                //Console.WriteLine(exp.Message);
                MessageBox.Show("Request failed");
                MessageBox.Show(exp.ToString());
            }
            finally
            {
                if (result != null)
                {
                    result.Close();
                }
            }

            Console.WriteLine("\r\nPress Enter to exit.");
            Console.ReadLine();
        }

        public void getServer(string ip,string ipport)
        {
            //
            // TODO: 在此处添加代码以启动应用程序
            //
            byte[] data = new byte[1024000];
            Socket newclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Console.Write("please input the server ip:");
            string ipadd = ip;
            //Console.WriteLine();
            //Console.Write("please input the server port:");
            int port = Convert.ToInt32(ipport);
            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ipadd), port);//服务器的IP和端口
            try
            {
                //因为客户端只是用来向特定的服务器发送信息，所以不需要绑定本机的IP和端口。不需要监听。
                newclient.Connect(ie);
            }
            catch (SocketException e)
            {
                Console.WriteLine("unable to connect to server");
                Console.WriteLine(e.ToString());
                return;
            }
            int recv = newclient.Receive(data);
            string stringdata = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(stringdata);
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "exit")
                    break;
                newclient.Send(Encoding.ASCII.GetBytes(input));
                data = new byte[1024];
                recv = newclient.Receive(data);
                stringdata = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringdata);
            }
            Console.WriteLine("disconnect from sercer");
            newclient.Shutdown(SocketShutdown.Both);
            newclient.Close();

        }

        public void Post()
        {
            //string strResult = "";

            //try
            //{
            //    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3256/NetCC_Agent.ashx?op=user_getuserlist&callback=ncc_getuser_callback&useraccount=czc@yuantel.com");

            //    myRequest.Method = "POST";

            //    myRequest.ContentType = "text/html;charset=utf-8";

            //    try
            //    {
            //        HttpWebResponse HttpWResp = (HttpWebResponse)myRequest.GetResponse();

            //        Stream myStream = HttpWResp.GetResponseStream();
            //        StreamReader sr = new StreamReader(myStream, Encoding.UTF8);
            //        StringBuilder strBuilder = new StringBuilder();
            //        while (-1 != sr.Peek())
            //        {
            //            strBuilder.Append(sr.ReadLine());
            //        }

            //        strResult = strBuilder.ToString();

            //    }
            //    catch (Exception exp)
            //    {

            //        strResult = "错误：" + exp.Message;
            //    }


            //}
            //catch (Exception exp)
            //{

            //    strResult = "错误：" + exp.Message;

            //}

            //MessageBox.Show(strResult);

            string param = "hl=zh-CN&newwindow=1";
            byte[] bs = Encoding.ASCII.GetBytes(param);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://www.google.com/intl/zh-CN/");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = bs.Length;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                //Stream myStream = req.GetResponseStream();
                //StreamReader sr = new StreamReader(myStream, Encoding.UTF8);
                MessageBox.Show(Convert.ToString(bs));
            }
            using (WebResponse wr = req.GetResponse())
            {
                //在这里对接收到的页面内容进行处理
                Stream myStream = wr.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.UTF8);
                MessageBox.Show(sr.ReadToEnd());
            } 
        }
         */
#endregion

        public double AllMoney(string orderId, string building, string names, string classLevel)
        {
            double moneyCount = -100.0;
            double tempMoney=0.0;
            System.Windows.Forms.Control[] temp;
            if (classLevel == "7000")
                temp = this.panel1.Controls.Find(orderId + building + names, true);
            else
                temp = this.plDownList.Controls.Find(orderId + building + names, true);
            if (temp.Length > 0)
                moneyCount = 0.0;
            for (int i = 0; i < temp.Length; i++)
            {
                tempMoney = ((ListboxItem)temp[i]).GetMoney();
                if (tempMoney == -100.0)
                {
                    DialogResult result = MessageBox.Show("还有文件未处理或未填写金额，是否继续？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                        return -100.0;
                    else
                        tempMoney = 0.0;
                }
                moneyCount += tempMoney;
            }
            return moneyCount;
        }

        public string GetList(string parameter1,string Values1,string parameter2,string Values2)
        {
            Encoding myEncoding = Encoding.GetEncoding("gb2312");
            string address = "http://www.xiaoyintong.com/school_printer/Login" + HttpUtility.UrlEncode(parameter1) + "=" + HttpUtility.UrlEncode(Values1) + "&" + HttpUtility.UrlEncode(parameter2) + "=" + HttpUtility.UrlEncode(Values2);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(address);
            req.Method = "GET";
            WebResponse wr = req.GetResponse();
            Stream myStream = wr.GetResponseStream();
            StreamReader sr = new StreamReader(myStream, Encoding.UTF8);
            ////using (WebResponse wr = req.GetResponse())
            //{
            //    //在这里对接收到的页面内容进行处理
            //    MessageBox.Show(sr.ReadToEnd());
            //} 
            return sr.ReadToEnd();
        }

        public void Update(string parameter1, string Values1, string parameter2, string Values2)
        {
            Encoding myEncoding = Encoding.GetEncoding("gb2312");
            string address = "http://www.baidu.com/?" + HttpUtility.UrlEncode(parameter1) + "=" + HttpUtility.UrlEncode(Values1) + "&" + HttpUtility.UrlEncode(parameter2) + "=" + HttpUtility.UrlEncode(Values2);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(address);
            req.Method = "GET";
            WebResponse wr = req.GetResponse();
            Stream myStream = wr.GetResponseStream();
            StreamReader sr = new StreamReader(myStream, Encoding.UTF8);
            ////using (WebResponse wr = req.GetResponse())
            //{
            //    //在这里对接收到的页面内容进行处理
            //    MessageBox.Show(sr.ReadToEnd());
            //} 
            //return sr.ReadToEnd();
        }

        public void ChangeOrderMoney(double lastMoney, double nowMoney, string orderId, string classLevel)
        {
            System.Windows.Forms.Control[] temp = null;
            if (classLevel == "7000")
                temp = this.panel1.Controls.Find(orderId, true);
            else
                temp = this.plDownList.Controls.Find(orderId, true);
            for (int i = 0; i < temp.Length; i++)
            {
                if (!((Order)temp[i]).ChangeMoney(lastMoney, nowMoney))
                {
                    MessageBox.Show("金额计算出问题了！");
                }
            }
        }

        public bool CheckOrder(string orderId, string classLevel)
        {
            System.Windows.Forms.Control[] temp = null;
            if (classLevel == "7000")
                temp = this.panel1.Controls.Find(orderId, true);
            else
                temp = this.plDownList.Controls.Find(orderId, true);

            return ((Order)temp[0]).CheckOrder(classLevel);
        }

        public void DeleteOrder(string orderId, string classLevel, string address, string names)
        {
            System.Windows.Forms.Control[] temp = null;
            if (classLevel == "7000")
            {
                temp = this.panel1.Controls.Find(orderId, true);
                for (int i = 0; i < temp.Length; i++)
                {
                    this.panel1.Controls.Remove(temp[i]);
                }

                temp = this.panel1.Controls.Find(orderId + address + names, true);
                for (int i = 0; i < temp.Length; i++)
                {
                    this.panel1.Controls.Remove(temp[i]);
                }

                temp = this.panel1.Controls.Find(address + names, true);
                for (int i = 0; i < temp.Length; i++)
                {
                    ((print_message)temp[i]).DeleteOrderNumber();
                    if (((print_message)temp[i]).CheckOrderRemaind() == 0)
                        this.panel1.Controls.Remove(temp[i]);
                }
            }
            else
            {
                temp = this.plDownList.Controls.Find(orderId, true);
                for (int i = 0; i < temp.Length; i++)
                {
                    this.plDownList.Controls.Remove(temp[i]);
                }

                temp = this.plDownList.Controls.Find(orderId + address + names, true);
                for (int i = 0; i < temp.Length; i++)
                {
                    this.plDownList.Controls.Remove(temp[i]);
                }

                temp = this.plDownList.Controls.Find(address + names, true);
                for (int i = 0; i < temp.Length; i++)
                {
                    ((print_message)temp[i]).DeleteOrderNumber();
                    if (((print_message)temp[i]).CheckOrderRemaind() == 0)
                        this.plDownList.Controls.Remove(temp[i]);  
                }
            }
            
        }

        public void SetHandledOrderNumber(string classLevel, string address, string names)
        {
            System.Windows.Forms.Control[] temp = null;
            if (classLevel == "7000")
                temp = this.panel1.Controls.Find(address + names, true);
            else
                temp = this.plDownList.Controls.Find(address + names, true);

            ((print_message)temp[0]).SetHandleOrderNumber();
        }

        /// <summary>

        /// Post data到url

        /// </summary>

        /// <param name="data">要post的数据</param>

        /// <param name="url">目标url</param>

        /// <returns>服务器响应</returns>

        public string PostDataToUrl(string data, string url)

        {

            Encoding encoding = Encoding.GetEncoding(sRequestEncoding);

            byte[] bytesToPost = encoding.GetBytes(data);

            return PostDataToUrl(bytesToPost, url);

        }

        Cookie testCookie = null;

        /// <summary>

        /// Post data到url

        /// </summary>

        /// <param name="data">要post的数据</param>

        /// <param name="url">目标url</param>

        /// <returns>服务器响应</returns>

        public string PostDataToUrl(byte[] data, string url)

        {

            #region 创建httpWebRequest对象

            WebRequest webRequest = WebRequest.Create(url);

            HttpWebRequest httpRequest = webRequest as HttpWebRequest;

            if (httpRequest == null)

            {

                throw new ApplicationException(

                    string.Format("Invalid url string: {0}", url)

                    );

            }

            #endregion

 

            #region 填充httpWebRequest的基本信息

            httpRequest.UserAgent = sUserAgent;

            httpRequest.ContentType = sContentType;

            httpRequest.Method = "POST";

            if (testCookie == null)
                httpRequest.CookieContainer = new CookieContainer();
            else
            {
                httpRequest.CookieContainer = new CookieContainer();
                httpRequest.CookieContainer.Add(testCookie);   //add cookie to the request
            }

            #endregion

 

            #region 填充要post的内容

            httpRequest.ContentLength = data.Length;

            Stream requestStream = httpRequest.GetRequestStream();

            requestStream.Write(data, 0, data.Length);

            requestStream.Close();

            #endregion

 

            #region 发送post请求到服务器并读取服务器返回信息

            Stream responseStream;
            HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();

            try
            {

                responseStream = httpRequest.GetResponse().GetResponseStream();
                if(testCookie == null)
                    testCookie = response.Cookies[0];   //get cookie from the server

            }

            catch(Exception e)
            {

                // log error

                Console.WriteLine(

                    string.Format("POST操作发生异常：{0}", e.Message)

                    );

                throw e;

            }

            #endregion

 

            #region 读取服务器返回信息

            string stringResponse = string.Empty;

            using(StreamReader responseReader = new StreamReader(responseStream, Encoding.GetEncoding(sResponseEncoding)))
            //using(StreamReader responseReader = new StreamReader(responseStream, Encoding.Default))

            {

                stringResponse = responseReader.ReadToEnd();
                

            }

            responseStream.Close();

            #endregion

            return stringResponse;

        }

 

        const string sUserAgent =

            "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        const string sContentType =

            "application/x-www-form-urlencoded";

        const string sRequestEncoding = "ascii";

        const string sResponseEncoding = "utf-8";

        private void button6_Click(object sender, EventArgs e)
        {
            //sure to resive the paper
            //DialogResult dialogResult = MessageBox.Show("是->取走中午的打印稿\n否->取走晚上的打印稿", "make sure", MessageBoxButtons.YesNoCancel);
            //if (dialogResult == DialogResult.Yes)
            //{
            //    recive_morning();
            //}
            //else if (dialogResult == DialogResult.No)
            //{
            //    recive_evening();
            //}
            recive_message recive_message_window = new recive_message(this,uid);
            recive_message_window.Show();
        }

        private void recive_morning()
        {
            StreamReader sr_recive = new StreamReader(Application.StartupPath + "\\文件数据\\no_recive_mornng");
            string send_message = "{\"task\":[";
            string temp = "";
            int is_first = 0;
            for (; (temp = sr_recive.ReadLine()) != null; )
            {
                if (is_first == 0)
                {
                    send_message += "\"" + temp + "\"";
                    is_first = 1;
                }
                else
                    send_message += "," + "\"" + temp + "\"";
            }
            sr_recive.Close();
            send_message += "]}";
            MessageBox.Show(send_message);
            //send to the server
            string Text = PostDataToUrl("uid" + "=" + uid + "&" + "info" + "=" + send_message, "http://www.xiaoyintong.com/v3_school_printer/commit_pickup");
            MessageBox.Show(Text);
            File.Delete(Application.StartupPath + "\\文件数据\\no_recive_moring");
        }

        private void recive_evening()
        {
            StreamReader sr_recive = new StreamReader(Application.StartupPath + "\\文件数据\\no_recive_evening");
            string send_message = "{\"task\":[";
            string temp = "";
            int is_first = 0;
            for (; (temp = sr_recive.ReadLine()) != null; )
            {
                if (is_first == 0)
                {
                    send_message += "\"" + temp + "\"";
                    is_first = 1;
                }
                else
                    send_message += "," + "\"" + temp + "\"";
            }
            sr_recive.Close();
            send_message += "]}";
            MessageBox.Show(send_message);
            //send to the server
            string Text = PostDataToUrl("uid" + "=" + uid + "&" + "info" + "=" + send_message, "http://www.xiaoyintong.com/v3_school_printer/commit_pickup");
            MessageBox.Show(Text);
            File.Delete(Application.StartupPath + "\\文件数据\\no_recive_evening");
        }
    }
}