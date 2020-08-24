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
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string ConName
        {
            get { return _conName; }
            set { _conName = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}