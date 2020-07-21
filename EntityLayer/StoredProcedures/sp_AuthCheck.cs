using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.StoredProcedures
{
    public class sp_AuthCheck
    {
        private string _name;
        private string _pwd;
        private string _action;
        public string uName
        {
            get { return _name; }
            set { _name = value; }
        }
        public string pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
    }
}
