using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SchoolPrint
{
    public partial class time_form : UserControl
    {
        public time_form()
        {
            InitializeComponent();
        }

        public time_form(string labels)
        {
            
            InitializeComponent();
            label1.Text = labels;
        }
    }
}
