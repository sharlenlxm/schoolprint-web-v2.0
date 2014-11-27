using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolPrintDomin
{
    public class DominInfo
    {
        private string username = "administrator";

        public string UserName
        {
            get { return username; }
            set { username = value; }
        }
        private string password = "lzc62351087";

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string domin = "http://202.114.18.229:2000";

        public string Domin
        {
            get { return domin; }
            set { domin = value; }
        }

    }
}
