using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.Admin
{
    public class BloodGroup
    {
        private string _BloodGroupName;
        private string _BloodGroupCode;
        private string _BloodGroupId;
        private string _action;
        private string _status;
        private string _message;
        public string Name
        {
            get { return _BloodGroupName; }
            set { _BloodGroupName = value; }
        }
        public string Code
        {
            get { return _BloodGroupCode; }
            set { _BloodGroupCode = value; }
        }
        public string Id
        {
            get { return _BloodGroupId; }
            set { _BloodGroupId = value; }
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