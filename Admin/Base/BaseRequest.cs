using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Base
{
    public class BaseRequest
    {
        private string _Tui;
        public string Tui
        {
            get { return _Tui; }
            set { _Tui = value; }
        }
    }
}