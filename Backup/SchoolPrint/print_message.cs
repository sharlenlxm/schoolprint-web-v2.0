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

namespace SchoolPrint
{
    public partial class print_message : UserControl
    {
        frmMain FatherForm;
        string Building;
        string Name;
        double Money;
        string filePathGloble = System.Windows.Forms.Application.StartupPath+"\\文件数据\\";

        public print_message(string building,string name,frmMain fatherForm)
        {
            InitializeComponent();

            label1.Text = building + " " + name;

            this.FatherForm = fatherForm;
            this.Building = building;
            this.Name = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Money = FatherForm.AllMoney(Building);
            #region print_message
            string filepath = filePathGloble + "dower.doc";
            object fileobj = filepath;
            object nullobj = System.Reflection.Missing.Value;
            object missing = System.Reflection.Missing.Value;
            object bookmark = "dower";
            object save = false;
            Word.Application app = new Word.Application();
            Word.Document doc = null;
            try
            {
                doc = app.Documents.Open(ref fileobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj);
                app.Visible = false;
                if (doc.Bookmarks.Exists("dower"))
                {
                    doc.Bookmarks.get_Item(ref bookmark).Range.Text = Money.ToString() + "元";
                    doc.Bookmarks.get_Item(ref bookmark).Range.Text = Building + "\r\n";
                    doc.Bookmarks.get_Item(ref bookmark).Range.Text = Name + "\r\n";
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
            }
            #endregion
        }

        public void color_set(Color set)
        {
            this.BackColor = set;
        }
    }
}
