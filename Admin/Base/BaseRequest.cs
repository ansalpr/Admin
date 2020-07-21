using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Base
{
    public class BaseRequest
    {
        private string _tui;
        public string tui
        {
            get { return _tui; }
            set { _tui = value; }
        }
    }
}