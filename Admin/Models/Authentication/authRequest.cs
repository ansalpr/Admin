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
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }       
    }
}