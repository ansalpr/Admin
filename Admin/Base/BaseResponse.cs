using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Base
{
    public class BaseResponse
    {
        private string _code;
        private string _message;
        private string _tui;
        private string _signature;
        public string code
        {
            get { return _code; }
            set { _code = value; }
        }
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        public string tui
        {
            get { return _tui; }
            set { _tui = value; }
        }
        public string signature
        {
            get { return _signature; }
            set { _signature = value; }
        }
    }
}