using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SchoolPrintDomin;
using System.Net;
using System.Web;

namespace SchoolPrint
{
    public partial class ListboxItem : UserControl
    {
        private string url;    //下载路径
        private string filePath;   //文件存储路径
        private string fileName;    //文件名
        private string fileId;      //文件id编号
        private FileType fileType;    //文件类型
        private DownloadState downState;   //标记文件状态
        private Image fileImage;    //
        DownloadUtil httpfile;
        frmMain formFather;
        private double thisSum = 0.0;
        //private string dayAddress;   //每天文件存放位置
        static public string dayAddress;
        static public string check;
        private string paperType;  //
        private string copies;
        private string printMode;
        private string remark;
        private string ID;
        private string Names;
        private string Phone;
        private string Upload_time;
        private string send_time;
        private string user_id;
        private string user_class;
        //private string check;

        WebReference.DALWebService service = new WebReference.DALWebService();   //建立服务器对象
        DominInfo dominInfo = new DominInfo();


        MsgClass msgClass = new MsgClass();

        public ListboxItem()
        {
            //MsgClass msgClass = new MsgClass();
            InitializeComponent();
            //service.Credentials = new NetworkCredential(dominInfo.UserName, dominInfo.Password, dominInfo.Domin);
        }
        public ListboxItem(string url,string fileName,string fileId,frmMain formfather,string paperType,string copies,string printMode,string remark,string ID,string Names,string Phone,string upload_time,string send_time,Color color,string user_id,string user_class)
        {
            //service.Credentials = new NetworkCredential(dominInfo.UserName, dominInfo.Password, dominInfo.Domin);
            this.url = url;
            this.filePath = dayAddress;
            this.fileName = fileName;
            this.fileId = fileId;
            //this.dayAddress = dayaddress;
            this.ID = ID;
            this.user_id = user_id;
            this.user_class = user_class;
            //this.check = check;
            //MsgClass msgClass = new MsgClass();
            InitializeComponent();
            this.BackColor = color;
            btnOpen.Text = "下载";
            btnDelete.Text = "暂停";
            //btnDelete.Text = "确认";
            /*************************************/
            if (File.Exists(filePath + "\\" + ID + fileName))
            {
                btnOpen.Text = "打开";
                btnDelete.Text = "确认";
                progressBar.Visible = false;
                downState = DownloadState.Finished;
                lblDownSpeed.Text = "下载完成";
            }
            /*************************************/
            this.formFather = formfather;
            this.paperType = paperType;
            this.copies = copies;
            this.printMode = printMode;
            this.remark = remark;
            this.Names = Names;
            this.Phone = Phone;
            this.Upload_time = upload_time;
            this.send_time = send_time;
            lblProgress.Text = paperType + " " + copies + " " + printMode + " " + remark;

            if (fileName.EndsWith("ppt") || fileName.EndsWith("pptx"))
            {
                fileType = FileType.PPT;
                fileImage = global::SchoolPrint.Properties.Resources.ppt;
            }
            else if (fileName.EndsWith("doc") || fileName.EndsWith("docx"))
            {
                fileType = FileType.WORD;
                fileImage = global::SchoolPrint.Properties.Resources.word;
            }
            else if (fileName.EndsWith("xls") || fileName.EndsWith("xlsx"))
            {
                fileType = FileType.EXCEL;
                fileImage = global::SchoolPrint.Properties.Resources.excel;
            }
            else if (fileName.EndsWith("pdf"))
            {
                fileType = FileType.PDF;
                fileImage = global::SchoolPrint.Properties.Resources.pdf;
            }
            else
            {
                fileType = FileType.UNKNOW;
                fileImage = null;
            }
        }

        public void ListboxItem_Start(object sender, EventArgs e)
        {
            if ((downState != DownloadState.Downloading) && (downState != DownloadState.Finished))
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                httpfile = new DownloadUtil(url, filePath + "\\" + ID + fileName, 5, msgClass);//多线程下载文件类(参数：下载文件的远程地址URL、保存到本地的文件名称、多少个线程下载)
                httpfile.downfile();//开始多线程下载文件
                timDown_Tick(sender, e);
                downState = DownloadState.Downloading;
            }
            //progressBar.Maximum = msgClass.FileSize;
            //lblFileName.Text = fileName;
            //lblFileId.Text = fileId;
            ////label1.Text = MsgClass.FileSize.ToString();
            //picImage.BackgroundImage = fileImage;
        }

