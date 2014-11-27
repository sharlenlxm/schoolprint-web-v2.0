using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SchoolPrintDomin;
using System.Net;

namespace everydaycheck
{
    public partial class 每日信息汇总 : Form
    {
        static WebReference.DALWebService service = new WebReference.DALWebService();
        static DominInfo dominInfo = new DominInfo();

        private DataSet dataSet;
        private double[] yy = new double[29];
        private double[] yy2 = new double[29];
        private double[] zs = new double[14];
        private double[] zs2 = new double[14];
        private double[] qy = new double[14];
        private double[] qy2 = new double[14];//这是各个楼栋的名字
        private double huaguang, zisong, xiaoyintong, yuner;//这是各个打印店的名字
        private string Building;

        private string[, ,] outfile = new string[3, 29, 100];


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
        private DateTime[] SureTime;

        int count;
        int[] yycount=new int[100];
        int[] qycount=new int[100];
        int[] zscount=new int[100];

        Label[] labels = new Label[100];

        public 每日信息汇总()
        {
            InitializeComponent();
            service.Credentials = new NetworkCredential(dominInfo.UserName, dominInfo.Password, dominInfo.Domin);

            try
            {
                string check = DateTime.Now.ToLongDateString().ToString();
                check = check.Replace(" ", "");
                //MessageBox.Show(check);

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
                SureTime = new DateTime[count];

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
                    SureTime[k] = row.SureTime;
                    //MessageBox.Show((DateTime.Compare(SureTime[k], Convert.ToDateTime("2013年11月5日 星期二 12:00:00"))).ToString());
                    //DocumentNum[i] = row["DocumentNum"].ToString();
                    //SubmitTime[i] = row["SubmitTime"].ToString();
                    k++;
                }
               

                for (int i = 0; i < count; i++)//更新数据
                {

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
                                outfile[0, num,yycount[num]] += address[i] + "  " + Names[i] + "  " + Phone[i] + "  " + "." + DocumentName[i].Substring(DocumentName[i].Length - 7) + "  " + Copies[i] + "份" + "  " + PrintMode[i] + "  " + PaperType[i] + " = " + Cost[i] + "元" + "\r\n" + "\r\n";
                                yycount[num]++;
                                
                                tag = "韵苑";
                            }
                            break;
                        case "沁苑":
                            {
                                qy[num] += Convert.ToDouble(Cost[i]);
                                outfile[1, num,qycount[num]] += address[i] + "  " + Names[i] + "  " + Phone[i] + "  " + "." + DocumentName[i].Substring(DocumentName[i].Length - 7) + "  " + Copies[i] + "份" + "  " + PrintMode[i] + "  " + PaperType[i] + " = " + Cost[i] + "元" + "\r\n" + "\r\n";
                                qycount[num]++;
                                tag = "沁苑";
                            }
                            break;
                        case "紫菘":
                            {
                                zs[num] += Convert.ToDouble(Cost[i]);
                                outfile[2, num,zscount[num]] += address[i] + "  " + Names[i] + "  " + Phone[i] + "  " + "." + DocumentName[i].Substring(DocumentName[i].Length - 7) + "  " + Copies[i] + "份" + "  " + PrintMode[i] + "  " + PaperType[i] + " = " + Cost[i] + "元" + "\r\n" + "\r\n";
                                zscount[num]++;
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
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string Address = Application.StartupPath + "\\楼栋文件信息\\" + DateTime.Now.ToLongDateString().ToString() + "\\";
            Directory.CreateDirectory(Address);

            //for (int i = 0; i < 29; i++)
            //{
            //    List<string> yylines = new List<string>(File.ReadAllLines(Address + "韵苑" + i.ToString() + "栋.txt"));
            //    //File.Delete(Address + "韵苑" + i.ToString() + "栋.txt");
            //    yylines.RemoveAt(lines.Count - 1);//删除第3行
            //    File.WriteAllLines(Address + "韵苑" + i.ToString() + "栋.txt", yylines.ToArray());

            //}

                for (int i = 0; i < 29; i++)
                {
                    int tag = 0;
                    for (int n = 0; n < 100; n++)
                    {
                        if (outfile[0, i, n] != null)
                        {
                            //StreamWriter sw=File.Create(Address+"")
                            tag = 1;
                            string text;
                            try
                            {
                                List<string> lines = new List<string>(File.ReadAllLines(Address + "韵苑" + i.ToString() + "栋.txt"));
                                //File.Delete(Address + "韵苑" + i.ToString() + "栋.txt");
                                lines.RemoveAt(lines.Count - 1);//删除第3行
                                File.WriteAllLines(Address + "韵苑" + i.ToString() + "栋.txt", lines.ToArray());

                                StreamReader sr = File.OpenText(Address + "韵苑" + i.ToString() + "栋.txt");
                                text = sr.ReadToEnd();
                                sr.Close();
                                File.Delete(Address + "韵苑" + i.ToString() + "栋.txt");

                            }
                            catch
                            {
                                text = "";
                            }

                            StreamWriter swrw = File.CreateText(Address + "韵苑" + i.ToString() + "栋.txt");
                            if (text.IndexOf(outfile[0, i,n]) == -1)
                            {
                                text += outfile[0, i,n];
                            }
                            else
                            {
                                string no = text.Substring(text.IndexOf(outfile[0, i,n]), outfile[0, i,n].Length);
                                text = text.Remove(text.IndexOf(outfile[0, i,n]), outfile[0, i,n].Length);

                                int k = no.IndexOf("=");
                                //MessageBox.Show(no.Substring(k + 2, no.IndexOf("元") - k-2));
                                yy[i] -= Convert.ToDouble(no.Substring(k + 2, no.IndexOf("元") - k - 2));
                            }
                            swrw.Write(text);
                            swrw.Write("总金额为：" + yy[i].ToString() + "元");
                            swrw.Close();
                            if (text == "")
                            {
                                File.Delete(Address + "韵苑" + i.ToString() + "栋.txt");
                            }
                        }
                    }
                }

            //for (int i = 0; i < 29; i++)
            //{
            //    if (outfile[1, i] != null)
            //    {
            //        //StreamWriter sw=File.Create(Address+"")
            //        string text;
            //        try
            //        {
            //            StreamReader sr = File.OpenText(Address + "沁苑" + i.ToString() + "栋.txt");
            //            text = sr.ReadToEnd();
            //            sr.Close();
            //            File.Delete(Address + "沁苑" + i.ToString() + "栋.txt");
            //        }
            //        catch
            //        {
            //            text = "";
            //        }

            //        StreamWriter swrw = File.CreateText(Address + "沁苑" + i.ToString() + "栋.txt");
            //        if (text.IndexOf(outfile[1, i]) == -1)
            //        {
            //            text += outfile[1, i];
            //        }
            //        else
            //            text = text.Remove(text.IndexOf(outfile[1, i]), outfile[1, i].Length);
            //        swrw.Write(text);
            //        swrw.Write("总金额为：" + qy[i].ToString() + "元");
            //        swrw.Close();
            //        if (text == "")
            //        {
            //            File.Delete(Address + "沁苑" + i.ToString() + "栋.txt");
            //        }
            //    }
            //}

            //for (int i = 0; i < 29; i++)
            //{
            //    if (outfile[2, i] != null)
            //    {
            //        //StreamWriter sw=File.Create(Address+"")
            //        string text;
            //        try
            //        {
            //            StreamReader sr = File.OpenText(Address + "紫菘" + i.ToString() + "栋.txt");
            //            text = sr.ReadToEnd();
            //            sr.Close();
            //            File.Delete(Address + "紫菘" + i.ToString() + "栋.txt");
            //        }
            //        catch
            //        {
            //            text = "";
            //        }

            //        StreamWriter swrw = File.CreateText(Address + "紫菘" + i.ToString() + "栋.txt");
            //        if (text.IndexOf(outfile[2, i]) == -1)
            //        {
            //            text += outfile[2, i];
            //        }
            //        else
            //            text = text.Remove(text.IndexOf(outfile[2, i]), outfile[2, i].Length);
            //        swrw.Write(text);
            //        swrw.Write("总金额为：" + zs[i].ToString() + "元");
            //        swrw.Close();
            //        if (text == "")
            //        {
            //            File.Delete(Address + "紫菘" + i.ToString() + "栋.txt");
            //        }
            //    }
            //}


                for (int i = 0; i < 29; i++)
                {
                    int tag = 0;
                    for (int n = 0; n < 100; n++)
                    {
                        if (outfile[1, i, n] != null )
                        {
                            //StreamWriter sw=File.Create(Address+"")
                            tag = 1;
                            string text;
                            try
                            {
                                List<string> lines = new List<string>(File.ReadAllLines(Address + "沁苑" + i.ToString() + "栋.txt"));
                                //File.Delete(Address + "韵苑" + i.ToString() + "栋.txt");
                                lines.RemoveAt(lines.Count - 1);//删除第3行
                                File.WriteAllLines(Address + "沁苑" + i.ToString() + "栋.txt", lines.ToArray());

                                StreamReader sr = File.OpenText(Address + "沁苑" + i.ToString() + "栋.txt");
                                text = sr.ReadToEnd();
                                sr.Close();
                                File.Delete(Address + "沁苑" + i.ToString() + "栋.txt");

                            }
                            catch
                            {
                                text = "";
                            }

                            StreamWriter swrw = File.CreateText(Address + "沁苑" + i.ToString() + "栋.txt");
                            if (text.IndexOf(outfile[1, i, n]) == -1)
                            {
                                text += outfile[1, i, n];
                            }
                            else
                            {
                                string no = text.Substring(text.IndexOf(outfile[1, i, n]), outfile[1, i, n].Length);
                                text = text.Remove(text.IndexOf(outfile[1, i, n]), outfile[1, i, n].Length);

                                int k = no.IndexOf("=");
                                qy[i] -= Convert.ToDouble(no.Substring(k + 2, no.IndexOf("元") - k - 2));
                            }
                            swrw.Write(text);
                            swrw.Write("总金额为：" + qy[i].ToString() + "元");
                            swrw.Close();
                            if (text == "")
                            {
                                File.Delete(Address + "沁苑" + i.ToString() + "栋.txt");
                            }
                        }
                    }
                }

                for (int i = 0; i < 29; i++)
                {
                    int tag = 0;
                    for (int n = 0; n < 100; n++)
                    {
                        if (outfile[2, i, n] != null)
                        {
                            //StreamWriter sw=File.Create(Address+"")
                            tag = 1;
                            string text;
                            try
                            {
                                List<string> lines = new List<string>(File.ReadAllLines(Address + "紫菘" + i.ToString() + "栋.txt"));
                                //File.Delete(Address + "韵苑" + i.ToString() + "栋.txt");
                                lines.RemoveAt(lines.Count - 1);//删除第3行
                                File.WriteAllLines(Address + "紫菘" + i.ToString() + "栋.txt", lines.ToArray());

                                StreamReader sr = File.OpenText(Address + "紫菘" + i.ToString() + "栋.txt");
                                text = sr.ReadToEnd();
                                sr.Close();
                                File.Delete(Address + "紫菘" + i.ToString() + "栋.txt");

                            }
                            catch
                            {
                                text = "";
                            }

                            StreamWriter swrw = File.CreateText(Address + "紫菘" + i.ToString() + "栋.txt");
                            if (text.IndexOf(outfile[2, i, n]) == -1)
                            {
                                text += outfile[2, i, n];
                            }
                            else
                            {
                                string no = text.Substring(text.IndexOf(outfile[2, i, n]), outfile[2, i, n].Length);
                                text = text.Remove(text.IndexOf(outfile[2, i, n]), outfile[2, i, n].Length);

                                int k = no.IndexOf("=");
                                zs[i] -= Convert.ToDouble(no.Substring(k + 2, no.IndexOf("元") - k - 2));
                            }
                            swrw.Write(text);
                            swrw.Write("总金额为：" + zs[i].ToString() + "元");
                            swrw.Close();
                            if (text == "")
                            {
                                File.Delete(Address + "紫菘" + i.ToString() + "栋.txt");
                            }
                        }
                    }
                }



            //MessageBox.Show("ok!");

            string[] sFile = Directory.GetFiles(Application.StartupPath + "\\楼栋文件信息\\" + DateTime.Now.ToLongDateString().ToString() + "\\");

            for (int i = 0; i < sFile.Length; i++)
            {
                sFile[i] = sFile[i].Substring(sFile[i].IndexOf("楼栋文件信息")+7);
            }

            for (int i = 0; i < sFile.Length; i++)
            {
                labels[i] = new Label();
                labels[i].Text = sFile[i];
                labels[i].Dock = System.Windows.Forms.DockStyle.Top;
                labels[i].Location = new System.Drawing.Point(0, 80 * i);
                labels[i].Click += new EventHandler(click);
                labels[i].MouseHover += new EventHandler(mouseHover);
                labels[i].MouseLeave += new EventHandler(mouseLeave);
                labels[i].TabIndex = i;
                this.panel1.Controls.Add(labels[i]);
            }

            label1.Text = "加载完成";

        }

        private void click(object sender, EventArgs e)
        {
            Label currentLabel = (Label)sender;
            //MessageBox.Show(currentLabel.Text);
            System.Diagnostics.Process.Start(Application.StartupPath + "\\楼栋文件信息\\" + currentLabel.Text);
            currentLabel.ForeColor = Color.Red;
        }

        private void mouseHover(object sender, EventArgs e)
        {
            Label currentLabel = (Label)sender;
            currentLabel.Font = new Font("宋体", 9, FontStyle.Underline);
        }

        private void mouseLeave(object sender, EventArgs e)
        {
            Label currentLabel = (Label)sender;
            currentLabel.Font = new Font("宋体", 9);
        }
    }
}
