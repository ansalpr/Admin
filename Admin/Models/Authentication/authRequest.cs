using Admin.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminAPI.Models
{
    public class authRequest:BaseRequest
    {
        private string _name;
        private string _password;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }       
    }
}