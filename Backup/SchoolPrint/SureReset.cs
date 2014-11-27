using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SchoolPrint
{
    public partial class SureReset : Form
    {
        public SureReset()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "westudio")
            {
                MessageBox.Show("密码不正确，禁止清零");
                this.Close();
            }
            else
            {
                SchoolPrint.frmMain.fatherForm.NewFileFolder();
                this.Close();
            }
        }
    }
}
