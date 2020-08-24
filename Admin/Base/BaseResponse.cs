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
        private string _Tui;
        private string _signature;
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public string Tui
        {
            get { return _Tui; }
            set { _Tui = value; }
        }
        public string Signature
        {
            get { return _signature; }
            set { _signature = value; }
        }
    }
}