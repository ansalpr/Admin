using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class Module
    {
        private string _ModuleName;
        private string _ModuleCode;
        private string _ModuleId;
        private string _action;
        private string _status;
        private string _message;
        public string Name
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }
        public string Code
        {
            get { return _ModuleCode; }
            set { _ModuleCode = value; }
        }
        public string Id
        {
            get { return _ModuleId; }
            set { _ModuleId = value; }
        }
        public string action
        {
            get { return _action; }
            set { _action = value; }
        }
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}