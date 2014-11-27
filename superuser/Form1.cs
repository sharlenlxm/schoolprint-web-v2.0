using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SchoolPrintDomin;
using System.Net;

namespace superuser
{
    public partial class mainwin : Form
    {

        static WebReference.DALWebService service = new WebReference.DALWebService();
        static DominInfo dominInfo = new DominInfo();

        //static DALService.DALWebService service = new DALService.DALWebService();
        private DataSet dataSet;
        private double[] yy = new double[29];
        private double[] yy2 = new double[29];
        private double[] zs = new double[14];
        private double[] zs2 = new double[14];
        private double[] qy = new double[14];
        private double[] qy2 = new double[14];//这是各个楼栋的名字
        private double huaguang, zisong, xiaoyintong, yuner;//这是各个打印店的名字
        private string Building;

        private string[,] outfile=new string[3,29];


        private string[] CustomerId;
        private string[] DocumentUrl;
        private long[] ID;
        private string[] DocumentName;
        private string[] PaperType;
        private string[] sendWay;
        private string[] State;
        private string[] address;
        private string[] ShoperId;
        private string[] Copies;
        private string[] PrintMode;
        private string[] Remark;
        private string[] Cost;
        private string[] Phone;
        private string[] Names;

        int count;


        public mainwin()
        {

            InitializeComponent();
            try
            {
                service.Credentials = new NetworkCredential(dominInfo.UserName, dominInfo.Password, dominInfo.Domin);


                string check = DateTime.Now.ToLongDateString().ToString();
                check = check.Replace(" ", "");
                MessageBox.Show(check);

                WebReference.Document[] result2 = service.SuperCheck(check);
                //int count = table.Rows.Count;
                count = result2.Length;
                //count = 2;
                CustomerId = new string[count];
                DocumentUrl = new string[count];
                ID = new long[count];
                DocumentName = new string[count];
                PaperType = new string[count];
                sendWay = new string[count];
                State = new string[count];
                address = new string[count];
                ShoperId = new string[count];
                Copies = new string[count];
                PrintMode = new string[count];
                Remark = new string[count];
                Cost = new string[count];
                Names = new string[count];
                Phone = new string[count];

                //string[] DocumentNum = new string[count];
                //string[] SubmitTime = new string[count];
                int k = 0;
                foreach (WebReference.Document row in result2)
                {
                    //CustomerId[k] = row["CustomerId"].ToString();
                    CustomerId[k] = row.CustomerId;
                    DocumentUrl[k] = row.DocumentUrl;
                    ID[k] = row.ID;
                    DocumentName[k] = row.DocumentName;
                    PaperType[k] = row.PaperType;
                    sendWay[k] = row.sendWay;
                    State[k] = row.State;
                    address[k] = row.address;
                    ShoperId[k] = row.ShoperId;
                    Copies[k] = row.Copies;
                    PrintMode[k] = row.PrintMode;
                    Remark[k] = row.Remark;
                    Cost[k] = row.Cost;
                    Names[k] = row.Name;
                    Phone[k] = row.Phone;
                    //DocumentNum[i] = row["DocumentNum"].ToString();
                    //SubmitTime[i] = row["SubmitTime"].ToString();
                    k++;
                }
                //dataSet = service.SuperCheck(check);
                //DataTable table = dataSet.Tables[0];
                //count = table.Rows.Count;
                //CustomerId = new string[count];
                //DocumentUrl = new string[count];
                //ID = new int[count];
                //DocumentName = new string[count];
                //PaperType = new string[count];
                //sendWay = new string[count];
                //State = new string[count];
                //address = new string[count];
                //ShoperId = new string[count];
                //Copies = new string[count];
                //PrintMode = new string[count];
                //Remark = new string[count];
                //Cost = new string[count];
                ////string[] DocumentNum = new string[count];
                ////string[] SubmitTime = new string[count];
                //int k = 0;
                //foreach (DataRow row in table.Rows)
                //{
                //    CustomerId[k] = row["CustomerId"].ToString();
                //    DocumentUrl[k] = row["DocumentUrl"].ToString();
                //    ID[k] = Convert.ToInt32(row["ID"].ToString());
                //    DocumentName[k] = row["DocumentName"].ToString();
                //    PaperType[k] = row["PaperType"].ToString();
                //    sendWay[k] = row["sendWay"].ToString();
                //    State[k] = row["State"].ToString();
                //    address[k] = row["address"].ToString();
                //    ShoperId[k] = row["ShoperId"].ToString();
                //    Copies[k] = row["Copies"].ToString();
                //    PrintMode[k] = row["PrintMode"].ToString();
                //    Remark[k] = row["Remark"].ToString();
                //    Cost[k] = row["Cost"].ToString();
                //    //DocumentNum[i] = row["DocumentNum"].ToString();
                //    //SubmitTime[i] = row["SubmitTime"].ToString();
                //    k++;
                //}


                for (int i = 0; i < count; i++)//更新数据
                {
                    richTextBox4.Text += address[i] + "  " + Names[i] + "  " + Phone[i] + "  " + DocumentName[i] + " = " + Cost[i] + "元" + "\n" + "\n";


                    string tag;
                    Building = address[i];
                    //Building = "韵苑1栋111室";
                    int place = Building.IndexOf("栋");
                    Building = Building.Remove(place);
                    string loudong = Building.Remove(2);
                    Building = Building.Substring(2);
                    int num = Convert.ToInt32(Building);
                    switch (loudong)//按楼栋进行总计
                    {
                        case "韵苑":
                            {
                                yy[num] += Convert.ToDouble(Cost[i]);
                                outfile[0,num] += address[i] + "  " + Names[i] + "  " + Phone[i] + "  " + DocumentName[i] + " = " + Cost[i] + "元" + "\n" + "\n";
                                tag = "韵苑";
                            }
                            break;
                        case "沁苑":
                            {
                                qy[num] += Convert.ToDouble(Cost[i]);
                                outfile[1,num] += address[i] + "  " + Names[i] + "  " + Phone[i] + "  " + DocumentName[i] + " = " + Cost[i] + "元" + "\n" + "\n";
                                tag = "沁苑";
                            }
                            break;
                        case "紫菘":
                            {
                                zs[num] += Convert.ToDouble(Cost[i]);
                                outfile[2,num] += address[i] + "  " + Names[i] + "  " + Phone[i] + "  " + DocumentName[i] + " = " + Cost[i] + "元" + "\n" + "\n";
                                tag = "紫菘";
                            }
                            break;
                        default:
                            MessageBox.Show(address[i]);
                            break;
                    }
                    switch (ShoperId[i])//按打印店进行总计
                    {
                        case "huaguangpan@126.com":
                            huaguang += Convert.ToInt32(Cost[i]);
                            break;
                        case "87547422":
                            zisong += Convert.ToInt32(Cost[i]);
                            break;
                        case "540256567@qq.com":
                            xiaoyintong += Convert.ToDouble(Cost[i]);
                            break;
                        case "987654321":
                            yuner += Convert.ToDouble(Cost[i]);
                            break;
                        default:
                            MessageBox.Show(Cost[i]);
                            break;

                    }
                    //richTextBox3.Text += tag + num.ToString()+"栋"+" = " + print.ToString() + "\n";
                    //richTextBox1.Text += "yy11" + " = " + yy11.ToString() + "\n";
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            for (int i = 1; i < 29; i++)
            {
                richTextBox1.Text += "韵苑" + i.ToString() + "栋" + " = " + yy[i].ToString() + "元" + "\n";
            }
            richTextBox1.Text += "\n";
            for (int i = 9; i < 14; i++)
            {
                richTextBox1.Text += "沁苑" + i.ToString() + "栋" + " = " + qy[i].ToString() + "元" + "\n";
            }
            richTextBox1.Text += "\n";
            for (int i = 1; i < 14; i++)
            {
                richTextBox1.Text += "紫菘" + i.ToString() + "栋" + " = " + zs[i].ToString() + "元" + "\n";
            }
            //richTextBox1.Text += "\n";

            richTextBox3.Text += "华光电脑服务部" + " = " + huaguang.ToString() + "元" + "\n"+"\n";
            richTextBox3.Text += "紫菘印务" + " = " + zisong.ToString() + "元" + "\n"+"\n";
            richTextBox3.Text += "校印通" + " = " + xiaoyintong.ToString() + "元" + "\n"+"\n";
            richTextBox3.Text += "韵二打印店" + " = " + yuner.ToString() + "元" + "\n"+"\n";
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("输入楼栋号呀！╭∩╮（︶︿︶）╭∩╮鄙视你！");
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (ShoperId[i] == "540256567@qq.com")
                    {
                        //Building = address[i];
                        //Building = Building.Remove(4);
                        //switch (string.Compare(Building, textBox1.Text))//楼栋详细信息
                        //{
                        //    case 0:
                        //        //richTextBox2.Text += address[i] + "  " + Names[i] + "  " + DocumentName[i] + " = " + Cost[i] + "\n";
                        //        richTextBox2.Text += address[i] + "  " + Names[i] + "  " + Phone[i] + "  " + DocumentName[i] + " = " + Cost[i] + "\n";
                        //        break;
                        //    default:
                        //        MessageBox.Show(Building);
                        //        break;
                        //}
                        Building = address[i];
                        //Building = "韵苑1栋111室";
                        int place = Building.IndexOf("栋");
                        Building = Building.Remove(place);
                        string loudong = Building.Remove(2);
                        Building = Building.Substring(2);
                        int num = Convert.ToInt32(Building);
                        Building = Building.Trim();
                        if (韵苑.Checked)
                        {
                            if (string.Compare(Building, textBox1.Text)==0)
                            {
                                yy2[num] += Convert.ToDouble(Cost[i]);
                                richTextBox2.Text += address[i] + "  " + Names[i] + "  " + Phone[i] + "  " + DocumentName[i] + " = " + Cost[i] + "元" + "\n" + "\n";
                            }
                        }
                        else if (沁苑.Checked)
                        {
                            if (string.Compare(Building, textBox1.Text) == 0)
                            {
                                qy2[num] += Convert.ToDouble(Cost[i]);
                                richTextBox2.Text += address[i] + "  " + Names[i] + "  " + Phone[i] + "  " + DocumentName[i] + " = " + Cost[i] + "元" + "\n" + "\n";
                            }
                        }
                        else if (紫菘.Checked)
                        {
                            if (string.Compare(Building, textBox1.Text) == 0)
                            {
                                zs2[num] += Convert.ToDouble(Cost[i]);
                                richTextBox2.Text += address[i] + "  " + Names[i] + "  " + Phone[i] + "  " + DocumentName[i] + " = " + Cost[i] + "元" + "\n" + "\n";
                            }
                        }
                    }
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)

        {
            for (int i = 0; i < count; i++)//所有打印店更新数据
            {
                //string tag;
                Building = address[i];
                //Building = "韵苑1栋111室";
                int place = Building.IndexOf("栋");
                Building = Building.Remove(place);
                string loudong = Building.Remove(2);
                Building = Building.Substring(2);
                int num = Convert.ToInt32(Building);
                switch (loudong)//按楼栋进行总计
                {
                    case "韵苑":
                        {
                            yy[num] += Convert.ToDouble(Cost[i]);
                            //tag = "韵苑";
                        }
                        break;
                    case "沁苑":
                        {
                            qy[num] += Convert.ToDouble(Cost[i]);
                            //tag = "沁苑";
                        }
                        break;
                    case "紫菘":
                        {
                            zs[num] += Convert.ToDouble(Cost[i]);
                            //tag = "紫菘";
                        }
                        break;
                    default:
                        MessageBox.Show(address[i]);
                        break;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < count; i++)//所有打印店更新数据
            {
                if (ShoperId[i] == "540256576@qq.com")
                {
                    //string tag;
                    Building = address[i];
                    //Building = "韵苑1栋111室";
                    int place = Building.IndexOf("栋");
                    Building = Building.Remove(place);
                    string loudong = Building.Remove(2);
                    Building = Building.Substring(2);
                    int num = Convert.ToInt32(Building);
                    switch (loudong)//按楼栋进行总计
                    {
                        case "韵苑":
                            {
                                yy[num] += Convert.ToDouble(Cost[i]);
                                //tag = "韵苑";
                            }
                            break;
                        case "沁苑":
                            {
                                qy[num] += Convert.ToDouble(Cost[i]);
                                //tag = "沁苑";
                            }
                            break;
                        case "紫菘":
                            {
                                zs[num] += Convert.ToDouble(Cost[i]);
                                //tag = "紫菘";
                            }
                            break;
                        default:
                            MessageBox.Show(address[i]);
                            break;
                    }
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Address = Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToLongDateString().ToString() + "\\";
            Directory.CreateDirectory(Address);
            for (int i = 0; i < 29; i++)
            {
                if (outfile[0,i] != null)
                {
                    //StreamWriter sw=File.Create(Address+"")
                    StreamWriter swrw = new StreamWriter(Address + "韵苑" + i.ToString() + "栋.txt", false);
                    swrw.Write(outfile[0,i]);
                    swrw.Close();
                }
            }

            for (int i = 0; i < 29; i++)
            {
                if (outfile[1,i] != null)
                {
                    //StreamWriter sw=File.Create(Address+"")
                    StreamWriter swrw = new StreamWriter(Address + "沁苑" + i.ToString() + "栋.txt", false);
                    swrw.Write(outfile[1,i]);
                    swrw.Close();
                }
            }

            for (int i = 0; i < 29; i++)
            {
                if (outfile[2,i] != null)
                {
                    //StreamWriter sw=File.Create(Address+"")
                    StreamWriter swrw = new StreamWriter(Address + "紫菘" + i.ToString() + "栋.txt", false);
                    swrw.Write(outfile[2,i]);
                    swrw.Close();
                }
            }

            MessageBox.Show("ok!");

            //if (richTextBox2.Text != null)
            //{
            //    if (韵苑.Checked)
            //    {
            //        string Address = Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToLongDateString().ToString() + "\\";
            //        //StreamWriter sw=File.Create(Address+"")
            //        Directory.CreateDirectory(Address);
            //        StreamWriter swrw = new StreamWriter(Address + "韵苑" + textBox1.Text + "栋.txt", false);
            //        swrw.Write(richTextBox2.Text);
            //        swrw.Close();
            //    }
            //    else if (沁苑.Checked)
            //    {
            //        string Address = Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToLongDateString().ToString() ;
            //        //StreamWriter sw=File.Create(Address+"")
            //        //string address = Address + "沁苑" + textBox1.Text + "栋.txt";
            //        Directory.CreateDirectory(Address);
            //        StreamWriter swrw = new StreamWriter(Address + "\\" + "沁苑" + textBox1.Text + "栋.txt", false);
            //        swrw.Write(richTextBox2.Text);
            //        swrw.Close();
            //    }
            //    else if (紫菘.Checked)
            //    {
            //        string Address = Application.StartupPath + "\\文件数据\\" + DateTime.Now.ToLongDateString().ToString() + "\\";
            //        //StreamWriter sw=File.Create(Address+"")
            //        Directory.CreateDirectory(Address);
            //        StreamWriter swrw = new StreamWriter(Address + "紫菘" + textBox1.Text + "栋.txt", false);
            //        swrw.Write(richTextBox2.Text);
            //        swrw.Close();
            //    }
            //    MessageBox.Show("ok!");
            //}
            
            
        }

    }

    
}
