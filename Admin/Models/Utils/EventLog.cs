using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Utils
{
    public class EventLogs
    {
        private string _remarks;
        private string _methodName;
        private string _className;
        private string _Tui;
        private string _ip;
        private string _action;
        private string _status;
        private string _message;


        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        public string Tui
        {
            get { return _Tui; }
            set { _Tui = value; }
        }
        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
    }
}