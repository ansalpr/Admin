using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Other
{
    public class DataProxy
    {
        private string _name;
        private string _conName;
        private string _type;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string conName
        {
            get { return _conName; }
            set { _conName = value; }
        }
        public string type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}