        private void ListboxItem_MouseHover(object sender, EventArgs e)
        {
            //this.BackColor = Color.LightBlue;
        }

        private void ListboxItem_MouseLeave(object sender, EventArgs e)
        {
            //this.BackColor = System.Drawing.SystemColors.Control;
        }

        private void timDown_Tick(object sender, EventArgs e)
        {
            if (httpfile != null)
            {
                int data = 0;
                for (int i = 0; i < msgClass.ThreadCount; i++)
                {
                    data += msgClass.threadsdata[i];
                }

                try
                {
                    progressBar.Maximum = msgClass.FileSize;
                    //进度条有问题
                    //label1.Text = MsgClass.FileSize.ToString();
                    progressBar.Value = data;
                    //
                }
                catch { }

                if (msgClass.Msg == "下载文件完成。")
                {
                    downState = DownloadState.Finished;
                    timDown.Stop();
                    progressBar.Visible = false;
                    lblFileName.Top += 10;
                    lblFileId.Top += 10;
                    //lblDownSpeed.Visible = false;
                    //lblProgress.Text = "下载完成";
                    lblDownSpeed.Text = "下载完成";
                    btnOpen.Text = "打开";
                    btnDelete.Text = "确认";
                }
                else
                {
                    TimeSpan TotalTs = DateTime.Now - msgClass.StartTime;//已下载文件的时间
                    double PByte = data / TotalTs.TotalSeconds / 1024;//计算速度，byte/秒换算成KB/秒
                    if (msgClass.Msg.IndexOf("正在下载") > 0)
                        lblDownSpeed.Text = string.Format("{0} KB/秒", (int)PByte);
                    else
                        lblDownSpeed.Text = "0 KB/秒";
                    //if(progressBar.Maximum!=0)
                    //    lblProgress.Text = (int)(progressBar.Value * 100 / progressBar.Maximum)+"%";
                    //lblProgress.Text = "这里是备注";
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDelete.Text == "继续")
            {
                if (httpfile != null) httpfile.Resume();
                btnDelete.Text = "暂停";
            }
            else if ((downState != DownloadState.Prepare) && btnDelete.Text != "确认")
            {
                if (httpfile != null) httpfile.Suspend();//暂停文件下载
                //if (downState == DownloadState.Finished)
                //{
                //    File.Delete(filePath + "\\" + fileName);
                //}
                //else File.Delete(filePath + "\\" + fileName + ".lu");
                btnDelete.Text = "继续";
            }
            if (btnDelete.Text == "确认")
            {
                //将金额传到服务器
                //service.UpMoney(ID, textBox1.Text);
                string times = DateTime.Now.ToLocalTime().ToString();
                ////int x =times.IndexOf("星");
                ////times = times.Remove(x, 3);
                //service.UpTime(ID, times);
                //service.EnsureDownload(ID, "已打印");
                //service.EnsureCheck(ID, check);
                //Update("ID",ID.ToString(), "money",textBox1.Text);
                //Update("ID",ID.ToString(), "Suretime",times);
                //Update("ID",ID.ToString(), "statues", "已打印");
                //Update("ID",ID.ToString(), "CheckDate", check);
                //string Text = "true";

                string Text = formFather.PostDataToUrl("uid" + "=" + formFather.uid + "&" + "o_tid" + "=" + ID.ToString() + "&" + "o_money" + "=" + textBox1.Text, "http://www.xiaoyintong.com/v3_school_printer/once_commit");
                //MessageBox.Show("uid" + "=" + formFather.uid + "&" + "o_id" + "=" + ID.ToString() + "&" + "o_money" + "=" + textBox1.Text);
                //Text = "true";
                
                //MessageBox.Show(check);
                if (Text.IndexOf("true") != -1)
                {
                    string no_recive_path = "";
                    if (send_time == "今天 12:30 ~ 13:00")
                        no_recive_path = Application.StartupPath + "\\文件数据\\no_recive_morning";
                    else if (send_time == "今天 22:00 ~ 23:00")
                        no_recive_path = Application.StartupPath + "\\文件数据\\no_recive_evening";
                    else if (send_time == "明天 12:30 ~ 13:00")
                        no_recive_path = Application.StartupPath + "\\文件数据\\" + DateTime.Now.AddDays(1).ToShortDateString().Replace("\\", "-") + "_morning";
                    else if (send_time == "明天 22:00 ~ 23:00")
                        no_recive_path = Application.StartupPath + "\\文件数据\\" + DateTime.Now.AddDays(1).ToShortDateString().Replace("\\", "-") + "_evening";
                    StreamWriter sw_add = new StreamWriter(no_recive_path, true, Encoding.Default);
                    sw_add.WriteLine(ID);
                    sw_add.Close();
#region nomal user record
                    if (user_class == "7000") //normal user
                    {
                        //修改本地记录
                        string dayCount = "今日打印情况.txt";
                        string dayCountS = "今日打印.la";
                        string text;
                        double mon = Convert.ToDouble(formFather.label6.Text.Remove(formFather.label6.Text.IndexOf("元")));
                        double single;
                        if (paperType.IndexOf("单面") >= 0)
                            single = Convert.ToDouble(formFather.label11.Text.Remove(formFather.label11.Text.IndexOf("元")));
                        else
                            single = Convert.ToDouble(formFather.label12.Text.Remove(formFather.label12.Text.IndexOf("元")));
                        //int k;
                        ////确认后，金额不能再更改
                        //textBox1.ReadOnly = true;
                        lblDownSpeed.Text = "已打印!";
                        lblDownSpeed.ForeColor = Color.Red;
                        //MessageBox.Show(dayAddress + "\\" + dayCount);
                        if (!File.Exists(dayAddress + "\\" + dayCount))//不存在记录文件
                        {

                            mon -= thisSum;
                            formFather.Sum -= thisSum;
                            single -= thisSum;
                            thisSum = Convert.ToDouble(textBox1.Text);
                            mon += thisSum;
                            single += thisSum;
                            formFather.Sum += Convert.ToDouble(textBox1.Text);
                            formFather.label6.Text = mon.ToString() + "元";
                            formFather.label5.Text = formFather.Sum.ToString() + "元";
                            if (paperType.IndexOf("单面") >= 0)
                                formFather.label11.Text = single.ToString() + "元";
                            else
                                formFather.label12.Text = single.ToString() + "元";
                            StreamWriter sw = File.CreateText(dayAddress + "\\" + dayCount);
                            sw.WriteLine(DateTime.Now.ToLongDateString().ToString() + "总金额是：" + formFather.label5.Text);
                            sw.WriteLine(" ");
                            sw.WriteLine(fileName + "  " + Names + "  " + fileId + "  " + copies + "  " + paperType + "  " + printMode + "  " + remark + " = " + thisSum);
                            //sw.Write("\r\n");
                            sw.Close();
                            //File.SetAttributes(dayAddress + "\\" + dayCount, System.IO.FileAttributes.ReadOnly); 
                            StreamReader swr = File.OpenText(dayAddress + "\\" + dayCount);
                            text = swr.ReadToEnd();
                            swr.Close();

                            byte[] chang = Encoding.Unicode.GetBytes(text);

                            for (int i = 0; i < chang.Length; i++)
                            {
                                chang[i] += 21;
                            }

                            string temp = Encoding.Unicode.GetString(chang);
                            File.Delete(dayAddress + "\\" + dayCountS);
                            sw = File.CreateText(dayAddress + "\\" + dayCountS);
                            sw.Write(temp);
                            sw.Close();

                        }
                        else
                        {

                            /*从加密文件恢复原文件*/
                            StreamReader srs = File.OpenText(dayAddress + "\\" + dayCountS);
                            string temps = srs.ReadToEnd();
                            srs.Close();
                            byte[] changs = Encoding.Unicode.GetBytes(temps);

                            for (int i = 0; i < changs.Length; i++)
                            {
                                changs[i] -= 21;
                            }
                            temps = Encoding.Unicode.GetString(changs);
                            StreamWriter srsw = new StreamWriter(dayAddress + "\\" + dayCount, false);
                            srsw.Write(temps);
                            srsw.Close();
                            /****************************************/

                            StreamReader sr = File.OpenText(dayAddress + "\\" + dayCount);
                            StreamWriter swr;
                            int len;
                            text = sr.ReadToEnd();
                            sr.Close();
                            //string test = formFather.Sum.ToString() + "元";
                            string test = formFather.label5.Text;
                            text = text.Replace(formFather.label5.Text, "");
                            len = formFather.label5.Text.Length;
                            //MessageBox.Show(text.IndexOf(fileName).ToString());
                            if (text.IndexOf(fileName) == -1)
                            {
                                formFather.Sum -= thisSum;
                                mon -= thisSum;
                                single -= thisSum;
                                thisSum = Convert.ToDouble(textBox1.Text);
                                mon += thisSum;
                                single += thisSum;
                                formFather.Sum += Convert.ToDouble(textBox1.Text);
                                formFather.label6.Text = mon.ToString() + "元";
                                formFather.label5.Text = formFather.Sum.ToString() + "元";
                                if (paperType.IndexOf("单面") >= 0)
                                    formFather.label11.Text = single.ToString() + "元";
                                else
                                    formFather.label12.Text = single.ToString() + "元";
                                text = text.Insert(text.IndexOf("：") + 1, formFather.label5.Text);
                                text += fileName + "  " + Names + "  " + fileId + "  " + copies + "  " + paperType + "  " + printMode + "  " + remark + " = " + thisSum + "\r\n";
                                File.Delete(dayAddress + "\\" + dayCount);
                                swr = File.CreateText(dayAddress + "\\" + dayCount);
                                swr.Write(text);
                                swr.Close();
                                //File.SetAttributes(dayAddress + "\\" + dayCount, System.IO.FileAttributes.ReadOnly); 

                                byte[] chang = Encoding.Unicode.GetBytes(text);

                                for (int i = 0; i < chang.Length; i++)
                                {
                                    chang[i] += 21;
                                }

                                string temp = Encoding.Unicode.GetString(chang);
                                File.Delete(dayAddress + "\\" + dayCountS);
                                swr = File.CreateText(dayAddress + "\\" + dayCountS);
                                swr.Write(temp);
                                swr.Close();
                            }
                            else
                            {

                                int place = text.IndexOf(fileName);
                                string subtext = text.Substring(place);
                                int subplace = subtext.IndexOf("\r");
                                int subequ = subtext.IndexOf("=") + 1;
                                //MessageBox.Show(subtext.Substring(subequ, subplace - subequ));
                                thisSum = Convert.ToInt32(subtext.Substring(subequ, subplace - subequ));
                                text = text.Remove(place, subplace);

                                formFather.Sum -= thisSum;
                                mon -= thisSum;
                                single -= thisSum;
                                thisSum = Convert.ToDouble(textBox1.Text);
                                mon += thisSum;
                                single += thisSum;
                                formFather.Sum += Convert.ToDouble(textBox1.Text);
                                formFather.label6.Text = mon.ToString() + "元";
                                formFather.label5.Text = formFather.Sum.ToString() + "元";
                                if (paperType.IndexOf("单面") >= 0)
                                    formFather.label11.Text = single.ToString() + "元";
                                else
                                    formFather.label12.Text = single.ToString() + "元";
                                int len2 = formFather.label5.Text.Length;
                                text = text.Insert(text.IndexOf("：") + 1, formFather.label5.Text);
                                text = text.Insert(place + len2, fileName + "  " + Names + "  " + fileId + "  " + copies + "  " + paperType + "  " + printMode + "  " + remark + " = " + thisSum + "\r\n");

                                File.Delete(dayAddress + "\\" + dayCount);
                                swr = File.CreateText(dayAddress + "\\" + dayCount);
                                swr.Write(text);
                                swr.Close();

                                byte[] chang = Encoding.Unicode.GetBytes(text);

                                for (int i = 0; i < chang.Length; i++)
                                {
                                    chang[i] += 21;
                                }

                                string temp = Encoding.Unicode.GetString(chang);
                                File.Delete(dayAddress + "\\" + dayCountS);
                                swr = File.CreateText(dayAddress + "\\" + dayCountS);
                                swr.Write(temp);
                                swr.Close();

                                //File.SetAttributes(dayAddress + "\\" + dayCount, System.IO.FileAttributes.ReadOnly); 
                            }
                        }
                    }
#endregion
#region VIP user record
                    else if (user_class == "7001")
                    {
                        //修改本地记录
                        string dayCount = "今日打印情况_vip.txt";
                        string dayCountS = "今日打印_vip.la";
                        string text;
                        double mon = Convert.ToDouble(formFather.label18.Text.Remove(formFather.label18.Text.IndexOf("元")));
                        double single;
                        if (paperType.IndexOf("单面") >= 0)
                            single = Convert.ToDouble(formFather.label15.Text.Remove(formFather.label15.Text.IndexOf("元")));
                        else
                            single = Convert.ToDouble(formFather.label16.Text.Remove(formFather.label16.Text.IndexOf("元")));
                        //int k;
                        ////确认后，金额不能再更改
                        //textBox1.ReadOnly = true;
                        lblDownSpeed.Text = "已打印!";
                        lblDownSpeed.ForeColor = Color.Red;
                        //MessageBox.Show(dayAddress + "\\" + dayCount);
                        if (!File.Exists(dayAddress + "\\" + dayCount))//不存在记录文件
                        {

                            mon -= thisSum;
                            formFather.Sum -= thisSum;
                            single -= thisSum;
                            thisSum = Convert.ToDouble(textBox1.Text);
                            mon += thisSum;
                            single += thisSum;
                            formFather.Sum += Convert.ToDouble(textBox1.Text);
                            formFather.label18.Text = mon.ToString() + "元";
                            formFather.label19.Text = formFather.Sum.ToString() + "元";
                            if (paperType.IndexOf("单面") >= 0)
                                formFather.label15.Text = single.ToString() + "元";
                            else
                                formFather.label16.Text = single.ToString() + "元";
                            StreamWriter sw = File.CreateText(dayAddress + "\\" + dayCount);
                            sw.WriteLine(DateTime.Now.ToLongDateString().ToString() + "总金额是：" + formFather.label19.Text);
                            sw.WriteLine(" ");
                            sw.WriteLine(fileName + "  " + Names + "  " + fileId + "  " + copies + "  " + paperType + "  " + printMode + "  " + remark + " = " + thisSum);
                            //sw.Write("\r\n");
                            sw.Close();
                            //File.SetAttributes(dayAddress + "\\" + dayCount, System.IO.FileAttributes.ReadOnly); 
                            StreamReader swr = File.OpenText(dayAddress + "\\" + dayCount);
                            text = swr.ReadToEnd();
                            swr.Close();

                            byte[] chang = Encoding.Unicode.GetBytes(text);

                            for (int i = 0; i < chang.Length; i++)
                            {
                                chang[i] += 21;
                            }

                            string temp = Encoding.Unicode.GetString(chang);
                            File.Delete(dayAddress + "\\" + dayCountS);
                            sw = File.CreateText(dayAddress + "\\" + dayCountS);
                            sw.Write(temp);
                            sw.Close();

                        }
                        else
                        {

                            /*从加密文件恢复原文件*/
                            //StreamReader srs = File.OpenText(dayAddress + "\\" + dayCountS);
                            //string temps = srs.ReadToEnd();
                            //srs.Close();
                            //byte[] changs = Encoding.Unicode.GetBytes(temps);

                            //for (int i = 0; i < changs.Length; i++)
                            //{
                            //    changs[i] -= 21;
                            //}
                            //temps = Encoding.Unicode.GetString(changs);
                            //StreamWriter srsw = new StreamWriter(dayAddress + "\\" + dayCount, false);
                            //srsw.Write(temps);
                            //srsw.Close();
                            /****************************************/

                            StreamReader sr = File.OpenText(dayAddress + "\\" + dayCount);
                            StreamWriter swr;
                            int len;
                            text = sr.ReadToEnd();
                            sr.Close();
                            //string test = formFather.Sum.ToString() + "元";
                            string test = formFather.label19.Text;
                            text = text.Replace(formFather.label19.Text, "");
                            len = formFather.label19.Text.Length;
                            //MessageBox.Show(text.IndexOf(fileName).ToString());
                            if (text.IndexOf(fileName) == -1)
                            {
                                formFather.Sum -= thisSum;
                                mon -= thisSum;
                                single -= thisSum;
                                thisSum = Convert.ToDouble(textBox1.Text);
                                mon += thisSum;
                                single += thisSum;
                                formFather.Sum += Convert.ToDouble(textBox1.Text);
                                formFather.label18.Text = mon.ToString() + "元";
                                formFather.label19.Text = formFather.Sum.ToString() + "元";
                                if (paperType.IndexOf("单面") >= 0)
                                    formFather.label15.Text = single.ToString() + "元";
                                else
                                    formFather.label16.Text = single.ToString() + "元";
                                text = text.Insert(text.IndexOf("：") + 1, formFather.label19.Text);
                                text += fileName + "  " + Names + "  " + fileId + "  " + copies + "  " + paperType + "  " + printMode + "  " + remark + " = " + thisSum + "\r\n";
                                File.Delete(dayAddress + "\\" + dayCount);
                                swr = File.CreateText(dayAddress + "\\" + dayCount);
                                swr.Write(text);
                                swr.Close();
                                //File.SetAttributes(dayAddress + "\\" + dayCount, System.IO.FileAttributes.ReadOnly); 

                                byte[] chang = Encoding.Unicode.GetBytes(text);

                                for (int i = 0; i < chang.Length; i++)
                                {
                                    chang[i] += 21;
                                }

                                //string temp = Encoding.Unicode.GetString(chang);
                                //File.Delete(dayAddress + "\\" + dayCountS);
                                //swr = File.CreateText(dayAddress + "\\" + dayCountS);
                                //swr.Write(temp);
                                //swr.Close();
                            }
                            else
                            {

                                int place = text.IndexOf(fileName);
                                string subtext = text.Substring(place);
                                int subplace = subtext.IndexOf("\r");
                                int subequ = subtext.IndexOf("=") + 1;
                                //MessageBox.Show(subtext.Substring(subequ, subplace - subequ));
                                thisSum = Convert.ToInt32(subtext.Substring(subequ, subplace - subequ));
                                text = text.Remove(place, subplace);

                                formFather.Sum -= thisSum;
                                mon -= thisSum;
                                single -= thisSum;
                                thisSum = Convert.ToDouble(textBox1.Text);
                                mon += thisSum;
                                single += thisSum;
                                formFather.Sum += Convert.ToDouble(textBox1.Text);
                                formFather.label18.Text = mon.ToString() + "元";
                                formFather.label19.Text = formFather.Sum.ToString() + "元";
                                if (paperType.IndexOf("单面") >= 0)
                                    formFather.label15.Text = single.ToString() + "元";
                                else
                                    formFather.label16.Text = single.ToString() + "元";
                                int len2 = formFather.label19.Text.Length;
                                text = text.Insert(text.IndexOf("：") + 1, formFather.label19.Text);
                                text = text.Insert(place + len2, fileName + "  " + Names + "  " + fileId + "  " + copies + "  " + paperType + "  " + printMode + "  " + remark + " = " + thisSum + "\r\n");

                                File.Delete(dayAddress + "\\" + dayCount);
                                swr = File.CreateText(dayAddress + "\\" + dayCount);
                                swr.Write(text);
                                swr.Close();

                                byte[] chang = Encoding.Unicode.GetBytes(text);

                                for (int i = 0; i < chang.Length; i++)
                                {
                                    chang[i] += 21;
                                }

                                //string temp = Encoding.Unicode.GetString(chang);
                                //File.Delete(dayAddress + "\\" + dayCountS);
                                //swr = File.CreateText(dayAddress + "\\" + dayCountS);
                                //swr.Write(temp);
                                //swr.Close();

                                //File.SetAttributes(dayAddress + "\\" + dayCount, System.IO.FileAttributes.ReadOnly); 
                            }
                        }
                    }
#endregion
                    
                }
                else
                {
                    MessageBox.Show("金额传值错误");
                    Text = Text.Substring(Text.IndexOf("info") + 6);

                    MessageBox.Show(Text);
                }
                
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (btnOpen.Text == "下载")
            {
                ListboxItem_Start(sender, e);
            }
            if ((downState == DownloadState.Finished)&&(btnOpen.Text == "打开"))
            {
                //System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
                //psi.Arguments = " /select," + filePath + "\\" + fileName;
                //MessageBox.Show(psi.Arguments);
                //System.Diagnostics.Process.Start(psi);
                System.Diagnostics.Process.Start(filePath + "\\" + ID + fileName);
            }
        }

        private void ListboxItem_Load(object sender, EventArgs e)
        {
            progressBar.Maximum = msgClass.FileSize;
            lblFileName.Text = fileName;
            lblFileId.Text = fileId;
            //label1.Text = MsgClass.FileSize.ToString();
            picImage.BackgroundImage = fileImage;
        }

        private void lblProgress_Click(object sender, EventArgs e)
        {
            string text_show;
            text_show = paperType + "  " + printMode + "  \n" + remark + "  " + Names + "  " + Phone + "\n提交时间" + Upload_time + "\n送货时间" + send_time;
            MessageBox.Show(text_show);
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

        public double GetMoney()
        {
            if (btnOpen.Text == "打开")
            {
                double temp;
                if (double.TryParse(textBox1.Text, out temp))
                {
                    return double.Parse(textBox1.Text);
                }
                else
                    return -100.0;
            }
            else
            {
                return -100.0;
            }
        }

    }
    public enum DownloadState
    {
        Prepare,Downloading,Stop,Finished
    }
    public enum FileType
    {
        EXCEL,PDF,PPT,WORD,UNKNOW
    }
}
