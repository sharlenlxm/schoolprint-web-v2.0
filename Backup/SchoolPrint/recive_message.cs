using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SchoolPrint
{
    public partial class recive_message : Form
    {
        frmMain fatherform;
        string uid;

        public recive_message()
        {
            InitializeComponent();
        }

        public recive_message(frmMain fatherform,string uid)
        {
            this.fatherform = fatherform;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader sr_recive = new StreamReader(Application.StartupPath + "\\文件数据\\no_recive_morning");
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
            //MessageBox.Show(send_message);
            //send to the server
            string Text = fatherform.PostDataToUrl("uid" + "=" + uid + "&" + "info" + "=" + send_message, "http://www.xiaoyintong.com/v3_school_printer/commit_pickup");
            //MessageBox.Show(Text);
            //Text = "sure";
            if (Text.IndexOf("sure") >= 0)
            {
                File.Delete(Application.StartupPath + "\\文件数据\\no_recive_morning");
                MessageBox.Show("取货成功");
            }
            else
                MessageBox.Show("出错了！\n" + Text);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
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
            //MessageBox.Show(send_message);
            //send to the server
            string Text = fatherform.PostDataToUrl("uid" + "=" + uid + "&" + "info" + "=" + send_message, "http://www.xiaoyintong.com/v3_school_printer/commit_pickup");
            //MessageBox.Show(Text);
            if (Text.IndexOf("sure") >= 0)
            {
                File.Delete(Application.StartupPath + "\\文件数据\\no_recive_evening");
                MessageBox.Show("取货成功");
            }
            else
                MessageBox.Show("出错了！\n" + Text);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
