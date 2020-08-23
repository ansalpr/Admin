using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class Menu
    {
        private string _MenuId;
        private string _MenuCode;
        private string _MenuName;
        private string _Path;
        private string _ModuleCode;
        private string _action;
        private string _status;
        private string _message;
        public string Id
        {
            get { return _MenuId; }
            set { _MenuId = value; }
        }
        public string Code
        {
            get { return _MenuCode; }
            set { _MenuCode = value; }
        }
        public string Name
        {
            get { return _MenuName; }
            set { _MenuName = value; }
        }
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
        public string ModuleCode
        {
            get { return _ModuleCode; }
            set { _ModuleCode = value; }
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