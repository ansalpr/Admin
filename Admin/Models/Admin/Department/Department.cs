using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class Department
    {
        private string _DepartmentName;
        private string _DepartmentCode;
        private string _DepartmentId;
        private string _action;
        private string _status;
        private string _message;
        public string Name
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; }
        }
        public string Code
        {
            get { return _DepartmentCode; }
            set { _DepartmentCode = value; }
        }
        public string Id
        {
            get { return _DepartmentId; }
            set { _DepartmentId = value; }
        }
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
    }
}